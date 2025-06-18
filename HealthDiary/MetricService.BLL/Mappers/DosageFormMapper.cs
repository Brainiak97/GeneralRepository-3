using MetricService.BLL.DTO.DosageForm;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class DosageFormMapper
    {
        public static DosageFormCreateDTO ToDosageFormCreateDTO(this DosageForm dosageForm)
        {
            return new DosageFormCreateDTO
            {
                Name=dosageForm.Name,                
            };
        }

        public static DosageForm ToDosageForm(this DosageFormCreateDTO dosageFormCreateDTO)
        {
            return new DosageForm
            {
                Id = 0,
                Name = dosageFormCreateDTO.Name,
            };
        }

        public static DosageFormUpdateDTO ToDosageFormUpdateDTO(this DosageForm dosageForm)
        {
            return new DosageFormUpdateDTO
            {
               Id=dosageForm.Id,
               Name=dosageForm.Name,
            };
        }

        public static DosageForm ToDosageForm(this DosageFormUpdateDTO dosageFormUpdateDTO)
        {
            return new DosageForm
            {
                Id=dosageFormUpdateDTO.Id,
                Name=dosageFormUpdateDTO.Name,
            };
        }

        public static DosageFormDTO ToDosageFormDTO(this DosageForm dosageForm)
        {
            return new DosageFormDTO
            {
                Id=dosageForm.Id,
                Name=dosageForm.Name,
            };
        }

        public static DosageForm ToDosageForm(this DosageFormDTO dosageFormDTO)
        {
            return new DosageForm
            {
               Id=dosageFormDTO.Id,
               Name=dosageFormDTO.Name,
            };
        }

        public static IEnumerable<DosageFormDTO> ToAnalysisCategoryDTO(this IEnumerable<DosageForm> dosageForms)
        {
            var result = new List<DosageFormDTO>();

            foreach (var dosageForm in dosageForms)
            {
                result.Add(ToDosageFormDTO(dosageForm));
            }
            return result;
        }
    }
}
