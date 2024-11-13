using System.Security.Claims;
using TodoList.Infrastructure.Services;

namespace TodoList.Api.Services;

public class HttpContextAccessorWrapper : IHttpContextAccessorWrapper
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public HttpContextAccessorWrapper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public int? UserId
    {
        get
        {
            var stringValue = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if(string.IsNullOrWhiteSpace(stringValue))
                return null;
            
            var result = int.TryParse(stringValue, out var intValue);
            
            if(result)
                return intValue;
            
            return null;
        }
    }
}