using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace FactoryAPI.Models
{
    public class Item
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
