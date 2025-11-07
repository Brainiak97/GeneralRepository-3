namespace UserService.Api.Contracts.Dtos
{
    public class UsersActivity
    {
       public long DoctorsLoggedIn { get; set; }
       public long PatientsLoggedIn { get; set; }
       public long Total { get; set; }
    }
}
