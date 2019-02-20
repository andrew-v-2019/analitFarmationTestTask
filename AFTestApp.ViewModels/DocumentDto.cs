using System;
using System.Collections.Generic;
using AFTestApp.ViewModels.Enums;

namespace AFTestApp.DtoModels
{
    public class DocumentDto
    {
        public int DocumentId { get; set; }

        public int DocumentTypeId { get; set; }

        public DocumentType DocumentType { get; set; }

        public int DocumentStatusId { get; set; }

        public DocumentStatus DocumentStatus { get; set; }

        public string DocumentNumber { get; set; }
        public DateTime Date { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}
