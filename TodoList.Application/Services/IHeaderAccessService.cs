using TodoList.Application.Services.Models;

namespace TodoList.Application.Services;

public interface IHeaderAccessService
{
    HeaderUserCredentials GetBasicAuthorizationHeaderParams(string authorizationHeader);
}