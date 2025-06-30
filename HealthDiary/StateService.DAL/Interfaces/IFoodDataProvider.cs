namespace StateService.DAL.Interfaces
{
    public interface IFoodDataProvider
    {
        Task<object> GetFoodDataAsync(string userId);
    }
}
