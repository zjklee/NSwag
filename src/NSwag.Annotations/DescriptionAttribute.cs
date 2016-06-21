using System;

namespace Stucco.NSwag.Annotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute : Attribute
    {
        public string Summary { get; set; }
        public string Description { get; set; }
    }
}