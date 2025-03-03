using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcomWebAPIServer2.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdminId { get; set; }

        [Required]

        public int Username { get; set; }

        [Required]
        public string? Password { get; set; }


        [Required]

        public string? AdminName { get; set; }

        [Required]

        [RegularExpression("^(SuperAdmin|Seller)$", ErrorMessage = "Role must be SuperAdmin or Seller.")]
        public string? Role { get; set; }
    }
}
