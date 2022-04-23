namespace Net6WebApiTemplate.Domain.Exceptions
{
    public class TrnInvalidException : Exception
    {
        public TrnInvalidException(string trn)
            : base($"TRN format \"{trn}\" is invalid")
        {

        }
    }
}