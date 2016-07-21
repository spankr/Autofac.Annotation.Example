using System;

namespace SampleServices.Services
{
    public class NotAnnotatedService : INotAnnotated
    {
        private readonly string _sampleValue;
        public NotAnnotatedService()
        {
            _sampleValue = $"Sample {DateTime.Now.Ticks}";
        }

        public string GetSampleValue()
        {
            return _sampleValue;
        }
    }
}
