namespace TodoList.Infrastructure.Services;

public interface IHttpContextAccessorWrapper
{
    int? UserId { get; }
}