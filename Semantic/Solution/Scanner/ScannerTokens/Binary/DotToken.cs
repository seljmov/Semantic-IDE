using System.Linq;

namespace Semantic.Solution
{
    public class DotToken : BinaryOperationToken
    {
        public DotToken(Token token, ScanToken leftOperand, ScanToken rightOperand) 
            : base(token, leftOperand, rightOperand)
        {
        }

        public string Name { get; set; }
        
        protected override ISemanticType ReturnType()
        {
            var @operator = GetOperator();
            if (@operator != null)
            {
                CheckVisibility(@operator, Name, LeftType.FullType);
                AddUsages(@operator);
                return GetItemType(@operator);
            }

            if (LeftType is ArrayType && SystemNames.ArraySizes.Contains(Name))
            {
                return SimpleType.Integer;
            }
            
            return SimpleType.Undefined;
        }

        protected override bool OperationIsCorrect()
        {
            if (LeftOperand.IsRecord || SyntaxNames.Types.Contains(LeftType.FullType)
                                     || LeftType.FullType.Equals(SyntaxNames.String))
            {
                GenerateFind(new IncorrectDotUsage(Token.Operator, Token.Words, LeftType.FullType));
            }

            return true;
        }

        protected override void ConvertOperands()
        {
            LeftType = LeftOperand.GetSemanticType();
            if (RightOperand is OperandToken operand)
            {
                Name = operand.Name;
            }
        }

        private SemanticOperator GetOperatorFromSimpleType(SimpleType leftType)
        {
            if (StringAnalyzer.IsClassOrModuleMember(leftType.Text))
            {
                var classOrModuleName = StringAnalyzer.GetClassOrModuleName(leftType.Text);
                var memberName = StringAnalyzer.GetMemberName(leftType.Text);

                if (Token.Tree.Project.HasModuleWithName(classOrModuleName))
                {
                    var tree = Token.Tree.Project.GetModuleByName(classOrModuleName);
                    var record = tree.GetDeclaredTypes().FirstOrDefault(x => x.NameWord.Text == memberName);
                    // record?.
                    if (record != null)
                    {
                        var member = record.GetMembers()
                            .FirstOrDefault(x => ((INameable) x).NameWord.Text.Equals(Name));

                        if (member != null)
                        {
                            return member;
                        }
                        
                        SemanticError error = null;
                        if (Parent is FunctionToken)
                        {
                            error = new ClassMissingMethod(Token.Operator, RightOperand.Words, record, Name);
                        }
                        else if (Parent is ArrayToken)
                        {
                            error = new ClassMissingArray(Token.Operator, RightOperand.Words, record, Name);
                        }
                        else
                        {
                            error = new ClassMissingField(Token.Operator, RightOperand.Words, record, Name);
                        }
            
                        GenerateFind(error);
                    }
                }
            }
            else
            {
                var record = Token.Tree.GetDeclaredTypes()
                    .FirstOrDefault(x => x.NameWord.Text.Equals(leftType.Text));

                if (record != null)
                {
                    var member = record.GetMembers()
                        .FirstOrDefault(x => ((INameable) x).NameWord.Text.Equals(Name));

                    if (member != null)
                    {
                        return member;
                    }
                        
                    SemanticError error = null;
                    if (Parent is FunctionToken)
                    {
                        error = new ClassMissingMethod(Token.Operator, RightOperand.Words, record, Name);
                    }
                    else if (Parent is ArrayToken)
                    {
                        error = new ClassMissingArray(Token.Operator, RightOperand.Words, record, Name);
                    }
                    else
                    {
                        error = new ClassMissingField(Token.Operator, RightOperand.Words, record, Name);
                    }
            
                    GenerateFind(error);
                }
                else
                {
                    var import = Token.Tree.GetImports()
                        .FirstOrDefault(x => x.NameWord.Text.Equals(leftType.Text));

                    if (import != null)
                    {
                        if (!Token.Tree.Project.HasModuleWithName(import.NameWord.Text))
                        {
                            GenerateFind(new UndeclaredModule(Token.Operator, LeftOperand.Words, import.NameWord.Text));
                        }
                        else
                        {
                            var tree = Token.Tree.Project.GetModuleByName(import.NameWord.Text);
                            var module = tree.FindModule();

                            if (module != null)
                            {
                                SemanticItem.AddUsages(module, LeftOperand.Token);
                                var member = tree.GetMembers()
                                    .FirstOrDefault(x => ((INameable) x).NameWord.Text.Equals(Name));

                                if (member != null)
                                {
                                    return member;
                                }
                                
                                SemanticError error = null;
                                if (Parent is FunctionToken)
                                {
                                    error = new ModuleMissingFunction(Token.Operator, RightOperand.Words, tree.FindModule(), Name);
                                }
                                else if (Parent is ArrayToken)
                                {
                                    error = new ModuleMissingArray(Token.Operator, RightOperand.Words, tree.FindModule(), Name);
                                }
                                else
                                {
                                    error = new ModuleMissingVariable(Token.Operator, RightOperand.Words, tree.FindModule(), Name);
                                }
            
                                GenerateFind(error);
                            }
                        }
                    }
                }
            }

            return null;
        }

        private void CheckVisibility(SemanticOperator member, string memberName, string classOrModuleName)
        {
            if (member is IHaveVisibility haveVisibility)
            {
                var visibility = haveVisibility.VisibilityWord;
                if (visibility.Equals(SyntaxNames.Private))
                {
                    if (!Token.Operator.IsChild(member.FindParent()))
                    {
                        GenerateFind(new AccessToPrivateMember(Token.Operator, RightOperand.Words, member, memberName,
                            classOrModuleName));
                    }
                }
            }
        }

        private SemanticOperator GetOperator()
        {
            if (LeftType is PointerType)
            {
                var pointerType = (PointerType) LeftType;
                if (pointerType.TypeWord is SimpleType)
                {
                    return GetOperatorFromSimpleType((SimpleType) pointerType.TypeWord);
                }
            }
            else if (LeftType is SimpleType)
            {
                return GetOperatorFromSimpleType((SimpleType) LeftType);
            }

            if (!(LeftType is ArrayType) || !SystemNames.ArraySizes.Contains(Name))
            {
                GenerateFind(new IncorrectDotUsage(Token.Operator, Token.Words, LeftType.FullType));
            }

            return null;
        }

        private void AddUsages(SemanticOperator @operator)
        {
            if (@operator is INameable nameable)
            {
                SemanticItem.AddUsages(nameable, RightOperand.Token);
            }
        }

        public Subprogram GetSubprogram()
        {
            var @operator = GetOperator();
            return (@operator is Subprogram subprogram) ? subprogram : null;
        }
    }
}