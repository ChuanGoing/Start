namespace ChuanGoing.Base.Interface.Domain
{
    public interface IVersionSetter
    {
        /// <summary>
        /// 设置版本号
        /// </summary>
        long PersistVersion { set; }
    }
}
