namespace StateService.Domain.Models
{
    public class UserHealthSummary
    {
        public int UserId { get; set; }
        public double CurrentWeight { get; set; }
        public double TargetWeight { get; set; }
        public double BMI => CurrentWeight / (Height / 100.0 * Height / 100.0);
        public int Height { get; set; }
        public int CaloriesConsumed { get; set; }
        public int CaloriesBurned { get; set; }
        public int CaloriesLeftToday => Math.Max(0, TDEE - CaloriesConsumed);
        public int TDEE { get; set; } // Total Daily Energy Expenditure
        public int Steps { get; set; }
        public int SleepMinutes { get; set; }
        public List<string> Recommendations { get; set; } = [];
    }
}
