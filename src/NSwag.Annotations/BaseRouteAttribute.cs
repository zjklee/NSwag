using System;

namespace Stucco.NSwag.Annotations
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class BaseRouteAttribute : Attribute
    {
        public string Template { get; set; }

        public BaseRouteAttribute(string template)
        {
            Template = template;
        }
    }
}