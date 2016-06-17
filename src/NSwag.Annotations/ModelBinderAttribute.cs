using System;

namespace NSwag.Annotations
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ModelBinderAttribute : Attribute
    {
        public Type BindsFromType { get; private set; }

        public ModelBinderAttribute(Type bindsFromType)
        {
            BindsFromType = bindsFromType;
        }
    }
}