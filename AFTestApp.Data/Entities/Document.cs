using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFTestApp.Data.Entities
{
    [Table("Document")]
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        public int DocumentTypeId { get; set; }
        public int DocumentStatusId { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime Date { get; set; }
    }
}
