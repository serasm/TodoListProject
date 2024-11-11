namespace TodoList.Application.Services.Models;

public class HeaderUserCredentials
{
    public readonly string Username;
    public readonly string Password;
    
    public HeaderUserCredentials(string username, string password)
    {
        Username = username;
        Password = password;
    }
}