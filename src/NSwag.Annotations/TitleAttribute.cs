using System;

namespace Stucco.NSwag.Annotations
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class TitleAttribute : Attribute
    {
        public string Title { get; set; }

        public TitleAttribute(string title)
        {
            Title = title;
        }
    }
}