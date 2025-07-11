namespace PolyclinicService.BLL.Common.Exceptions;

/// <summary>
/// Класс исключение при отсутствии валидатора в DI-контейнере сервиса.
/// </summary>
/// <param name="message">Сообщение об ошибке.</param>
public class ValidatorNotFoundException(string message) : Exception(message);