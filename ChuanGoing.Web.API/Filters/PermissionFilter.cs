using ChuanGoing.Base.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace ChuanGoing.Web.API.Filters
{
    public class PermissionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                //TODO:用户自定义权限验证
                Guid userId = context.HttpContext.GetId();
                bool right;
                #region 自定义权限验证
                //根据userId判断用户内部系统权限信息

                //var userPermissions = repo.GetUserPermissions(userId);
                //var permissions = repo.GetPermissions();
                var metas = context.ActionDescriptor.EndpointMetadata;
                foreach (var meta in metas)
                {
                    if (meta is PermissionAttribute permission)
                    {
                        //if (!permissions.Any(p => permission.Code.Any(c => c == p.Code))
                        //    && !userPermissions.Any(p => permission.Code.Any(c => c == p.Code)))
                        //{
                        //    throw new WebException(HttpStatusCode.Forbidden, MessageCodes.AccessDenied, "你没有访问该资源的权限");
                        //}
                        //break;
                    }
                }

                right = false;
                #endregion
                if (!right)
                {
                    context.Result = new ContentResult() { StatusCode = (int)HttpStatusCode.Forbidden, Content = "你没有访问该资源的权限" };
                }

            }
        }
    }
}
