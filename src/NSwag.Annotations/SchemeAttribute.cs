using System;

namespace Stucco.NSwag.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SchemeAttribute : Attribute
    {
        public string Scheme { get; set; }

        public SchemeAttribute(string scheme)
        {
            Scheme = scheme;
        }
    }
}