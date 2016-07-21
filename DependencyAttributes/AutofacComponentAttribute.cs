using System;

namespace DependencyAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutofacComponentAttribute : Attribute
    {

        public bool IsSingleInstance { get; set; }

        public AutofacComponentAttribute()
        {
            IsSingleInstance = false;
        }
    }
}
