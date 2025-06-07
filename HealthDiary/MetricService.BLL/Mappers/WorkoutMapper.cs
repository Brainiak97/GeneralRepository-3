using MetricService.BLL.DTO.Workout;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class WorkoutMapper
    {       
        public static Workout ToWorkout(this WorkoutCreateDTO workoutCreate)
        {
            return new Workout()
            {
                Description = workoutCreate.Description,
                UserId = workoutCreate.UserId,
                StartTime = workoutCreate.StartTime,
                PhysicalActivityId = workoutCreate.PhysicalActivityId,
                EndTime = workoutCreate.EndTime,
                Id = 0
            };
        }

      
        public static Workout ToWorkout(this WorkoutUpdateDTO workoutUpdateDTO, int userId)
        {
            return new Workout()
            {
                Id = workoutUpdateDTO.Id,
                Description = workoutUpdateDTO.Description,
                EndTime = workoutUpdateDTO.EndTime,
                PhysicalActivityId = workoutUpdateDTO.PhysicalActivityId,
                StartTime = workoutUpdateDTO.StartTime,
                UserId = userId,                
            };
        }

        public static WorkoutDTO ToWorkoutDTO(this Workout workout)
        {
            return new WorkoutDTO
            {                
                CaloriesBurned= workout.CaloriesBurned,
                Description = workout.Description,
                EndTime = workout.EndTime,
                Id = workout.Id,
                PhysicalActivityId = workout.PhysicalActivityId,
                StartTime = workout.StartTime,
                UserId = workout.UserId,
            };
        }

      
        public static IEnumerable<WorkoutDTO> ToWorkoutDTO(this IEnumerable<Workout> workout)
        {

            var result = new List<WorkoutDTO>();
            foreach (var workoutItem in workout)
            {
                result.Add(ToWorkoutDTO(workoutItem));
            }

            return result;
        }
    }
}
