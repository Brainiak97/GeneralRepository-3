using MetricService.BLL.DTO.PhysicalActivity;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class PhysicalActivityMapper
    {
        public static PhysicalActivityCreateDTO ToPhysicalActivityCreateDTO(this PhysicalActivity physicalActivity)
        {
            return new PhysicalActivityCreateDTO
            {
                Name = physicalActivity.Name,
                EnergyEquivalent = physicalActivity.EnergyEquivalent,
            };
        }

        public static PhysicalActivity ToPhysicalActivity(this PhysicalActivityCreateDTO physicalActivityCreateDTO)
        {
            return new PhysicalActivity
            {
                EnergyEquivalent = physicalActivityCreateDTO.EnergyEquivalent,
                Name = physicalActivityCreateDTO.Name,
                Id = 0
            };
        }

        public static PhysicalActivityUpdateDTO ToPhysicalActivityUpdateDTO(this PhysicalActivity physicalActivity)
        {
            return new PhysicalActivityUpdateDTO
            {
                Id = physicalActivity.Id,
                Name = physicalActivity.Name,
                EnergyEquivalent = physicalActivity.EnergyEquivalent,
            };
        }

        public static PhysicalActivity ToPhysicalActivity(this PhysicalActivityUpdateDTO physicalActivityUpdateDTO)
        {
            return new PhysicalActivity
            {
                EnergyEquivalent = physicalActivityUpdateDTO.EnergyEquivalent,
                Name = physicalActivityUpdateDTO.Name,
                Id = physicalActivityUpdateDTO.Id
            };
        }

        public static PhysicalActivityDTO ToPhysicalActivityDTO(this PhysicalActivity physicalActivity)
        {
            return new PhysicalActivityDTO
            {
                EnergyEquivalent= physicalActivity.EnergyEquivalent,
                Name = physicalActivity.Name,
                Id = physicalActivity.Id
            };
        }

        public static PhysicalActivity ToPhysicalActivity(this PhysicalActivityDTO physicalActivityDTO)
        {
            return new PhysicalActivity
            {
               EnergyEquivalent=physicalActivityDTO.EnergyEquivalent,
               Name = physicalActivityDTO.Name,
               Id = physicalActivityDTO.Id
            };
        }

        public static IEnumerable<PhysicalActivityDTO> ToPhysicalActivityDTO(this IEnumerable<PhysicalActivity> physicalActivities)
        {
            var result = new List<PhysicalActivityDTO>();

            foreach (var physicalActivity in physicalActivities)
            {
                result.Add(ToPhysicalActivityDTO(physicalActivity));
            }
            return result;
        }
    }
}
