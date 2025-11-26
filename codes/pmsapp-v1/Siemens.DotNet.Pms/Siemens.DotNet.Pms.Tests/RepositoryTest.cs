using NUnit.Framework.Legacy;
using Siemens.DotNet.Pms.Entities;
using Siemens.DotNet.Pms.Repository;

namespace Siemens.DotNet.Pms.Tests
{
    public class RepositoryTest
    {
        private IAsyncRepository<Product> productRepository;

        [SetUp]
        public void Setup()
        {
            productRepository = new AsyncFileRepository<Product>("products.json");
        }

        [TearDown]
        public void Teardown()
        {
            productRepository = null;
        }

        [Test]
        public void ReadDataAsyncTest()
        {
            var expectedSet = new HashSet<Product> { new Product { Id = 100, Name = "dell xps laptop", Price = 120000m, Description = "new 15 inch laptop from dell" } };

            Task<HashSet<Product>?> productTask = productRepository.ReadAllAsync();
            var set = productTask.Result;
            if (set != null)
            {
                Assert.That(
                    expectedSet,
                    Is.EqualTo(set).AsCollection
                    );
            }
        }
    }
}
