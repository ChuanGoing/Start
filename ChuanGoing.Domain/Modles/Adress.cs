namespace ChuanGoing.Domain.Modles
{
    public class Adress
    {
        public string Province { get; }
        public string City { get; }

        public override string ToString()
        {
            return $"{Province}-{City}";
        }
    }
}
