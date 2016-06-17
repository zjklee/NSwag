using System;

namespace NSwag.Annotations
{
    public class DescriptionAttribute : Attribute
    {
        public string Summary { get; set; }
        public string Description { get; set; }
    }
}