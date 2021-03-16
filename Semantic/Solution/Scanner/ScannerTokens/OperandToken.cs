using System;
using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class OperandToken : ScanToken
    {
        public OperandToken(Token token, string name)
            : base(token)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public bool IsOutArgument { get; set; }

        protected internal override List<Word> Words => new() { Token };

        public override ISemanticType GetSemanticType()
        {
            Token.ConstantTypes = ConstantTypes.NonType;

            if (!(Parent is FunctionToken) && !(Parent is ArrayToken))
            {
                if (StringAnalyzer.IsInt(Name))
                {
                    Token.ConstantTypes = ConstantTypes.Int;
                    return SimpleType.Integer;
                }
                else if (StringAnalyzer.IsReal(Name))
                {
                    Token.ConstantTypes = ConstantTypes.Real;
                    return SimpleType.Real;
                }
                else if (StringAnalyzer.IsChar(Name))
                {
                    Token.ConstantTypes = ConstantTypes.Char;
                    return SimpleType.Character;
                }
                else if (StringAnalyzer.IsString(Name))
                {
                    Token.ConstantTypes = ConstantTypes.String;
                    return SimpleType.String;
                }
                else if (StringAnalyzer.IsLogical(Name))
                {
                    Token.ConstantTypes = ConstantTypes.Bool;
                    return SimpleType.Boolean;
                }
                else if (Name == SyntaxNames.Nil)
                {
                    return SimpleType.NullPointer;
                }

                // Ух ты, Linq такой крутой
                foreach (var import in Token.Tree.GetImports()
                    .Where(import => import.NameWord.Text == Name))
                {
                    SemanticItem.AddUsages(import, Token);
                    return new SimpleType(null, Name, EItemType.General);
                }
            }
            
            var item = Token.GetNameInSameScope(Name);
            if (item != null)
            {
                SemanticItem.AddUsages(item, Token);
                if (MustBeInitialized(item) 
                    && Token._initializedNames.All(x => x.All(y => !y.Contains(Name))))
                {
                    GenerateFind(new UninitializedVariableUsage(Token.Operator, Words, Name));
                }

                return GetItemType((SemanticItem) item);
            }

            SemanticError error = null;
            if (Parent is FunctionToken)
            {
                error = new UndeclaredSubprogram(Token.Operator, Words, Name);
            }
            else if (Parent is ArrayToken)
            {
                error = new UndeclaredArray(Token.Operator, Words, Name);
            }
            else
            {
                error = new UndeclaredVariable(Token.Operator, Words, Name);
            }
                
            GenerateFind(error);
            return SimpleType.Undefined;
        }

        private bool MustBeInitialized(INameable item)
        {
            return !IsOutArgument
                   && (item is Variable variable
                       && (
                           variable.TypeWord is SimpleType type && !((ISemanticType) variable.TypeWord).IsUserType
                           || variable.TypeWord is PointerType
                       )
                   )
                   || item is Parameter parameter && parameter.ModeWord.Text == SyntaxNames.Out;
        }

        private static bool MustBeInitializedMax(INameable item)
            => (new Random().Next(0, 100)) < 50;
    }
}