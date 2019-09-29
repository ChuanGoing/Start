using System;

namespace ChuanGoing.Base.Utils
{
    public class GenerateIdHelper

    {
        public static TPrimaryKey NewId<TPrimaryKey>()
        {
            var type = typeof(TPrimaryKey);
            TPrimaryKey t;
            switch (type.FullName)
            {
                //TODO:举例
                case "System.Int32":
                    t = (TPrimaryKey)Convert.ChangeType(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0), typeof(TPrimaryKey));
                    break;
                case "System.Guid":
                    t = (TPrimaryKey)Convert.ChangeType(Guid.NewGuid(), typeof(TPrimaryKey));
                    break;
                case "System.String":
                    t = (TPrimaryKey)Convert.ChangeType(Guid.NewGuid().ToString("N"), typeof(TPrimaryKey));
                    break;
                //...
                default:
                    throw new Exception("主键类型超出定义范围");
            }
            return t;
        }
    }
}
