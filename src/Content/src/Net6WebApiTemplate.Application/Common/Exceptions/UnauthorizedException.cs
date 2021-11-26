namespace Net6WebApiTemplate.Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) :
            base(message)
        {

        }
    }
}