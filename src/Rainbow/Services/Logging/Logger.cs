using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Rainbow.Services.Logging;

public class Logger
{
    public Task Verbose(string source, string message, Exception exception = null)
    {
        return LogAsync(new LogMessage(LogSeverity.Verbose, source, message, exception));
    }

    public Task Debug(string source, string message, Exception exception = null)
    {
        return LogAsync(new LogMessage(LogSeverity.Debug, source, message, exception));
    }

    public Task Info(string source, string message, Exception exception = null)
    {
        return LogAsync(new LogMessage(LogSeverity.Info, source, message, exception));
    }

    public Task Warn(string source, string message, Exception exception = null)
    {
        return LogAsync(new LogMessage(LogSeverity.Warning, source, message, exception));
    }

    public Task Error(string source, string message, Exception exception = null)
    {
        return LogAsync(new LogMessage(LogSeverity.Error, source, message, exception));
    }

    public Task LogAsync(LogMessage msg)
    {
        if (msg.Exception is CommandException cmdException)
        {
            Console.WriteLine($"{cmdException.Context.User} failed to execute '{cmdException.Command.Name}' in {cmdException.Context.Channel}.");
            Console.WriteLine(cmdException.ToString());
        }
        else
        {
            Console.WriteLine(msg.ToString());
        }
        
        return Task.CompletedTask;
    }
}