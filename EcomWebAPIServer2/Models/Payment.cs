using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcomWebAPIServer2.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentId { get; set; }

        [Required]
        public int UserId { get; set; }
        

        public int OrderId { get; set; }


        [Required]
        
        public double Amount { get; set; }

        [Required]
        [StringLength(100)]

        public string PaymentMethod { get; set; }

        public string Status { get; set; }

        [Required]
        public DateTime PaymentDateTime { get; set; }


     
    }
}
