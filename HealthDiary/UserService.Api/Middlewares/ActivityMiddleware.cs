using UserService.BLL.Services;

namespace UserService.Api.Middlewares
{
    public class ActivityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly OnlineUsersService _onlineUsersService;

        public ActivityMiddleware(RequestDelegate next, OnlineUsersService onlineUsersService)
        {
            _next = next;
            _onlineUsersService = onlineUsersService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userId = int.Parse(context.User.FindFirst("sub")!.Value);
                var role = context.User.FindFirst("role")!.Value;

                _onlineUsersService.UpdateActivity(userId, role);
            }

            await _next(context);
        }
    }
}
