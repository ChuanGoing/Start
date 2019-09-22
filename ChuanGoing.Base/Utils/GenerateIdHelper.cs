namespace ChuanGoing.Base.Utils
{
    public class GenerateIdHelper

    {
        public static TPrimaryKey NewId<TPrimaryKey>()
        {
            var type = typeof(TPrimaryKey);
            TPrimaryKey t = default;
            switch (type)
            {
                //TODO:
            }
            return t;
        }
    }
}
