using System;

namespace NSwag.Annotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class JsonDocumentationPathAttribute : Attribute
    {
        public string Path { get; private set; }

        public JsonDocumentationPathAttribute(string path)
        {
            Path = path;
        }
    }
}