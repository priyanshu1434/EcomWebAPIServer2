using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcomWebAPIServer2.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }

     //   public virtual ICollection<Order> Orders { get; set; }

     //   public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
