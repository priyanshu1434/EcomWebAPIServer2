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

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Required]
        public int PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; }

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



    }
}
