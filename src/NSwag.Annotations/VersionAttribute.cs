using System;

namespace Stucco.NSwag.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class VersionAttribute : Attribute
    {
        public string Version { get; set; }

        public VersionAttribute(string version)
        {
            Version = version;
        }
    }
}