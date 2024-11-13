using Autofac;

namespace TodoList.Infrastructure.Processing;

public class ProcessingModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RequestExecutor>().As<IRequestExecutor>();
    }
}