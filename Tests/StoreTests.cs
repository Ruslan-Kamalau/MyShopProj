using NUnit.Framework;

namespace MyProject.Tests
{
    [TestFixture]
    public class StoreTests
    {
        private Store _store;

        [SetUp]
        public void Setup()
        {
            _store = new Store();
        }

        [Test]
        public async Task AddProductAsync_Test()
        {
            var product = new Product("Laptop", 1000m, "Electronics");

            await _store.AddProductAsync(product);

            var products = _store.GetProducts().ToList();
            Assert.That(1, Is.EqualTo(products.Count));
            Assert.That("Laptop", Is.EqualTo(products[0].Name));
        }
    }
}
