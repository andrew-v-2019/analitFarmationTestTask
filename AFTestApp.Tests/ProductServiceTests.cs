using AFTestApp.Data;
using AFTestApp.Data.Entities;
using AFTestApp.Services.Services;
using Moq;
using NUnit.Framework;


namespace AFTestApp.Tests
{
    public class ProductServiceTests
    {

        private Mock<IAfTestAppContextFactory> _dbContextFactoryMock;
        private AfTestAppContext _dbContextMock;

        private ProductService _service;

        [SetUp]
        public void SetUp()
        {
            _dbContextFactoryMock = new Mock<IAfTestAppContextFactory>();
            _dbContextMock = new AfTestAppContext
            {
                Products = new FakeDbSet<Product>()
            };
            _dbContextFactoryMock.Setup(x => x.CreateContext()).Returns(_dbContextMock);
            _service = new ProductService(_dbContextFactoryMock.Object);
        }

        [Test]
        public void GetProducts_ProductsExists_ProductsReturned()
        {
            var product = new Product()
            {
                Name = nameof(GetProducts_ProductsExists_ProductsReturned),
                ProductId = 1,
                Price = 1,
                Code = nameof(GetProducts_ProductsExists_ProductsReturned)
            };
            _dbContextMock.Products.Add(product);

            var testingProducts = _service.GetProducts();
            Assert.IsNotNull(testingProducts);
            Assert.AreEqual(testingProducts.Count, 1);

            foreach (var p in testingProducts)
            {
                Assert.AreEqual(p.ProductId, product.ProductId);
                Assert.AreEqual(p.Code, product.Code);
                Assert.AreEqual(p.Name, product.Name);
                Assert.AreEqual(p.Price, product.Price);
            }
        }
    }
}
