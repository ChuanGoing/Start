using ChuanGoing.Base.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;

namespace ChuanGoing.Web.API
{
    public static class HttpContextExtension
    {
        public static Guid GetId(this HttpContext httpContext)
        {
            var principal = httpContext.User;
            if (principal.Identity.IsAuthenticated)
            {
                var subClaim = principal.Claims.FirstOrDefault(claim => claim.Type == "sub");
                if (subClaim != null)
                {
                    //TODO:此处必须由内部系统做验证,判断是否为内部系统认证用户
                    return new Guid(subClaim.Value);
                }
            }
            throw new InnerException((int)HttpStatusCode.Unauthorized);
        }
    }
}
