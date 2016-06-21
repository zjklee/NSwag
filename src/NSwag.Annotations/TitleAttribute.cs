using System;

namespace Stucco.NSwag.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TitleAttribute : Attribute
    {
        public string Title { get; set; }

        public TitleAttribute(string title)
        {
            Title = title;
        }
    }
}