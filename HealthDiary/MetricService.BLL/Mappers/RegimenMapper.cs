using MetricService.BLL.DTO.Regimen;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class RegimenMapper
    {
        public static RegimenCreateDTO ToRegimenCreateDTO(this Regimen regimen)
        {
            return new RegimenCreateDTO
            {
                Comment = regimen.Comment,
                Dosage = regimen.Dosage,
                EndDate = regimen.EndDate,
                MedicationId = regimen.MedicationId,
                StartDate = regimen.StartDate,
                Shedule = regimen.Shedule,
                UserId = regimen.UserId,
            };
        }

        public static Regimen ToRegimen(this RegimenCreateDTO regimenCreateDTO)
        {
            return new Regimen
            {
                UserId = regimenCreateDTO.UserId,
                Dosage = regimenCreateDTO.Dosage,
                Shedule = regimenCreateDTO.Shedule,
                StartDate = regimenCreateDTO.StartDate,
                MedicationId = regimenCreateDTO.MedicationId,
                EndDate = regimenCreateDTO.EndDate,
                Comment = regimenCreateDTO.Comment,
                Id = 0
            };
        }

        public static RegimenUpdateDTO ToRegimenUpdateDTO(this Regimen regimen)
        {
            return new RegimenUpdateDTO
            {
                Id = regimen.Id,
                Comment = regimen.Comment,
                Dosage = regimen.Dosage,
                EndDate = regimen.EndDate,
                StartDate = regimen.StartDate,
                Shedule = regimen.Shedule,
            };
        }

        public static Regimen ToRegimen(this RegimenUpdateDTO regimenUpdateDTO, int userId, int medicationId)
        {
            return new Regimen
            {
                EndDate = regimenUpdateDTO.EndDate,
                Id = regimenUpdateDTO.Id,
                Shedule = regimenUpdateDTO.Shedule,
                StartDate = regimenUpdateDTO.StartDate,
                Comment = regimenUpdateDTO.Comment,
                Dosage = regimenUpdateDTO.Dosage,
                UserId = userId,
                MedicationId = medicationId,
            };
        }

        public static RegimenDTO ToRegimenDTO(this Regimen regimen)
        {
            return new RegimenDTO
            {
                Comment = regimen.Comment,
                Dosage = regimen.Dosage,
                MedicationId = regimen.MedicationId,
                UserId = regimen.UserId,
                Shedule = regimen.Shedule,
                Id = regimen.Id
            };
        }

        public static Regimen ToRegimen(this RegimenDTO regimenDTO)
        {
            return new Regimen
            {
                Id = regimenDTO.Id,
                Dosage = regimenDTO.Dosage,
                Comment = regimenDTO.Comment,
                EndDate = regimenDTO.EndDate,
                MedicationId = regimenDTO.MedicationId,
                UserId = regimenDTO.UserId,
                Shedule = regimenDTO.Shedule,
                StartDate = regimenDTO.StartDate,
            };
        }

        public static IEnumerable<RegimenDTO> ToRegimenDTO(this IEnumerable<Regimen> regimens)
        {
            var result = new List<RegimenDTO>();

            foreach (var regimen in regimens)
            {
                result.Add(ToRegimenDTO(regimen));
            }
            return result;
        }
    }
}
