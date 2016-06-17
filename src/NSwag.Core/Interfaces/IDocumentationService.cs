using System;
using System.Reflection;

namespace NSwag.Interfaces
{
    public interface IDocumentationService
    {
        Documentation GetMemberDescription(object memberInfo);
    }
}