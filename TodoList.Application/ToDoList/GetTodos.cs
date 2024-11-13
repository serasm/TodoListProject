using MediatR;
using TodoList.Application.Repositories;
using TodoList.Application.Services;
using TodoList.Core.Models;

namespace TodoList.Application.ToDoList;

public class GetTodosRequest : IRequest<List<Todo>>
{
    
}

public class GetTodosHandler : IRequestHandler<GetTodosRequest, List<Todo>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly ITodosRepository _todosRepository;
    private readonly IHeaderAccessService _headerAccessService;

    public GetTodosHandler(
        IUsersRepository usersRepository,
        ITodosRepository todosRepository,
        IHeaderAccessService headerAccessService)
    {
        _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        _todosRepository = todosRepository ?? throw new ArgumentNullException(nameof(todosRepository));
        _headerAccessService = headerAccessService ?? throw new ArgumentNullException(nameof(headerAccessService));
    }
    
    public async Task<List<Todo>> Handle(GetTodosRequest request, CancellationToken cancellationToken)
    {
        var userId = _headerAccessService.GetUserId();
        
        if(!userId.HasValue)
            throw new Exception($"User not logged in");
        
        var user = await _usersRepository.GetById(userId.Value);
        
        if(user == null)
            throw new Exception($"User not logged in");

        var userTodos = await _todosRepository.GetForUser(user.Id);

        return userTodos.ToList();
    }
}