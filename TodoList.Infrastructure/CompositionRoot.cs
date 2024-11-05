using Autofac;

namespace TodoList.Infrastructure;

internal interface ICompositionRoot
{
    ILifetimeScope BeginLifetimeScope();
}

internal class CompositionRoot : ICompositionRoot
{
    private ILifetimeScope _lifetimeScope;

    public CompositionRoot(ILifetimeScope lifetimeScope) => this._lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));

    public ILifetimeScope BeginLifetimeScope() => this._lifetimeScope.BeginLifetimeScope();
}