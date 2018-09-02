using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shino.Database.Models
{
    public class Points
    {
        [Key]
        public int Id { get; set; }

        public int UserServerId { get; set; }
        public virtual UserServer UserServer { get; set; }

        public int Count { get; set; }

        public DateTime FirstAwarded { get; set; } 
        public DateTime LastAwarded { get; set; }
    }
}
