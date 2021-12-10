using Net6WebApiTemplate.Application.Common.Models;

namespace Net6WebApiTemplate.Application.Common.Interfaces
{
    public interface ISignInManager
    {
        Task<Result> PasswordSignInAsync(string username, string password, bool isPersistent, bool LockoutOnFailiure);
    }
}