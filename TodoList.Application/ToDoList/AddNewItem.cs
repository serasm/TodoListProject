using MediatR;
using TodoList.Application.Repositories;
using TodoList.Application.Services;
using TodoList.Core.Models;

namespace TodoList.Application.ToDoList;

public class AddNewItemRequest : IRequest
{
    public string Description { get; set; }
    public string Title { get; set; }
    public DateTime? DueDate { get; set; }
}

public class AddNewItemHandler : IRequestHandler<AddNewItemRequest>
{
    private readonly IUsersRepository _usersRepository;
    private readonly ITodosRepository _todosRepository;
    private readonly IHeaderAccessService _headerAccessService;
    
    public AddNewItemHandler(IUsersRepository usersRepository, ITodosRepository todosRepository, IHeaderAccessService headerAccessService)
    {
        _todosRepository = todosRepository ?? throw new ArgumentNullException(nameof(todosRepository));
        _headerAccessService = headerAccessService ?? throw new ArgumentNullException(nameof(headerAccessService));
    }
    
    public async Task Handle(AddNewItemRequest request, CancellationToken cancellationToken)
    {
        var userId = _headerAccessService.GetUserId();
        
        if(!userId.HasValue)
            throw new Exception($"User not logged in");
        
        var user = await _usersRepository.GetById(userId.Value);
        
        if(user == null)
            throw new Exception($"User not logged in");
        
        var newItem = new Todo()
        {
            OwnerId = user.Id,
            IsDone = false,
            Description = request.Description,
            Title = request.Title,
            DueDate = request.DueDate
        };

        await _todosRepository.Create(newItem);
    }
}