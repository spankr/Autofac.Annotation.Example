using Autofac;
using DependencyAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleServices.Services;
using System.Linq;
using System.Reflection;

namespace SampleServices.Tests
{
    [TestClass]
    public class AutofacTests
    {
        [TestMethod]
        public void TestWithBasicAutofac()
        {
            // Build the container.
            var builder = new ContainerBuilder();
            var servicesAssembly = Assembly.Load("SampleServices");

            builder.RegisterAssemblyTypes(servicesAssembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().SingleInstance();

            var ctx = builder.Build();

            var service = ctx.Resolve<ISampleService>();
            Assert.IsNotNull(service);
            Assert.IsNotNull(service.GetSampleValue());

            var svc2 = ctx.Resolve<IPerRequest>();
            Assert.IsNotNull(svc2);
            Assert.IsNotNull(svc2.GetSampleValue());

            var notAnnotated = ctx.Resolve<INotAnnotated>();
            Assert.IsNotNull(notAnnotated);
            Assert.IsNotNull(notAnnotated.GetSampleValue());
        }

        [TestMethod]
        public void TestByAnnotation()
        {
            // Build the container.
            var builder = new ContainerBuilder();
            var servicesAssembly = Assembly.Load("SampleServices");

            // Singletons
            builder.RegisterAssemblyTypes(servicesAssembly).Where(t => {
                var att = (t.GetCustomAttribute(typeof(AutofacComponentAttribute)) as AutofacComponentAttribute);
                return att != null && att.IsSingleInstance;
            }).AsImplementedInterfaces().SingleInstance();

            // Non-singletons
            builder.RegisterAssemblyTypes(servicesAssembly).Where(t =>
            {
                var att = (t.GetCustomAttribute(typeof(AutofacComponentAttribute)) as AutofacComponentAttribute);
                return att != null && !att.IsSingleInstance;
            }).AsImplementedInterfaces().InstancePerDependency();

            var ctx = builder.Build();

            // Single instance
            var service = ctx.Resolve<ISampleService>();
            Assert.IsNotNull(service);
            var firstReference = service.GetSampleValue();
            Assert.IsNotNull(service.GetSampleValue());

            service = ctx.Resolve<ISampleService>();
            Assert.AreEqual(firstReference, service.GetSampleValue(), "This should be the same instance");


            // Per-request instances
            var perRequestService = ctx.Resolve<IPerRequest>();
            Assert.IsNotNull(perRequestService);
            firstReference = perRequestService.GetSampleValue();
            Assert.IsNotNull(firstReference);

            perRequestService = ctx.Resolve<IPerRequest>();
            Assert.IsNotNull(perRequestService);
            Assert.AreNotEqual(firstReference, perRequestService.GetSampleValue(), "This should not be the same instance");

            // Not annotated
            var notAnnotated = ctx.ResolveOptional<INotAnnotated>();
            Assert.IsNull(notAnnotated);
        }
    }
}
