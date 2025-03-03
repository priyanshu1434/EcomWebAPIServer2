using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcomWebAPIServer2.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }


        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }


        [Phone(ErrorMessage = "Invalid phone number format")]
        public int PhoneNumber { get; set; }


        public string ShippingAddress { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
