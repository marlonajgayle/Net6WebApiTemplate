using MediatR;
using Net6WebApiTemplate.Application.Common.Exceptions;
using Net6WebApiTemplate.Application.Common.Interfaces;

namespace Net6WebApiTemplate.Application.Auth.Commands.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, AuthResult>
    {
        private readonly ISignInManager _signInManager;
        private readonly IJwtTokenManager _jwtTokenManager;

        public SignInCommandHandler(ISignInManager signInManager, IJwtTokenManager jwtTokenManager)
        {
            _signInManager = signInManager;
            _jwtTokenManager = jwtTokenManager;
        }

        public async Task<AuthResult> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            // validate username & password 
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            // Throw exception if credential validation failed
            if (!result.Succeeded)
            {
                throw new UnauthorizedException("Invalid username or password.");
            }

            // Generate JWT token response if validation successful
            AuthResult response = await _jwtTokenManager.GenerateClaimsTokenAsync(request.Username, cancellationToken);

            return response;
        }
    }
}