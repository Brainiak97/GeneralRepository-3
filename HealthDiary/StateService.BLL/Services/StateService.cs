using AutoMapper;
using MetricService.Api.Contracts;
using MetricService.Api.Contracts.Dtos.Common;
using Shared.Common.Exceptions;
using StateService.BLL.Interfaces;
using StateService.Domain.Dto;
using StateService.Domain.Models;

namespace StateService.BLL.Services
{
    public class StateService(
        IMetricServiceClient metricServiceClient,
        IMapper mapper) : IStateService
    {
        private readonly IMetricServiceClient _metricServiceClient = metricServiceClient;
        private readonly IMapper _mapper = mapper;

        public async Task<UserHealthReport> GetDailySummaryAsync(int userId)
        {
            var today = DateTime.Today;
            var request = new RequestListWithPeriodById { UserId = userId, EndDate = today.AddDays(1), BegDate = today };
            var reports = await GetPeriodSummaryAsync(request);

            return reports.FirstOrDefault() ?? throw new EntryNotFoundException("Не удалось получить данные.");
        }

        public async Task<IEnumerable<UserHealthReport>> GetPeriodSummaryAsync(RequestListWithPeriodById request)
        {
            CheckDate(request);

            var requestMetricService = _mapper.Map<RequestListWithPeriodByIdDTO>(request);
            var metricsTask = _metricServiceClient.GetAllHealthMetricsValue(requestMetricService);
            var workoutsTask = _metricServiceClient.GetAllWorkouts(requestMetricService);
            var sleepTask = _metricServiceClient.GetAllSleeps(requestMetricService);

            await Task.WhenAll(metricsTask, workoutsTask, sleepTask);

            var metricsList = _mapper.Map<List<HealthMetrics>>(metricsTask.Result) ?? [];
            var workoutsList = _mapper.Map<List<Workout>>(workoutsTask.Result) ?? [];
            var sleepList = _mapper.Map<List<Sleep>>(sleepTask.Result) ?? [];


            var dateRange = Enumerable
                .Range(0, 1 + request.EndDate.Subtract(request.BegDate).Days)
                .Select(offset => request.BegDate.AddDays(offset))
                .ToList();

            var reports = new List<UserHealthReport>();

            foreach (var date in dateRange)
            {
                var dateAsDate = date.Date;

                var dailyMetrics = metricsList
                    .Where(m => m.MetricDate.Date == dateAsDate)
                    .ToList();

                var dailyWorkouts = workoutsList
                    .Where(w => w.StartTime.Date == dateAsDate)
                    .ToList();

                var dailySleeps = sleepList
                    .Where(s => s.StartSleep.Date == dateAsDate || s.EndSleep.Date == dateAsDate)
                    .ToList();

                reports.Add(new UserHealthReport
                {
                    Date = DateOnly.FromDateTime(date),
                    HealthMetrics = _mapper.Map<List<HealthMetrics>>(dailyMetrics),
                    PhysicalActivity = dailyWorkouts,
                    Sleep = dailySleeps,
                    FoodData = null
                });
            }

            return reports;
        }

        public async Task<MedicationProgress> GetMedicationProgress(RequestListWithPeriodById request)
        {
            CheckDate(request);

            var requestMetricService = _mapper.Map<RequestListWithPeriodByIdDTO>(request);

            var regimensTask = _metricServiceClient.GetAllRegimens(requestMetricService);
            var intakesTask = _metricServiceClient.GetAllIntakes(requestMetricService);
            var medicationTask = _metricServiceClient.GetAllMedications();
            var dosageFormTask = _metricServiceClient.GetAllDosageForms();

            await Task.WhenAll(regimensTask, intakesTask, medicationTask, dosageFormTask);

            var regimens = regimensTask.Result;
            var intakes = intakesTask.Result;
            var medications = medicationTask.Result;
            var dosageForms = dosageFormTask.Result;

            return new MedicationProgress
            {
                UserId = request.UserId,
                Regimens = _mapper.Map<List<RegimenProgress>>(regimens, opt =>
                {
                    opt.Items["DosageForm"] = dosageForms;
                    opt.Items["Medications"] = medications;
                    opt.Items["Intakes"] = intakes;
                })
            };
        }

        private void CheckDate(RequestListWithPeriodById request)
        {
            if (request.BegDate > request.EndDate)
                throw new ArgumentException($"Дата начала периода ({request.BegDate}) должна быть раньше даты окончания периода ({request.EndDate}).");
        }
    }
}
