using StateService.BLL.Interfaces;
using StateService.DAL.Interfaces;
using StateService.Domain.Models;

namespace StateService.BLL.Services
{
    public class StateService(
        IUserDataProvider userDataProvider,
        IMetricDataProvider metricDataProvider,
        IFoodDataProvider foodDataProvider) : IStateService
    {
        private readonly IUserDataProvider _userDataProvider = userDataProvider;
        private readonly IMetricDataProvider _metricDataProvider = metricDataProvider;
        private readonly IFoodDataProvider _foodDataProvider = foodDataProvider;

        public async Task<UserHealthSummary> GetDailySummaryAsync(int userId)
        {
            return new UserHealthSummary
            {
                UserId = userId,
                //CurrentWeight = user.Weight,
                //TargetWeight = user.TargetWeight,
                //Height = user.Height,
                //CaloriesConsumed = nutrition.CaloriesConsumed,
                //CaloriesBurned = activity.CaloriesBurned,
                //TDEE = CalculateTDEE(user),
                //Steps = activity.Steps,
                //SleepMinutes = activity.SleepMinutes,
                //Recommendations = GenerateRecommendations(nutrition, activity)
            };
        }
    }
}
