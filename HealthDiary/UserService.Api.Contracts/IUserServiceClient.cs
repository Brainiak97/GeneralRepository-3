using Refit;
using UserService.Api.Contracts.Dtos;

namespace UserService.Api.Contracts
{
    public interface IUserServiceClient
    {
        const string Auth = "/Auth";
        const string User = "/User";

        [Post( $"{Auth}/Login" )]
        Task<AuthResponseDto?> Login( LoginRequestDto request, CancellationToken cancellationToken );

        [Post( $"{Auth}/Register" )]
        Task<AuthResponseDto?> Register( RegisterRequestDto request, CancellationToken cancellationToken );

        [Post( $"{Auth}/RegisterDoctor" )]
        Task<AuthResponseDto?> RegisterDoctor( RegisterRequestDto request, CancellationToken cancellationToken );

        [Post( $"{Auth}/AssignRoleToUser" )]
        Task AssignRoleToUser( string roleName, int userId, CancellationToken cancellationToken );

        [Post( $"{Auth}/GetUsersActivity" )]
        Task<UsersActivity?> GetUsersActivity();

        [Get( $"{User}/GetAllUsers" )]
        Task<IEnumerable<UserDto?>> GetAllUsers( CancellationToken cancellationToken );

        [Get( $"{User}/GetUserInfo" )]
        Task<UserDto?> GetUserInfoAsync( int userId, CancellationToken cancellationToken );

        [Post( $"{User}/SendVerificationEmail" )]
        Task<ResponseMessage> SendVerificationEmail( string email, CancellationToken cancellationToken );

        [Get( $"{User}/VerifyEmail" )]
        Task<ResponseMessage> VerifyEmail( string token, CancellationToken cancellationToken );

        [Post( $"{User}/ForgotPassword" )]
        Task<ResponseMessage> ForgotPassword( string email, CancellationToken cancellationToken );

        [Post( $"{User}/ResetPassword" )]
        Task<ResponseMessage> ResetPassword( ResetPasswordDto dto, CancellationToken cancellationToken );

        // TODO заменить HttpResponseMessage
        [Put( $"{User}/RestoreUser/{{id}}" )]
        Task<HttpResponseMessage> UpdateUser( int id, UserUpdateDto dto, CancellationToken cancellationToken );

        [Put( $"{User}/RestoreUser/{{id}}" )]
        Task<HttpResponseMessage> BlockUser( int id, CancellationToken cancellationToken );

        [Put( $"{User}/RestoreUser/{{id}}" )]
        Task<HttpResponseMessage> UnblockUser( int id, CancellationToken cancellationToken );

        [Delete( $"{User}/RestoreUser/{{id}}" )]
        Task<HttpResponseMessage> DeleteUser( int id, CancellationToken cancellationToken );

        [Delete( $"{User}/RestoreUser/{{id}}" )]
        Task<HttpResponseMessage> RestoreUser( int id, CancellationToken cancellationToken );
    }
}
