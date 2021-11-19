using Microsoft.AspNetCore.DataProtection;
using Net6WebApiTemplate.Application.Common.Enums;
using Net6WebApiTemplate.Application.Common.Interfaces;

namespace Net6WebApiTemplate.Infrastructure.DataProtection
{
    public class RouteDataProtection : IDataEncryption
    {
        private readonly IDataProtector _dataProtector;

        public RouteDataProtection(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtector = dataProtectionProvider.CreateProtector(DataProtectionPurpose.RouteValues.ToString());
        }

        public string Encrypt(string plainInput)
        {
            return _dataProtector.Protect(plainInput);
        }

        public string Decrypt(string cipherText)
        {
            return _dataProtector.Unprotect(cipherText);
        }
    }
}