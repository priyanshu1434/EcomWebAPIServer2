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
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public double ProductPrice { get; set; }

        public string ProductCategory { get; set; }

        public string ProductImgURL { get; set; }


    }
}
