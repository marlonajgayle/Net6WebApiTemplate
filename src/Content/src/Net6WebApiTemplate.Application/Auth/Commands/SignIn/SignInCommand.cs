using MediatR;

namespace Net6WebApiTemplate.Application.Auth.Commands.SignIn
{
    public class SignInCommand : IRequest<AuthResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}