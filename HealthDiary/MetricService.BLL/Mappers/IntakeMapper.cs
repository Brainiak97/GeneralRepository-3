using MetricService.BLL.DTO.Intake;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class IntakeMapper
    {
        public static IntakeCreateDTO ToIntakeCreateDTO(this Intake intake)
        {
            return new IntakeCreateDTO
            {
                Comment = intake.Comment,
                IntakeStatus=intake.IntakeStatus,
                RegimenId=intake.RegimenId,
                TakenAt = intake.TakenAt,
            };
        }

        public static Intake ToIntake(this IntakeCreateDTO intakeCreateDTO)
        {
            return new Intake
            {
                TakenAt = intakeCreateDTO.TakenAt,
                RegimenId = intakeCreateDTO.RegimenId,
                IntakeStatus = intakeCreateDTO.IntakeStatus,
                Comment = intakeCreateDTO.Comment,
                Id = 0,                
            };
        }

        public static IntakeUpdateDTO ToIntakeUpdateDTO(this Intake intake)
        {
            return new IntakeUpdateDTO
            {
               Id = intake.Id,
               Comment = intake.Comment,
               IntakeStatus= intake.IntakeStatus,   
               TakenAt = intake.TakenAt               
            };
        }

        public static Intake ToIntake(this IntakeUpdateDTO intakeUpdateDTO, int regimenId)
        {
            return new Intake
            {
               Comment = intakeUpdateDTO.Comment,
               Id = intakeUpdateDTO.Id,
               TakenAt= intakeUpdateDTO.TakenAt,
               IntakeStatus=intakeUpdateDTO.IntakeStatus,
               RegimenId=regimenId,
            };
        }

        public static IntakeDTO ToIntakeDTO(this Intake intake)
        {
            return new IntakeDTO
            {
                Comment= intake.Comment,
                Id = intake.Id,
                RegimenId = intake.RegimenId,
                IntakeStatus = intake.IntakeStatus,
                TakenAt = intake.TakenAt,                
            };
        }

        public static Intake ToIntake(this IntakeDTO intakeDTO)
        {
            return new Intake
            {
               Comment=intakeDTO.Comment,
               Id = intakeDTO.Id,
               IntakeStatus=intakeDTO.IntakeStatus,
               RegimenId=intakeDTO.RegimenId,
               TakenAt= intakeDTO.TakenAt
            };
        }

        public static IEnumerable<IntakeDTO> ToIntakeDTO(this IEnumerable<Intake> intakes)
        {
            var result = new List<IntakeDTO>();

            foreach (var intake in intakes)
            {
                result.Add(ToIntakeDTO(intake));
            }
            return result;
        }
    }
}
