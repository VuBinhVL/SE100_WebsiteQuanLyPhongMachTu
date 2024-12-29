using Microsoft.AspNetCore.Http;
using PhongMachTu.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.MyMiddlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenStore _tokenStore;

        public TokenValidationMiddleware(RequestDelegate next, TokenStore tokenStore)
        {
            _next = next;
            _tokenStore = tokenStore;
        }

        public async Task Invoke(HttpContext context, TokenStore tokenStore)
        {
            if(context.User.Identity?.IsAuthenticated==false)
            {
                await _next(context);
                return;
            }
            try
            {
                var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var userId = int.Parse(context.User.FindFirst("UserId").Value); // Lấy UserId từ JWT claims
                if (!tokenStore.IsTokenValid(token, userId))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            await _next(context);
        }

    }
}