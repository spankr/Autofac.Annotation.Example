using DependencyAttributes;

namespace SampleServices.Services
{
    [AutofacComponent(IsSingleInstance = true)]
    public class SampleService : ISampleService
    {
        private static int NumInstances = 0;

        private readonly string _sampleValue;
        public SampleService()
        {
            _sampleValue = $"Sample {NumInstances}";
            NumInstances++;
        }

        public string GetSampleValue()
        {
            return _sampleValue;
        }
    }
}
