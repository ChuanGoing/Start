namespace ChuanGoing.Domain.Modles
{
    public class Adress
    {
        public string Province { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return $"{Province}-{City}";
        }
    }
}
