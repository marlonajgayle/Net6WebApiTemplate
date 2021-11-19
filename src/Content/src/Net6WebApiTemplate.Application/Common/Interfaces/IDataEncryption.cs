namespace Net6WebApiTemplate.Application.Common.Interfaces
{
    public interface IDataEncryption
    {
        string Encrypt(string plainInput);
        string Decrypt(string cipherText);
    }
}