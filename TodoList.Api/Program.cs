using Autofac;
using Autofac.Extensions.DependencyInjection;
using TodoList.Api.Services;
using TodoList.Infrastructure;
using TodoList.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.Initialize(builder.Configuration.GetSection("AuthenticationConfig").Get<AuthenticationConfig>(), builder.Configuration.GetConnectionString("DefaultConnection"), false);
        containerBuilder.RegisterType<HttpContextAccessorWrapper>().As<IHttpContextAccessorWrapper>()
            .InstancePerLifetimeScope();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();