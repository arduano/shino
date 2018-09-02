using System;
using System.ComponentModel.DataAnnotations;

namespace Shino.Database.Models
{
    public class UserActions
    {
        [Key]
        public int Id { get; set; }

        public int ActionId { get; set; }
        public virtual Action Action { get; set; }

        public int UserServerId { get; set; }
        public virtual UserServer UserServer { get; set; }

        public int Count { get; set; }

        public DateTime FirstUsed { get; set; }
        public DateTime LastUsed { get; set; }
    }
}
