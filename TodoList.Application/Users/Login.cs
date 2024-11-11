using System.Security.Authentication;
using MediatR;
using TodoList.Application.Repositories;
using TodoList.Application.Services;

namespace TodoList.Application.Users;

public class LoginRequest : IRequest<string>
{
    public readonly string AuthorizationString;

    public LoginRequest(string authorizationString)
    {
        AuthorizationString = authorizationString ?? throw new ArgumentNullException(nameof(authorizationString));
    }
}

public class LoginHandler : IRequestHandler<LoginRequest, string>
{
    private readonly IHeaderAccessService _headerAccessService;
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;
    private readonly IAuthenticationService _authenticationService;

    public LoginHandler(
        IHeaderAccessService headerAccessService,
        IUserRepository userRepository,
        IHashService hashService,
        IAuthenticationService authenticationService)
    {
        _headerAccessService = headerAccessService ?? throw new ArgumentNullException(nameof(headerAccessService));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
    }
    
    public Task<string> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var auth = _headerAccessService.GetBasicAuthorizationHeaderParams(request.AuthorizationString);

        if (auth == null)
            throw new AuthenticationException();

        var user = _userRepository.GetByUsername(auth.Username);

        if (user == null)
            throw new AuthenticationException();

        if (!_hashService.CompareHash(user, auth.Password, user.Password))
            throw new AuthenticationException();

        return Task.FromResult(_authenticationService.GenerateSignedToken(user));
    }
}