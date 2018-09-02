using System.ComponentModel.DataAnnotations;

namespace Shino.Database.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int UserServerId { get; set; }
        public virtual UserServer UserServer { get; set; }

        public int Count { get; set; }
    }
}
