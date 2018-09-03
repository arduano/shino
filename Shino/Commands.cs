using Discord;
using Discord.Commands;
using Microsoft.EntityFrameworkCore;
using Shino.Database;
using Shino.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shino
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        private ShinoDb db;
        private UserServer us;

        public Commands()
        {
            db = new ShinoDb();
        }

        [Command("EmbedTest")]
        public async Task EmbedTest()
        {
            var embed = new EmbedBuilder();
        }

        [Command("lp")]
        public async Task CheckPoints()
        {
            CheckUser();
            var eb = new EmbedBuilder();
            eb.WithDescription((Context.Guild.CurrentUser.Nickname ?? Context.User.Username) + " you have \t**" +  + "** points");
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }

        [Command("inventory")]
        public async Task CheckInventory()
        {
            CheckUser();
            var eb = new EmbedBuilder();
            eb.WithDescription((Context.Guild.CurrentUser.Nickname ?? Context.User.Username) + " you have \t**" + us.Inventory.ToList().Sum(l => l.Count) + "** items in your inventory");
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }

        private void CheckUser()
        {

            User user;
            Server server;

            if (!db.Users.Any(u => u.DiscordId == Context.User.Id))
            {
                user = new User()
                {
                    DiscordId = Context.User.Id
                };
                db.Users.Add(user);
                db.SaveChanges();
            }
            else
            {
                user = db.Users.FirstOrDefault(u => u.DiscordId == Context.User.Id);
            }

            if (!db.Servers.Any(s => s.DiscordId == Context.Guild.Id))
            {
                server = new Server()
                {
                    DiscordId = Context.Guild.Id
                };
                db.Servers.Add(server);
                db.SaveChanges();
            }
            else
            {
                server = db.Servers.FirstOrDefault(s => s.DiscordId == Context.Guild.Id);
            }

            if (!db.UserServers.Any(us => us.UserId == user.Id && us.ServerId == server.Id))
            {
                us = new UserServer()
                {
                    UserId = user.Id,
                    ServerId = server.Id,
                };
                db.UserServers.Add(us);
                db.SaveChanges();

                var lp = new Points()
                {
                    Count = 0,
                    UserServerId = us.Id
                };

                db.Points.Add(lp);
                db.SaveChanges();
                us.LovePoints = lp;
                us.LovePointsId = lp.Id;
            }
            else
            {
                us = db.UserServers.Include(s => s.LovePoints).FirstOrDefault(us => us.UserId == user.Id && us.ServerId == server.Id);
            }
        }
    }
}
