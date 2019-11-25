using System;

namespace ChuanGoing.Web.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermissionAttribute : Attribute
    {
        /// <summary>
        /// 权限代码
        /// </summary>
        public string Code { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">权限代码</param>
        public PermissionAttribute(string code)
        {
            Code = code;
        }
    }
}
