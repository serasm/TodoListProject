using System.Reflection;
using Autofac;
using Quartz;

namespace TodoList.Infrastructure.Quartz;

public class QuartzModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(x => typeof(IJob).IsAssignableFrom(x)).InstancePerDependency();
    }
}