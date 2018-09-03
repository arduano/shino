using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shino.Database.Models
{
    public class UserServer
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ServerId { get; set; }
        public virtual Server Server { get; set; }

        public int PointsId { get; set; }
        public virtual Points Points { get; set; }

        public virtual ICollection<UserActions> UserActions { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
