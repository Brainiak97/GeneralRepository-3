using MetricService.BLL.DTO.Workout;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class WorkoutMapper
    {
        public static WorkoutCreateDTO ToWorkoutCreateDTO(this Workout workout)
        {
            return new WorkoutCreateDTO
            {
                
                CaloriesBurned = workout.CaloriesBurned,
                Description = workout.Description,
                EndTime = workout.EndTime,
                PhysicalActivityId = workout.PhysicalActivityId,
                StartTime = workout.StartTime,
                UserId = workout.UserId,
            };
        }

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

        public static WorkoutUpdateDTO ToWorkoutUpdateDTO(this Workout workout)
        {
            return new WorkoutUpdateDTO
            {
                StartTime = workout.StartTime,
                EndTime = workout.EndTime,
                CaloriesBurned=workout.CaloriesBurned,
                Description = workout.Description,
                Id = workout.Id,
                PhysicalActivityId = workout.PhysicalActivityId,
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

        public static Workout ToWorkout(this WorkoutDTO workoutDTO)
        {
            return new Workout
            {                    
                UserId = workoutDTO.UserId,
                PhysicalActivityId = workoutDTO.PhysicalActivityId,
                EndTime = workoutDTO.EndTime,
                Description = workoutDTO.Description,
                Id = workoutDTO.Id,
                StartTime = workoutDTO.StartTime,
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
