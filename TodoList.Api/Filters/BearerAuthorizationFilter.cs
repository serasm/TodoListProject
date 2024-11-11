namespace TodoList.Api.Filters;

public class BearerAuthorizationFilter : AbstractAuthorizationFilter
{
    protected override string Role => _authenticationService.AccessRole;

    public BearerAuthorizationFilter() : base() {}
}