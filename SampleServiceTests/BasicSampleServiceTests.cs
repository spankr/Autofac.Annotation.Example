using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleServices.Services;

namespace SampleServiceTests
{
    [TestClass]
    public class BasicSampleServiceTests
    {
        [TestMethod]
        public void TestGetSample()
        {
            var service = new SampleService();

            Assert.IsNotNull(service);
            Assert.IsNotNull(service.GetSampleValue());
        }

        [TestMethod]
        public void TestGetNotAnnotated()
        {
            var service = new NotAnnotatedService();

            Assert.IsNotNull(service);
            Assert.IsNotNull(service.GetSampleValue());
        }

        [TestMethod]
        public void TestGetPerRequest()
        {
            var service = new PerRequestService();

            Assert.IsNotNull(service);
            Assert.IsNotNull(service.GetSampleValue());
        }

    }
}
