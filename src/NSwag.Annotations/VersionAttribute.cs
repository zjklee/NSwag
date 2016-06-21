using System;

namespace Stucco.NSwag.Annotations
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class VersionAttribute : Attribute
    {
        public string Version { get; set; }

        public VersionAttribute(string version)
        {
            Version = version;
        }
    }
}