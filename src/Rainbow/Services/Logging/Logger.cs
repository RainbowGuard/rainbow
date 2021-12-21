using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Rainbow.Services.Logging;

public class Logger
{
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