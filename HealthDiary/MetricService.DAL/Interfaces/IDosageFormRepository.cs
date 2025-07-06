using MetricService.Domain.Models;


namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о форме выпуска препарата
    /// </summary>
    /// <seealso cref="DosageForm" />
    public interface IDosageFormRepository : IRepository<DosageForm>
    {
    }

}
