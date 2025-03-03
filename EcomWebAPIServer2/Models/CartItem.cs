using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcomWebAPIServer2.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CartItemId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public int Quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
