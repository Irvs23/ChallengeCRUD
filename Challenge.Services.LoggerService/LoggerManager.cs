using Microsoft.Extensions.Configuration;
using Challenge.Core.Contracts;
using Serilog;
using Microsoft.Extensions.Configuration;


namespace Challenge.Services.LoggerService;

public class LoggerManager : ILoggerManager
{
    private readonly IConfiguration _configuration;
    private static ILogger? logger;

    public LoggerManager(IConfiguration Config)
    {
        _configuration = Config;
        logger = new LoggerConfiguration()
            .ReadFrom.Configuration(_configuration)
            .CreateLogger();
    }

    public void LogDebug(string message) => logger.Debug(message);

    public void LogError(string message) => logger.Error(message);

    public void LogInfo(string message) => logger.Information(message);

    public void LogWarn(string message) => logger.Warning(message);
}