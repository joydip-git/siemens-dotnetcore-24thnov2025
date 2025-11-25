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
            
        }
    }
}
