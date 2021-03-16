using System.Collections.Generic;

namespace Semantic.Solution
{
    public class UndeclaredModule : SemanticError
    {
        // Наследовать UndeclaredObject, ибо он наследует SemanticError, ибо как-то некрасиво выходит
        public UndeclaredModule(SemanticOperator @operator, List<Word> items, string module) 
            : base(@operator, items)
        {
            Text = string.Format(Strings.ModuleIsNotDeclared, module, @operator.Tree.Project.Name);
        }
    }
}