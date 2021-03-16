namespace Semantic.Solution
{
    public interface ISemanticType
    {
        bool IsUserType { get; }
        string FullType { get; }
        bool CanCastTo(ISemanticType semanticType, bool allowIntToReadl);
    }
}