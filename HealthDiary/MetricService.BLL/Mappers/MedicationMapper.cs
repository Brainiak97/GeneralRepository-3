using MetricService.BLL.DTO.Intake;
using MetricService.BLL.DTO.MedicationDTO;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class MedicationMapper
    {
        public static MedicationCreateDTO ToMedicationCreateDTO(this Medication medication)
        {
            return new MedicationCreateDTO
            {
                DosageFormId = medication.DosageFormId,
                Instruction = medication.Instruction,
                Name = medication.Name,
            };
        }

        public static Medication ToMedication(this MedicationCreateDTO medicationCreateDTO)
        {
            return new Medication
            {
                Name = medicationCreateDTO.Name,
                Instruction = medicationCreateDTO.Instruction,
                DosageFormId = medicationCreateDTO.DosageFormId,
                Id = 0
            };
        }

        public static MedicationUpdateDTO ToMedicationUpdateDTO(this Medication medication)
        {
            return new MedicationUpdateDTO
            {
                Id = medication.Id,
                Instruction = medication.Instruction,
                Name = medication.Name,
            };
        }

        public static Medication ToMedication(this MedicationUpdateDTO medicationUpdateDTO, int dosageFormId)
        {
            return new Medication
            {
                Name= medicationUpdateDTO.Name,
                Id = medicationUpdateDTO.Id,    
                Instruction= medicationUpdateDTO.Instruction,
                DosageFormId= dosageFormId,
            };
        }

        public static MedicationDTO ToMedicationDTO(this Medication medication)
        {
            return new MedicationDTO
            {
                DosageFormId= medication.DosageFormId,
                Id = medication.Id,
                Instruction=medication.Instruction,
                Name=medication.Name,
            };
        }

        public static Medication ToMedication(this MedicationDTO medicationDTO)
        {
            return new Medication
            {
                Name = medicationDTO.Name,
                Id = medicationDTO.Id,
                DosageFormId=medicationDTO.DosageFormId,
                Instruction=medicationDTO.Instruction,
            };
        }

        public static IEnumerable<MedicationDTO> ToMedicationDTO(this IEnumerable<Medication> medications)
        {
            var result = new List<MedicationDTO>();

            foreach (var medication in medications)
            {
                result.Add(ToMedicationDTO(medication));
            }
            return result;
        }
    }
}
