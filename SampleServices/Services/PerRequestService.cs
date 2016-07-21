using DependencyAttributes;

namespace SampleServices.Services
{
    [AutofacComponent]
    public class PerRequestService : IPerRequest
    {
        private static int NumInstances = 0;

        private readonly string _sampleValue;
        public PerRequestService()
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
