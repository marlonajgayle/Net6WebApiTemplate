using Net6WebApiTemplate.Domain.Common;

namespace Net6WebApiTemplate.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string Parish { get; set; }

        private Address()
        {
        }

        public Address(string addressLine1, string addressLine2, string parish)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            Parish = parish;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AddressLine1;
            yield return AddressLine2;
            yield return Parish;
        }
    }
}