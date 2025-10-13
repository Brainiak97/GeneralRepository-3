using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StateService.DAL.Interfaces;
using StateService.Domain.Dto;
using System.Net.Http.Headers;
using System.Text;

namespace StateService.DAL.Providers
{
    public class HttpGroqProvider(HttpClient httpClient, IConfiguration configuration) : IGroqProvider
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _apiKey = configuration["Groq:ApiKey"]
            ?? throw new InvalidOperationException("Groq:ApiKey is missing in configuration.");
        private readonly string _model = "meta-llama/llama-4-scout-17b-16e-instruct";

        /// <summary>
        /// Общий метод для отправки prompt в Groq и получения ответа
        /// </summary>
        /// <param name="prompt">Сформированный текст запроса</param>
        /// <returns>Строка с ответом модели</returns>
        private async Task<string> QueryModelAsync(string prompt)
        {
            var requestBody = new
            {
                model = _model, // имя модели AI, котороя используется для генерации ответа
                messages = new[] { new { role = "user", content = prompt } }, // массив сообщений в формате чат-истории, как того требует groq-API
                temperature = 0.7, // параметр "творчетсва" модели
                                   // диапазон: обычно от 0.0 до 2.0
                                   // 0.0 — максимально предсказуемый ответ
                                   // 1.0 — баланс между креативностью и точностью
                                   // > 1.0 — очень креативный, но может быть нестабильным
                                   // 0.7 — популярное сбалансированное значение
                max_tokens = 512 // около 400 слов (достаточный объем для ответа)
            };

            var jsonContent = JsonConvert.SerializeObject(requestBody);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Очистка и установка заголовков
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            try
            {
                var response = await _httpClient.PostAsync(_httpClient.BaseAddress!, httpContent);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    dynamic? result = JsonConvert.DeserializeObject(responseString);
                    return result?.choices[0].message.content?.ToString()
                           ?? throw new InvalidOperationException("Ответ Groq пустой или некорректный.");
                }
                else
                {
                    Console.WriteLine($"Groq API Error: {response.StatusCode}");
                    Console.WriteLine($"Response: {responseString}");
                    throw new HttpRequestException($"Запрос Groq не удался с кодом статуса: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение во время запроса Groq: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Генерация рекомендаций по сводке здоровья
        /// </summary>
        public async Task<string> GetHealthRecommendationsAsync(AggregatedHealthSummaryDto summary)
        {
            var periodText = summary.Period switch
            {
                "week" => "за последнюю неделю",
                "month" => "за последний месяц",
                "year" => "за последний год",
                _ => "за выбранный период"
            };

            // === Группируем метрики по имени и считаем среднее ===
            var groupedMetrics = summary.HealthMetrics
                .GroupBy(m => m.MetricName)
                .ToDictionary(
                    g => g.Key,
                    g => new
                    {
                        AvgValue = g.Average(m => m.Value!.Value),
                        Unit = g.FirstOrDefault(m => !string.IsNullOrEmpty(m.Unit))?.Unit ?? ""
                    }
                );

            // === Формируем строки для промпта ===
            var metricsLines = groupedMetrics
                .Select(kvp => $"- {kvp.Key}: {kvp.Value.AvgValue:F1} {kvp.Value.Unit}".Trim())
                .ToList();

            var metricsBlock = metricsLines.Count != 0
                ? string.Join("\n", metricsLines)
                : "- Нет данных о биометрических показателях";

            var prompt = $@"
Проанализируй данные о здоровье пользователя {periodText}, определи, являются ли эти показатели нормой,
укажи отклонения и дай до 5 практических, кратких рекомендаций по улучшению самочувствия.
Не пиши приветствия и не упоминай, что ты ИИ. Только чёткие советы.

**Показатели:**
{metricsBlock}
- Сон: в среднем {summary.AvgSleepDurationHours:F1} часа в сутки, качество сна: {summary.AvgSleepQuality:F1}/10
- Калории: всего сожжено {summary.TotalCaloriesBurned:F0} ккал
- Тренировки: {summary.WorkoutCount} сессий

Сформулируй рекомендации дружелюбно, но профессионально. Акцент на сон, активность, питание, стресс и общее самочувствие.";

            return await QueryModelAsync(prompt.Trim());
        }
    }
}
