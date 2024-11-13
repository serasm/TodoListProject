using Autofac;
using TodoList.Application.Services;

namespace TodoList.Infrastructure.Services;

public class ServicesModule : Autofac.Module
{
    private readonly AuthenticationConfig _authenticationConfig;
    
    public ServicesModule(AuthenticationConfig authConfig)
    {
        _authenticationConfig = authConfig ?? throw new ArgumentNullException(nameof(authConfig));
    }
    
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<HeaderAccessService>().As<IHeaderAccessService>()
            .InstancePerLifetimeScope();
        builder.RegisterType<HashService>().As<IHashService>()
            .InstancePerLifetimeScope();
        builder.RegisterType<AuthenticationService>().As<IAuthenticationService>()
            .InstancePerLifetimeScope();
        builder.RegisterInstance(_authenticationConfig);
    }
}