using System;
using System.Linq;
using AFTestApp.Data;
using AFTestApp.Data.Entities;
using AFTestApp.Services.Interfaces;
using AFTestApp.ViewModels;
using AFTestApp.ViewModels.Enums;

namespace AFTestApp.Services.Services
{
    public class DocumentService: IDocumentService
    {
        private readonly IAfTestAppContextFactory _afTestAppContextFactory;

        public DocumentService(IAfTestAppContextFactory afTestAppContextFactory)
        {
            _afTestAppContextFactory = afTestAppContextFactory;
        }

        public DocumentViewModel GetNewDocument()
        {
            var documentViewModel = new DocumentViewModel()
            {
                Date = DateTime.UtcNow,
                DocumentStatus = DocumentStatus.Open,
                DocumentType = DocumentType.Sale,
            };
            var doc = new Document()
            {
                Date = documentViewModel.Date,
                DocumentStatusId = (int) documentViewModel.DocumentStatus,
                DocumentTypeId = (int) documentViewModel.DocumentType
            };
            using (var context = _afTestAppContextFactory.CreateContext())
            {
                context.Documents.Add(doc);
                documentViewModel.DocumentId = doc.DocumentId;
                documentViewModel.DocumentNumber =doc.DocumentId.GetDocumentNumber();
                doc.DocumentNumber = documentViewModel.DocumentNumber;
                context.SaveChanges();
            }

            return documentViewModel;
        }

        
        public DocumentViewModel SubmitDocument(DocumentViewModel documentViewModel)
        {
            using (var context = _afTestAppContextFactory.CreateContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreatedDocumentProducts(documentViewModel, context);
                        SubmitDocument(documentViewModel.DocumentId, context);

                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }

            return documentViewModel;
        }

        private static void CreatedDocumentProducts(DocumentViewModel documentViewModel, AfTestAppContext context)
        {
            var groupedProducts = documentViewModel.Products.GroupBy(x => x.ProductId).Select(x => new { Id = x.Key, Count = x.Count() });
            foreach (var groupedProduct in groupedProducts)
            {
                var docProduct = new DocumentProduct()
                {
                    ProductId = groupedProduct.Id,
                    Count = groupedProduct.Count,
                };
                context.DocumentProduct.Add(docProduct);
            }
        }

        private static void SubmitDocument(int documentId, AfTestAppContext context)
        {
            var doc = context.Documents.FirstOrDefault(x => x.DocumentId == documentId);
            if (doc != null)
            {
                doc.DocumentStatusId = (int)DocumentStatus.Submitted;
            }
        }



    }
}
