namespace BlogAPI.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string context) : base(context)
        {

        }
    }
}
