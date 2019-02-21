using System;
using System.Data.Entity;
using System.Linq;
using AFTestApp.Data;
using AFTestApp.Data.Entities;
using AFTestApp.Services.Interfaces;
using AFTestApp.ViewModels.Enums;
using AFTestApp.DtoModels;
using AFTestApp.Extensions;
using AFTestApp.ViewModels;
using NLog;

namespace AFTestApp.Services.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IAfTestAppContextFactory _afTestAppContextFactory;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public DocumentService(IAfTestAppContextFactory afTestAppContextFactory)
        {
            _afTestAppContextFactory = afTestAppContextFactory;
        }

        private string GetNextDocumentNumber()
        {
            using (var context = _afTestAppContextFactory.CreateContext())
            {
                const int result = 1;
                var lastDocument = context.Documents.OrderByDescending(x => x.DocumentId).FirstOrDefault();
                return lastDocument == null ? result.GetDocumentNumber() : (lastDocument.DocumentId + 1).GetDocumentNumber();
            }
        }

        public DocumentDto GetNewDocument()
        {
            var document = new Document()
            {
                Date = DateTime.UtcNow,
                DocumentStatusId = (int)DocumentStatus.Open,
                DocumentTypeId = (int)DocumentType.Sale,
                DocumentNumber = GetNextDocumentNumber(),
            };

            var dto = Project(document);
            return dto;
        }


        public SubmitResultDto SubmitDocument(DocumentDto documentDto)
        {
            var result = new SubmitResultDto()
            {
                Document = documentDto,
                Success = true,
            };
            using (var context = _afTestAppContextFactory.CreateContext())
            {
                result.TotalDocumentsCount = context.Documents.Count();
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        SubmitDocument(documentDto, context);
                        CreatedDocumentProducts(documentDto, context);

                        context.SaveChanges();
                        tran.Commit();
                        result.TotalDocumentsCount++;
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        result.Success = false;
                        result.Message = e.Message;
                        Logger.Error(e);
                    }
                }
            }
            return result;
        }

        public void CreatedDocumentProducts(DocumentDto documentDto, AfTestAppContext context)
        {
            foreach (var product in documentDto.Products)
            {
                var docProduct = new DocumentProduct();
                docProduct.MapFromObjectWithSameNames(product);
                docProduct.DocumentId = documentDto.DocumentId;
                context.DocumentProduct.Add(docProduct);
            }
        }

        public void SubmitDocument(DocumentDto documentDto, AfTestAppContext context)
        {
            var doc = Project(documentDto);
            doc.DocumentStatusId = (int)DocumentStatus.Submitted;
            context.Documents.Add(doc);
            context.SaveChanges();
            documentDto.DocumentId = doc.DocumentId;
        }

        private static DocumentDto Project(Document doc)
        {
            var documentDto = new DocumentDto();
            documentDto.MapFromObjectWithSameNames(doc);
            documentDto.DocumentStatus = (DocumentStatus)doc.DocumentStatusId;
            documentDto.DocumentType = (DocumentType)doc.DocumentTypeId;
            return documentDto;
        }

        private static Document Project(DocumentDto docDto)
        {
            var document = new Document();
            document.MapFromObjectWithSameNames(docDto);
            return document;
        }
    }
}
