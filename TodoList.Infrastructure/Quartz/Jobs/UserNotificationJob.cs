using MediatR;
using Quartz;
using Serilog;
using TodoList.Infrastructure.Processing;

namespace TodoList.Infrastructure.Quartz.Jobs;

public class UserNotificationJob : IJob
{
    private readonly IRequestExecutor _requestExecutor;
    private readonly ILogger _logger;
    
    public UserNotificationJob(IRequestExecutor requestExecutor, ILogger logger)
    {
        _requestExecutor = requestExecutor ?? throw new ArgumentNullException(nameof(requestExecutor));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public Task Execute(IJobExecutionContext context)
    {
        //return _requestExecutor.Execute();
        return Task.CompletedTask;
    }
}