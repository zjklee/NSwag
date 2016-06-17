using System;
using NSwag.Interfaces;
using System.Reflection;
using System.ComponentModel;
using System.Linq;

namespace NSwag.CodeGeneration.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class AttributeDocumentationService : IDocumentationService
    {
        private readonly Type _type;
        private readonly string _property;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="property"></param>
        public AttributeDocumentationService(Type type, string property)
        {
            _type = type;
            _property = property;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public Documentation GetMemberDescription(object memberInfo)
        {
            if (memberInfo is MethodInfo)
            {
                var provider = ((MethodInfo) memberInfo).GetCustomAttributes()
                    .SingleOrDefault(o => o.GetType() == _type);

                if (provider == null) return new Documentation();

                return new Documentation
                {
                    Descripation = (string) provider.GetType().GetRuntimeProperty(_property).GetValue(provider)
                };
            }

            if (memberInfo is ParameterInfo)
            {
                var provider = ((ParameterInfo)memberInfo).GetCustomAttributes()
                    .SingleOrDefault(o => o.GetType() == _type);

                if(provider == null) return new Documentation();

                return new Documentation
                {
                    Descripation = (string)provider.GetType().GetRuntimeProperty(_property).GetValue(provider)
                };
            }

            return new Documentation();
        }
    }
}