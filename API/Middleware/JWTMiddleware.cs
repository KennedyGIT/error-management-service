using core.Entities;
using core.Interfaces;

namespace API.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITokenService tokenService) 
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var username = tokenService.ValidateToken(token);
            if (username != null) 
            {
                var authUser = new UserEntity()
                {
                    Username = username
                };

                context.Items["User"] = authUser;
            }

            await _next(context);
        }
    }
}
