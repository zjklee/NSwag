using System;

namespace Stucco.NSwag.Annotations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ParameterAttribute : Attribute
    {
        public string Name { get; private set; }
        public Type ParameterType { get; private set; }
        public object Default { get; set; }
        public string Description { get; set; }
        public bool Required { get; private set; }
        public string SwaggerParameterType { get; set; }

        public ParameterAttribute(string name, Type parameterType, string defaultValue = null, string description = "", bool required = false, string swaggerParameterType = null)
        {
            Name = name;
            ParameterType = parameterType;
            Default = Convert.ChangeType(defaultValue, parameterType, null);
            Description = description;
            Required = required;
            SwaggerParameterType = swaggerParameterType;
        }
    }
}