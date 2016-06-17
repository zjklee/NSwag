using System.Collections.Generic;
using System.Reflection;

namespace NSwag.CodeGeneration.Infrastructure
{
    public class DocumantationService<T>
    {
        private readonly Dictionary<MemberInfo, Documentation> _documentations
            = new Dictionary<MemberInfo, Documentation>();

        public DocumantationService()
        {
        }

        public Dictionary<MemberInfo, Documentation> Documentations
        {
            set { _documentations = value; }
            get { return _documentations; }
        }
    }
}