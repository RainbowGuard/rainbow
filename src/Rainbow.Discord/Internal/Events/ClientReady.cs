using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Rainbow.Discord.Internal.Events
{
    internal static class ClientReady
    {
        public static async Task Handler(DiscordSocketClient client)
        {
            foreach (var guild in await client.Rest.GetGuildsAsync())
            {
                var command = new SlashCommandBuilder()
                    .WithName(SlashCommandNames.Command1)
                    .WithDescription("Something spicy")
                    .Build();

                try
                {
                    await guild.CreateApplicationCommandAsync(command);
                }
                catch (ApplicationCommandException e)
                {
                    var json = JsonConvert.SerializeObject(e.Error, Formatting.Indented);
                    Console.WriteLine(json);
                }
            }
        }
    }
}