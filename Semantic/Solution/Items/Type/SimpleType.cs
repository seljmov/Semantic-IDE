using System;

namespace Semantic.Solution
{
    public class SimpleType : Word, ISemanticType
    {
        public SimpleType(SemanticItem? @operator, string text, EItemType type)
            : base(@operator, text, type, true) { }

        // TODO: Обновить лексику и указать тут нужное (см. старый код)
        public override string ItemName => Strings.SimpleType;

        internal static ISemanticType Integer => new SimpleType(null, SyntaxNames.Integer, EItemType.General);
        internal static ISemanticType Real => new SimpleType(null, SyntaxNames.Real, EItemType.General);
        internal static ISemanticType Boolean => new SimpleType(null, SyntaxNames.Boolean, EItemType.General);
        internal static ISemanticType Character => new SimpleType(null, SyntaxNames.Character, EItemType.General);
        internal static ISemanticType NullPointer => new SimpleType(null, SyntaxNames.NullPointer, EItemType.General);
        internal static ISemanticType Undefined => new SimpleType(null, SyntaxNames.Undefined, EItemType.General);
        internal static ISemanticType Subprogram => new SimpleType(null, Strings.Subprogram, EItemType.General);
        internal static ISemanticType Type => new SimpleType(null, SyntaxNames.Type, EItemType.General);
        internal static ISemanticType SystemType => new SimpleType(null, SyntaxNames.SystemType, EItemType.General);
        internal static ISemanticType String => new SimpleType(null, SyntaxNames.String, EItemType.General);

        public bool IsUserType => !SyntaxNames.Types.Contains(Text);

        public string FullType => _parent != null && Text.Contains('.') && !SyntaxNames.Types.Contains(Text)
            ? $"{Tree?.Name}.{Text}"
            : Text;

        public bool CanCastTo(ISemanticType semanticType, bool allowIntToReadl)
        {
            throw new NotImplementedException();
        }
    }
}
