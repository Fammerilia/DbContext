namespace WebApplication2.GlobalException
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}

