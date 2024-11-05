using Autofac;
using MediatR;

namespace TodoList.Infrastructure.Processing;

internal interface IRequestExecutor
{
    Task Execute(IRequest command);
    Task<TResult> Execute<TResult>(IRequest<TResult> command);
}

internal class RequestExecutor : IRequestExecutor
{
    private ICompositionRoot _compositionRoot;

    public RequestExecutor(
        ICompositionRoot compositionRoot
    )
    {
        this._compositionRoot = compositionRoot;
    }

    public async Task Execute(IRequest command)
    {
        using (var scope = this._compositionRoot.BeginLifetimeScope())
        {
            var mediator = scope.Resolve<IMediator>();
            await mediator.Send(command);
        }
    }

    public async Task<TResult> Execute<TResult>(IRequest<TResult> command)
    {
        using (var scope = this._compositionRoot.BeginLifetimeScope())
        {
            var mediator = scope.Resolve<IMediator>();
            return await mediator.Send(command);
        }
    }
}