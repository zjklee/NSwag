using System;

namespace Stucco.NSwag.Annotations
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ModelBindsFromAttribute : Attribute
    {
        public ModelBindsFromAttribute(Type bindsFromType)
        {
            BindsFromType = bindsFromType;
        }

        public Type BindsFromType { get; private set; }
    }
}