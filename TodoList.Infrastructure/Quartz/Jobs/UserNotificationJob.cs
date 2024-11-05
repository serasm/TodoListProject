using MediatR;
using Quartz;
using Serilog;

namespace TodoList.Infrastructure.Quartz.Jobs;

public class UserNotificationJob : IJob
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    
    public UserNotificationJob(IMediator mediator, ILogger logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public Task Execute(IJobExecutionContext context)
    {
        throw new NotImplementedException();
    }
}