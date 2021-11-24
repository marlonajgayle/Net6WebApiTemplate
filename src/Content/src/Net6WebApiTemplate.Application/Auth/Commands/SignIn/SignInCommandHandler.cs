using MediatR;

namespace Net6WebApiTemplate.Application.Auth.Commands.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, AuthResult>
    {
        public SignInCommandHandler()
        {

        }

        public Task<AuthResult> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            // validate username & password 
            // Throw exception if username & password validation failed
            // Generate JWT token response if validation successful
            // return JWT token response
            throw new NotImplementedException();
        }
    }
}