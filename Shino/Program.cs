﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Shino.Extra;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Shino
{
    class Program
    {
        public static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
        private DiscordSocketClient _discord;
        private CommandService _commands;
        private IServiceProvider _services;
        private int argPos = 0;
        private HttpClient httpClient = new HttpClient();

        async Task<string> GetGifLink(/*insert command options here*/) //Move this method to commands class
        {
            var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "http://ianlaan.nl/lovebot/api?token=" + //Replace token with variable
                "2y10lV0iIncR9Q82GFbEzMUwDX8mljO0SofHYRHjgArWr115RJEk0u&type=gif&value=" + //Make this flexible with command options
                "angry"));
            var content = await response.Content.ReadAsStringAsync();
            var js = JsonConvert.DeserializeObject(content);
            return ((dynamic)js).response;
        }

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
            else
            {
                //Alternate response system thing

                #region Check Phrases

                #endregion

                #region Check Language
                //idk maybe something that detects heated arguments and reminds people to calm down or somethng
                #endregion
            }
        }

        bool ContainsPhrase(string context, string phrase)
        {
            phrase = phrase.ToLower();
            context = context.ToLower();
            int p = 0;
            for (int i = 0; i < context.Length; i++)
            {
                //check if the phrase can start
                if (p < 2)
                {
                    if (i == 0 || !Char.IsLetter(context[i])) p = 1;
                }

                //check phrase letters if started
                if (p > 0)
                {
                    if (context[i] == phrase[p - 1]) p++;
                    else if (Char.IsLetter(context[i])) p = 0;

                    if (p == phrase.Length) return true;
                }
            }
            return false;
        }
    }
}
