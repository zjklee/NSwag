namespace Stucco.NSwag.Core.Interfaces
{
    public interface IDocumentationService
    {
        Documentation GetMemberDescription(object memberInfo);
    }
}