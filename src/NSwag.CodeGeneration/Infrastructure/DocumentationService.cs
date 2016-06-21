using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stucco.NSwag.Core;
using Stucco.NSwag.Core.Interfaces;

namespace Stucco.NSwag.CodeGeneration.Infrastructure
{
    /// <summary>
    /// </summary>
    public class DocumentationService : IDocumentationService
    {
        private static readonly Lazy<DocumentationService> _instance
            = new Lazy<DocumentationService>(() => new DocumentationService());

        /// <summary>
        /// </summary>
        protected DocumentationService()
        {
        }

        /// <summary>
        /// </summary>
        internal Dictionary<object, Documentation> Documentations { get; } = new Dictionary<object, Documentation>();

        /// <summary>
        /// </summary>
        public static DocumentationService Default => _instance.Value;

        /// <summary>
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public Documentation GetMemberDescription(object memberInfo)
        {
            Documentation doc;
            if (Documentations.TryGetValue(memberInfo, out doc)) return doc;
            return new Documentation();
        }
    }

    /// <summary>
    /// </summary>
    public class DocumentationService<T> : DocumentationService
    {
        /// <summary>
        /// </summary>
        protected DocumentationService()
        {
        }

        /// <summary>
        /// </summary>
        public static DocumentationService<T> Instance
        {
            get { return new DocumentationService<T>(); }
        }

        /// <summary>
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
        /// </summary>
        /// <returns></returns>
        public static DocumentationService<T> Create(Expression<Func<T, object>> memberExpression,
            Action<Documentation> action)
        {
            var expression = memberExpression.Body as MemberExpression;
            if (expression == null) return null;
            var member = expression.Member;

            return Create(member, action);
        }

        /// <summary>
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