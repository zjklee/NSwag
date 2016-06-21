using System;

namespace Stucco.NSwag.Annotations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ResponseHeaderAttribute : Attribute
    {
        public string Name { get; private set; }
        public string StatusCode { get; set; }
        public string Description { get; private set; }
        public Type Schema { get; private set; }

        public ResponseHeaderAttribute(string name, string statusCode, string description, Type schema = null)
        {
            Name = name;
            Description = description;
            StatusCode = statusCode;
            Schema = schema ?? typeof(string);                    
        }
    }
}