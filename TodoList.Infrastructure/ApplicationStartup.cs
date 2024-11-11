﻿using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using TodoList.Infrastructure.Database;
using TodoList.Infrastructure.Quartz;
using TodoList.Infrastructure.Quartz.Jobs;
using TodoList.Infrastructure.Services;

namespace TodoList.Infrastructure;

public static class ApplicationStartup
{
    public static void Initialize(
        this ContainerBuilder containerBuilder,
        AuthenticationConfig authConfig,
        string connectionString,
        bool runQuartz = true)
    {
        if (runQuartz)
        {
            StartQuartz(connectionString);
        }

        containerBuilder.CreateAutofacServiceProvider(authConfig);
    }
    
    private static void CreateAutofacServiceProvider(
            this ContainerBuilder container,
            AuthenticationConfig authConfig)
    {
        container.RegisterModule(new ServicesModule(authConfig));
    }

    private static void StartQuartz(
        string connectionString)
    {
        var schedulerFactory = new StdSchedulerFactory();
        var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

        var container = new ContainerBuilder();

        container.RegisterModule(new QuartzModule());

        container.Register(c =>
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<TodoContext>();
            dbContextOptionsBuilder.UseNpgsql(connectionString);

            return new TodoContext(dbContextOptionsBuilder.Options);
        }).AsSelf().InstancePerLifetimeScope();

        scheduler.JobFactory = new JobFactory(container.Build());

        scheduler.Start().GetAwaiter().GetResult();

        var processOutboxJob = JobBuilder.Create<UserNotificationJob>().Build();
        var trigger =
            TriggerBuilder
                .Create()
                .StartNow()
                .WithCronSchedule("0/15 * * ? * *")
                .Build();

        scheduler.ScheduleJob(processOutboxJob, trigger).GetAwaiter().GetResult();
    }
}