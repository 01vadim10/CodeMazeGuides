﻿using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MessageTemplateForLoggingCA2254;
public class Application
{
    private readonly ILogger<Application> _logger;

    public Application(ILogger<Application> logger)
    {
        _logger = logger;
    }

    public void Run()
    {
        var userName = "John Doe";
        var loggedInTime = DateTime.Now;
        LogMessageWithJson(userName, loggedInTime);
        LogMessageWithSamePlaceholderAndVariableName(userName);
        LogMessageWithDifferentPlaceholderAndVariableName(userName);
        LogMessageWithCA2254Warning(userName, loggedInTime);
        LogMessageToFixCA2254Warning(userName, loggedInTime);
    }

    public void LogMessageWithJson(string userName, DateTime loggedInTime)
    {
        var logEntry = new
        {
            EventId = "logged_in",
            Username = userName,
            Time = loggedInTime,
        };

        var message = JsonSerializer.Serialize(logEntry);
        _logger.Log(LogLevel.Information, message);
    }

    public void LogMessageWithSamePlaceholderAndVariableName(string userName)
    {
        _logger.Log(LogLevel.Information, "User '{userName}' added apples to the basket.", userName);
    }
    
    public void LogMessageWithDifferentPlaceholderAndVariableName(string randomSentence)
    {
        _logger.Log(LogLevel.Information, "User '{name}' added apples to the basket.", randomSentence);
    }
    
    public void LogMessageWithCA2254Warning(string userName, DateTime loggedInTime)
    {
        _logger.Log(LogLevel.Information, $"User {userName} logged on {loggedInTime}");
    }
    
    public void LogMessageToFixCA2254Warning(string userName, DateTime loggedInTime)
    {
        _logger.Log(LogLevel.Information, "User {userName} logged on {loggedInTime}", userName, loggedInTime);
    }
}
