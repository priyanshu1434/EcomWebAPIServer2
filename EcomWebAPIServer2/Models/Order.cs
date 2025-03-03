using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcomWebAPIServer2.Models
{
    public class Order
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int OrderId { get; set; }

        [Required]

        [ForeignKey("User")]

        public int UserId { get; set; }

        [Required]

        [ForeignKey("Product")]

        public int ProductId { get; set; }

        [Required]
        [ForeignKey("PaymentId")]
        public int PaymentId { get; set; }

        [Required]

        public double TotalPrice { get; set; }

        [Required]

        [MaxLength(250)]

        public string ShippingAddress { get; set; }

        [Required]

        [MaxLength(50)]

        public string OrderStatus { get; set; }

        [Required]

        [MaxLength(50)]

        public string PaymentStatus { get; set; }

        [Required]

        public DateTime OrderDateTime { get; set; }

        public virtual User User { get; set; }

        public virtual Product Product { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
