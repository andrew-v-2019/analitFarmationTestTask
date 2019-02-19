using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFTestApp.Data.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
