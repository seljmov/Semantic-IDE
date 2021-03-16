using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ArgumentHasIncorrectType : SemanticError
    {
        public ArgumentHasIncorrectType(SemanticOperator @operator, List<Word> items) 
            : base(@operator, items)
        {
            Text = string.Format(Strings.NewFunctionParameterMustBeType, SyntaxNames.New);
        }
        
        public ArgumentHasIncorrectType(SemanticOperator @operator, List<Word> items, 
            string name, string formalType, string actualType) 
            : base(@operator, items)
        {
            ParameterName = name;
            FormalType = formalType;
            ActualType = actualType;

            Text = string.Format(Strings.ParameterHasTypeActualType, ParameterName, FormalType, ActualType);
        }
        
        public string ParameterName { get; set; }
        public string FormalType { get; set; }
        public string ActualType { get; set; }
    }
}