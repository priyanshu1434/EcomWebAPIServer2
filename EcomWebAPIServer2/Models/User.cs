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
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public long PhoneNumber { get; set; }


        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^(user|seller)$", ErrorMessage = "Role must be either 'user' or 'seller'.")]
        public string Role { get; set; }

        //  public virtual ICollection<Order> Orders { get; set; }

        //  public virtual ICollection<CartItem> CartItems { get; set; }

        //  public virtual ICollection<Payment> Payments { get; set; }
    }
}
