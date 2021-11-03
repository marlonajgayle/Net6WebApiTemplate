using Net6WebApiTemplate.Domain.Exceptions;
using ValueOf;

namespace Net6WebApiTemplate.Domain.ValueObjects
{
    public class Trn : ValueOf<string, Trn>
    {
        private const int RequiredLength = 8;

        protected override void Validate()
        {
           if (Value.Length != RequiredLength)
           {
                throw new TrnInvalidException(Value);
           }

           if (!int.TryParse(Value, out _))
           {
                throw new TrnInvalidException(Value);
            }
        }
    }
}