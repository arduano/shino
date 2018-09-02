using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shino.Database.Models
{
    public class Server
    {
        [Key]
        public int Id { get; set; }
        public ulong DiscordId { get; set; }

        public virtual ICollection<UserServer> Users { get; set; }
    }
}
