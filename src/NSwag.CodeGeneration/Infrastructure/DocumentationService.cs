using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using NSwag.Interfaces;

namespace NSwag.CodeGeneration.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class DocumentationService : IDocumentationService
    {
        private readonly Dictionary<object, Documentation> _documentations
            = new Dictionary<object, Documentation>();

        /// <summary>
        /// 
        /// </summary>
        internal Dictionary<object, Documentation> Documentations
        {
            get { return _documentations; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public Documentation GetMemberDescription(object memberInfo)
        {
            Documentation doc;
            if (_documentations.TryGetValue(memberInfo, out doc)) return doc;
            return new Documentation();
        }

        private static readonly Lazy<DocumentationService> _instance
            = new Lazy<DocumentationService>(() => new DocumentationService());

        /// <summary>
        /// 
        /// </summary>
        protected DocumentationService()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public static DocumentationService Default => _instance.Value;
    }

    /// <summary>
    /// 
    /// </summary>
    public class DocumentationService<T> : DocumentationService
    {
        /// <summary>
        /// 
        /// </summary>
        protected DocumentationService()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public static DocumentationService<T> Instance
        {
            get { return new DocumentationService<T>(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberExpression"></param>
        /// <returns></returns>
        public Documentation GetMemberDescription(Expression<Func<T, object>> memberExpression)
        {
            var expression = memberExpression.Body as MemberExpression;
            if (expression != null)
            {
                var member = expression.Member;
                return GetMemberDescription(member);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DocumentationService<T> Create(Expression<Func<T, object>> memberExpression, Action<Documentation> action)
        {
            var expression = memberExpression.Body as MemberExpression;
            if (expression == null) return null;
            var member = expression.Member;

            return Create(member, action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DocumentationService<T> Create(object member, Action<Documentation> action)
        {
            var created = new Documentation();
            Default.Documentations.Add(member, created);
            action(created);
            return Instance;
        }
    }

}