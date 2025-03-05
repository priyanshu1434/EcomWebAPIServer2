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
        public int ProductId { get; set; }
        // [ForeignKey("ProductId")]
        // public virtual Product Product { get; set; }

        [Required]
        public int UserId { get; set; }
        // [ForeignKey("UserId")]
        //public virtual User User { get; set; }

        public int Quantity { get; set; }

        // public Product Product { get; set; }
    }
}
