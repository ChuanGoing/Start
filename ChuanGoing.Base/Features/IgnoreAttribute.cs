using System;

namespace ChuanGoing.Base.Features
{
    /// <summary>
    /// 数据行为忽略特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IgnoreAttribute : Attribute
    {
    }
}
