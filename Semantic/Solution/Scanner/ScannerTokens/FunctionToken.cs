using System.Collections.Generic;

namespace Semantic.Solution
{
    public class FunctionToken : ScanToken
    {
        public FunctionToken(Token token, ScanToken function, List<ScanToken> arguments) 
            : base(token)
        {
            Function = function;
            Arguments = arguments;
            Function.Parent = this;
        }

        public ScanToken Function { get; private set; }
        public List<ScanToken> Arguments { get; private set; }

        protected internal override List<Word> Words
        {
            get
            {
                var words = new List<Word>();
                words.AddRange(Function.Words);
                foreach (var token in Arguments)
                {
                    words.AddRange(token.Words);
                }

                return words;
            }
        }
        public override ISemanticType GetSemanticType()
        {
            if (Function is DotToken dotToken)
            {
                dotToken.GetSemanticType();
                if (dotToken.LeftType is ArrayType && SystemNames.ArraySizes.Contains(dotToken.Name))
                {
                    return SimpleType.Integer;
                }

                var subprogram = dotToken.GetSubprogram();
                if (subprogram != null)
                {
                    ValidateParameters(subprogram.ParametersWord);
                    return GetSubprogramType(subprogram);
                }
                
                GenerateFind(new ObjectIsNotSubprogram(
                    dotToken.RightOperand.Token.Operator, 
                    dotToken.RightOperand.Token.Words, 
                    dotToken.RightOperand.Token.Text));
            }
            if (Function is OperandToken operandToken)
            {
                if (operandToken.Name.Equals(SyntaxNames.New))
                {
                    const int formalCount = 1;
                    var actualCount = Arguments.Count;
                    for (var i = 0; i < actualCount; ++i)
                    {
                        var actualType = Arguments[i].GetSemanticType();
                        if (i < formalCount)
                        {
                            if (!actualType.IsUserType)
                            {
                                GenerateFind(new ArgumentHasIncorrectType(Token.Operator, Arguments[i].Words));
                            }
                        }
                    }

                    if (actualCount != formalCount)
                    {
                        GenerateFind(new IncorrectArgumentCount(Token.Operator, Function.Words, actualCount, formalCount));
                        return SimpleType.Undefined;
                    }

                    return new PointerType(null) {TypeWord = (SemanticItem) Arguments[0].GetSemanticType()};
                }

                if (operandToken.Name.StartsWith(SystemNames.SystemFuncDog))
                {
                    foreach (var token in Arguments)
                    {
                        token.GetSemanticType();
                    }
                        
                    return SimpleType.SystemType;
                }

                var name = Token.GetNameInSameScope(operandToken.Name);
                if (name is Subprogram subNameable)
                {
                    SemanticItem.AddUsages(name, Function.Token);
                    ValidateParameters(subNameable.ParametersWord);
                    return GetSubprogramType(subNameable);
                }

                if (name is TypeParameter {TypeWord: ProcedureType type})
                {
                    SemanticItem.AddUsages(name, Function.Token);
                    ValidateParameters(type.Parameters);
                    return SimpleType.Subprogram;
                }
                    
                GenerateFind(new UndeclaredSubprogram(Token.Operator, operandToken.Words, operandToken.Name));
            }
                
            return SimpleType.Undefined;
        }

        private void ValidateParameters(SemanticObservableContainer parameters)
        {
            var formalCount = parameters.ItemsCount;
            var actualCount = Arguments.Count;
            for (var i = 0; i < actualCount; ++i)
            {
                if (i > formalCount)
                {
                    // Что-то страное... Следует подумать над этим.
                    Arguments[i].GetSemanticType();
                }
                else
                {
                    var parameter = (TypeParameter)parameters[i];

                    if (Arguments[i] is OperandToken operandToken)
                    {
                        operandToken.IsOutArgument = parameter.ModeWord.Text == SyntaxNames.Out;
                    }

                    ISemanticType actualType = Arguments[i].GetSemanticType();
                    var formalType = (ISemanticType)parameter.TypeWord;
                    if (!actualType.CanCastTo(formalType, true))
                    {
                        var name = parameter is Parameter typeParameter
                                        ? typeParameter.NameWord.Text
                                        : "№" + (i + 1);
                    }

                    if (parameter.ModeWord.Text != SyntaxNames.In && !Arguments[i].IsLValue())
                    {
                        GenerateFind(new ValueUsedAsOutArgument(Token.Operator, Arguments[i].Words));
                    }
                }
            }

            if (actualCount != formalCount)
            {
                GenerateFind(new IncorrectArgumentCount(Token.Operator, Function.Words, actualCount, formalCount));
            }
        }
    }
}