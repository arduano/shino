using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Shino.Extra;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Shino
{
    class Program
    {
        public static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
        private DiscordSocketClient _discord;
        private CommandService _commands;
        private IServiceProvider _services;
        private int argPos = 0;


        public async Task RunBotAsync()
        {
            // Initialize discord and command service
            _discord = new DiscordSocketClient(new DiscordSocketConfig());
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_discord)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            // Add command module
            await _commands.AddModuleAsync(new Commands().GetType());

            _discord.MessageReceived += MessageReceived;
            _discord.Log += Log;
            _discord.Ready += ClientReady;

            var token = File.ReadAllText("token");
            await _discord.LoginAsync(TokenType.Bot, token);
            await _discord.StartAsync();

            await Task.Delay(-1);
        }

        private async Task ClientReady()
        {
            await _discord.SetActivityAsync(new Activity("with innocence", ActivityType.Watching));
            Console.WriteLine();
        }

        private async Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
        }

        private async Task MessageReceived(SocketMessage arg)
        {
            if (!(arg is SocketUserMessage msg)) return;
            var context = new SocketCommandContext(_discord, msg);

            if (msg.HasStringPrefix(">", ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos);

                if (result.IsSuccess)
                {
                    Console.WriteLine(msg.Author.Username + ": " + msg.Content);
                }
                else
                {
                    Console.Write(result.ErrorReason);
                }
            }
            else if (msg.Author.Id == _discord.CurrentUser.Id)
            {
                Console.WriteLine("| Response: " + msg.Content);
            }
        }
    }
}
