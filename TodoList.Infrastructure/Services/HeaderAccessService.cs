﻿using System.Text;
using TodoList.Application.Services;
using TodoList.Application.Services.Models;

namespace TodoList.Infrastructure.Services;

public class HeaderAccessService : IHeaderAccessService
{
    private readonly AuthenticationConfig _authenticationConfig;

    public HeaderAccessService(AuthenticationConfig authenticationConfig)
    {
        _authenticationConfig = authenticationConfig ?? throw new ArgumentNullException(nameof(authenticationConfig));
    }
    
    public HeaderUserCredentials GetBasicAuthorizationHeaderParams(string authorizationHeader)
    {
        if (string.IsNullOrWhiteSpace(authorizationHeader) ||
            !authorizationHeader.Contains(_authenticationConfig.BasicAuthScheme, StringComparison.OrdinalIgnoreCase))
            return null;
        
        var value = authorizationHeader.Replace(_authenticationConfig.BasicAuthScheme, string.Empty, StringComparison.OrdinalIgnoreCase).Trim();
        
        var encoding = Encoding.GetEncoding("iso-8859-1");
        var credentials = encoding.GetString(Convert.FromBase64String(value));
        
        var loginData = credentials.Split(':');
        return new HeaderUserCredentials(loginData[0], loginData[1]);
    }
}