using System;
using System.Linq;
using AFTestApp.Data;
using AFTestApp.Data.Entities;
using AFTestApp.Services.Interfaces;
using AFTestApp.ViewModels.Enums;
using AFTestApp.DtoModels;

namespace AFTestApp.Services.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IAfTestAppContextFactory _afTestAppContextFactory;

        public DocumentService(IAfTestAppContextFactory afTestAppContextFactory)
        {
            _afTestAppContextFactory = afTestAppContextFactory;
        }

        public DocumentDto GetNewDocument()
        {
            var documentViewModel = new DocumentDto()
            {
                Date = DateTime.UtcNow,
                DocumentStatus = DocumentStatus.Open,
                DocumentType = DocumentType.Sale,
            };
            var doc = new Document()
            {
                Date = documentViewModel.Date,
                DocumentStatusId = (int)documentViewModel.DocumentStatus,
                DocumentTypeId = (int)documentViewModel.DocumentType
            };
            using (var context = _afTestAppContextFactory.CreateContext())
            {
                context.Documents.Add(doc);
                context.SaveChanges();
                documentViewModel.DocumentId = doc.DocumentId;
                documentViewModel.DocumentNumber = doc.DocumentId.GetDocumentNumber();
                doc.DocumentNumber = documentViewModel.DocumentNumber;
                context.SaveChanges();
            }

            return documentViewModel;
        }


        public DocumentDto SubmitDocument(DocumentDto documentDto)
        {
            using (var context = _afTestAppContextFactory.CreateContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreatedDocumentProducts(documentDto, context);
                        SubmitDocument(documentDto.DocumentId, context);

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

            return documentDto;
        }

        private static void CreatedDocumentProducts(DocumentDto documentViewModel, AfTestAppContext context)
        {
            foreach (var product in documentViewModel.Products)
            {
                var docProduct = new DocumentProduct()
                {
                    Order = product.Order,
                    ProductId = product.ProductId,
                    Count = product.Count,
                    DocumentId = documentViewModel.DocumentId
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
