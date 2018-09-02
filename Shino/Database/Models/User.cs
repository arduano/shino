using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shino.Database.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public ulong DiscordId { get; set; }

        public virtual ICollection<UserServer> Servers { get; set; }
    }
}
