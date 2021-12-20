using System;
using System.Threading.Tasks;
using Discord;

namespace Rainbow.Services.Logging;

public class Logger
{
    public Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}