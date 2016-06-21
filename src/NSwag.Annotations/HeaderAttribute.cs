using System;

namespace Stucco.NSwag.Annotations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class HeaderAttribute : Attribute
    {
        public string Name { get; private set; }        
        public string Description { get; private set; }
        public bool Required { get; private set; }
        public object Default { get; set; }
        public Type Schema { get; private set; }

        public HeaderAttribute(string name, string description, bool required, string defaultValue = null, Type schema = null)
        {
            Name = name;
            Description = description;
            Required = required;            
            Schema = schema ?? typeof(string);
            Default = Convert.ChangeType(defaultValue, Schema, null);            
        }
    }
}