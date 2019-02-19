using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFTestApp.Data.Entities
{
    [Table("DocumentProduct")]
    public class DocumentProduct
    {
        [Key]
        [Column(Order = 1)]
        public int DocumentId { get; set; }

        public int Count { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ProductId { get; set; }

        public int Order { get; set; }

        [ForeignKey("DocumentId")]
        public virtual Document Document { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
