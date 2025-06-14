namespace MetricService.BLL.DTO.Workout
{
    public  class WorkoutDTO:WorkoutBaseDTO
    {
        /// <summary>
        /// идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// пользователя
        /// </summary>        
        public int UserId { get; set; }
    }
}
