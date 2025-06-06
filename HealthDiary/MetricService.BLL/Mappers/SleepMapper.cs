using MetricService.BLL.DTO.Sleep;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class SleepMapper
    {
        public static SleepCreateDTO ToSleepCreateDTO(this Sleep sleep)
        {
            return new SleepCreateDTO()
            {
                Comment = sleep.Comment,
                EndSleep = sleep.EndSleep,
                QualityRating = sleep.QualityRating,
                SleepDuration = sleep.SleepDuration,
                StartSleep = sleep.StartSleep,
                UserId = sleep.UserId,
            };
        }

        public static Sleep ToSleep(this SleepCreateDTO sleepDTO)
        {
            return new Sleep()
            {
                UserId = sleepDTO.UserId,
                Comment = sleepDTO.Comment,
                EndSleep = sleepDTO.EndSleep,
                QualityRating = sleepDTO.QualityRating,
                StartSleep = sleepDTO.StartSleep,
                Id = 0
            };
        }

        public static SleepUpdateDTO ToSleepUpdateDTO(this Sleep sleep)
        {
            return new SleepUpdateDTO()
            {
                Comment = sleep.Comment,
                EndSleep = sleep.EndSleep,
                QualityRating = sleep.QualityRating,
                SleepDuration = sleep.SleepDuration,
                StartSleep = sleep.StartSleep,
                Id = sleep.Id
            };
        }

        public static Sleep ToSleep(this SleepUpdateDTO sleepDTO, int userId)
        {
            return new Sleep()
            {
                UserId = userId,
                Comment = sleepDTO.Comment,
                EndSleep = sleepDTO.EndSleep,
                QualityRating = sleepDTO.QualityRating,
                StartSleep = sleepDTO.StartSleep,
                Id = sleepDTO.Id,
            };
        }

        public static SleepDTO ToSleepDTO(this Sleep sleep)
        {
            return new SleepDTO()
            {
                Comment = sleep.Comment,
                EndSleep = sleep.EndSleep,
                Id = sleep.Id,
                QualityRating = sleep.QualityRating,
                StartSleep = sleep.StartSleep,
                SleepDuration = sleep.SleepDuration,
                UserId = sleep.UserId
            };
        }

        public static IEnumerable<SleepDTO> ToSleepDTO(this IEnumerable<Sleep> sleep)
        {
            var result =new List<SleepDTO>();
            foreach (var item in sleep) {
                result.Add(ToSleepDTO(item));
            }
            return result;
        }

        public static Sleep ToSleep(this SleepDTO sleepDTO)
        {
            return new Sleep()
            {
                Comment = sleepDTO.Comment,
                EndSleep = sleepDTO.EndSleep,
                Id = sleepDTO.Id,
                QualityRating = sleepDTO.QualityRating,
                StartSleep = sleepDTO.StartSleep,
                UserId = sleepDTO.UserId
            };
        }
    }
}
