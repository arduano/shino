using System.ComponentModel.DataAnnotations;

namespace Shino.Database.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinPoints { get; set; }
        public int MaxPoints { get; set; }
    }
}
