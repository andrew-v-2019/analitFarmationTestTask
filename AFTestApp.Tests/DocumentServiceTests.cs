using System;
using System.Collections.Generic;
using System.Linq;
using AFTestApp.Data;
using AFTestApp.Data.Entities;
using AFTestApp.DtoModels;
using AFTestApp.Extensions;
using AFTestApp.Services.Services;
using AFTestApp.ViewModels.Enums;
using Moq;
using NUnit.Framework;

namespace AFTestApp.Tests
{
    public class DocumentServiceTests
    {
        private Mock<IAfTestAppContextFactory> _dbContextFactoryMock;
        private AfTestAppContext _dbContextMock;
        private DocumentService _service;

        [SetUp]
        public void SetUp()
        {
            _dbContextFactoryMock = new Mock<IAfTestAppContextFactory>();
            _dbContextMock = new AfTestAppContext()
            {
                Products = new FakeDbSet<Product>(),
                Documents = new FakeDbSet<Document>(),
                DocumentProduct = new FakeDbSet<DocumentProduct>(),
            };
            _dbContextFactoryMock.Setup(x => x.CreateContext()).Returns(_dbContextMock);
            _service = new DocumentService(_dbContextFactoryMock.Object);
        }

        [Test]
        public void GetNewDocument_FirstDocument_DocumentReturned()
        {
            var doc = _service.GetNewDocument();
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.DocumentStatusId, (int)DocumentStatus.Open);
            Assert.AreEqual(doc.DocumentTypeId, (int)DocumentType.Sale);
            Assert.AreEqual(doc.Date.Date, DateTime.UtcNow.Date);
            Assert.AreEqual(doc.DocumentNumber, 1.GetDocumentNumber());
        }

        [Test]
        public void SubmitDocument_DocumentCorrect_DocumentSubmitted()
        {
            var documentDto = new DocumentDto()
            {
                DocumentId = 1,
                DocumentStatusId = 1,
                Date = DateTime.UtcNow,
                Products = new List<ProductDto>(),
                DocumentNumber = nameof(SubmitDocument_DocumentCorrect_DocumentSubmitted),
                DocumentTypeId = 1,
                DocumentType = DocumentType.Sale
            };
            _service.SubmitDocument(documentDto, _dbContextMock);

            var testingDoc = _dbContextMock.Documents.FirstOrDefault(x => x.DocumentId == documentDto.DocumentId);

            Assert.IsNotNull(testingDoc);
            Assert.AreEqual(documentDto.DocumentId, testingDoc.DocumentId);
            Assert.AreEqual(documentDto.DocumentNumber, testingDoc.DocumentNumber);
            Assert.AreEqual(documentDto.DocumentStatusId, (int)DocumentStatus.Submitted);
            Assert.AreEqual(documentDto.DocumentTypeId, testingDoc.DocumentTypeId);
        }

        [Test]
        public void CreatedDocumentProducts_WithProducts_ProductsCreated()
        {
            var documentDto = new DocumentDto()
            {
                DocumentId = 1,
                Products = new List<ProductDto>()
            };

            for (var i = 1; i < 100; i++)
            {
                var productDto = new ProductDto()
                {
                    Count = 100,
                    Code = nameof(CreatedDocumentProducts_WithProducts_ProductsCreated),
                    Name = nameof(CreatedDocumentProducts_WithProducts_ProductsCreated),
                    Price = i,
                    Order = i
                };
                documentDto.Products.Add(productDto);
            }

            _service.CreatedDocumentProducts(documentDto, _dbContextMock);

            var testingDocProducts = _dbContextMock.DocumentProduct.Where(x => x.DocumentId == documentDto.DocumentId).Select(x => x);

            Assert.IsNotNull(testingDocProducts);
            Assert.AreEqual(testingDocProducts.Count(), documentDto.Products.Count);

            foreach (var testingDocProduct in testingDocProducts)
            {
                var productDto = documentDto.Products.FirstOrDefault(x => x.ProductId == testingDocProduct.ProductId);
                Assert.IsNotNull(productDto);
                Assert.AreEqual(testingDocProduct.DocumentId, documentDto.DocumentId);
                Assert.AreEqual(testingDocProduct.ProductId, productDto.ProductId);
                Assert.AreEqual(testingDocProduct.Count, productDto.Count);
            }
        }
    }
}
