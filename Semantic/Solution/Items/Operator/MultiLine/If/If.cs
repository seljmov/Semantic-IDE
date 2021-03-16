using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semantic.Solution
{
    public class If : MultiLineOperator
    {
        public If()
        {
            Expression = (Expression) _items.First(x => x is Expression);
            ElseIfList = new List<ElseIf>();
            Else = null;
        }

        public Expression Expression { get; private set; }
        public List<ElseIf> ElseIfList { get; private set; }
        public Else? Else { get; internal set; }

        public override string ItemName => SyntaxNames.If;

        internal override void Scan(bool scanNext)
        {
            for (var i = 0; i < _items.Count; ++i)
            {
                SemanticFind find = _errors[i];
                if (find.Operator == this)
                {
                    _errors.Remove(find);
                    --i;
                }
            }
            
            _initializedNames.Push(new List<string>());
            ISemanticType rightType = Expression.GetSemanticType();
            ISemanticType leftType = SimpleType.Boolean;
            if (!rightType.CanCastTo(leftType, false))
            {
                GenerateFind(new InCorrectExpressionType(Operator, Expression.Words, leftType.FullType, rightType.FullType));
            }

            if (Child != null)
            {
                Child.Scan(true);
            }

            var ifNames = new HashSet<string>();
            ifNames.UnionWith(_initializedNames.Pop());

            foreach (var elseIf in ElseIfList)
            {
                _initializedNames.Push(new List<string>());
                elseIf.Scan(true);
                ifNames.IntersectWith(_initializedNames.Pop());
            }

            if (Else != null)
            {
                _initializedNames.Push(new List<string>());
                Else.Scan(true);
                ifNames.IntersectWith(_initializedNames.Pop());
            }
            else
            {
                ifNames.Clear();
            }

            if (_initializedNames.Count > 0)
            {
                _initializedNames.First().AddRange(ifNames);
            }

            if (Next != null && scanNext)
            {
                Next.Scan(true);
            }
        }

        public override void CheckControlFlow()
        {
            if (_returnsBranches.Count > 0 && _returnsBranches.First().HasReturn)
            {
                GenerateFind(new UnreachableCode(this, Words));
            }

            if (_returnsBranches.Count > 0)
            {
                _returnsBranches.Push(new Branch {SemanticOperator = this, HasReturn = false});
            }

            if (Child != null)
            {
                Child.CheckControlFlow();
            }

            foreach (var elseIf in ElseIfList)
            {
                elseIf.CheckControlFlow();
            }

            if (Else != null)
            {
                Else.CheckControlFlow();
            }

            if (_returnsBranches.Count > 0)
            {
                bool hasReturn = _returnsBranches.Pop().HasReturn;
                Branch branch = _returnsBranches.First();
                branch.HasReturn = Else != null & hasReturn;
            }

            if (Next != null)
            {
                Next.CheckControlFlow();
            }
        }

        private bool HasElse() => Else != null;

        internal void AddNullOperatorToElseOrElseIf(SemanticOperator newOperator)
            => Tree.InsertOperatorAsChild(new NullOperator(), newOperator);

        internal void AddElseIf(ElseIf newElseIf)
            => Tree.AddCommandAndExecute(new AddElseIfCommand(this, newElseIf));

        internal void AddElse(Else newElse)
            => Tree.AddCommandAndExecute(new AddElseCommand(this, newElse));

        internal void InsertElseIfAsPrevious(ElseIf newElseIf, ElseIf existedElseIf)
            => Tree.AddCommandAndExecute(new InsertElseIfAsPreviousCommand(this, newElseIf, existedElseIf));

        internal void InsertElseIfAsNext(ElseIf newElseIf, ElseIf existedElseIf)
            => Tree.AddCommandAndExecute(new InsertElseIfAsNextCommand(this, newElseIf, existedElseIf));

        internal void InsertElseIfBeforeElse(ElseIf newElseIf, Else existedElse)
            => Tree.AddCommandAndExecute(new InsertElseIfBeforeElseCommand(this, newElseIf, existedElse));

        internal void DeleteElseIf(ElseIf existedElseIf)
            => Tree.AddCommandAndExecute(new DeleteElseIfCommand(this, existedElseIf));

        internal void DeleteElse(Else existedElse)
            => Tree.AddCommandAndExecute(new DeleteElseCommand(this, existedElse));

        public override List<string> ToOpsString()
        {
            var opsString = new List<string>() {ItemName};

            if (Child != null)
            {
                opsString.AddRange(Child.ToOpsString());
            }

            foreach (var elseIf in ElseIfList)
            {
                opsString.AddRange(elseIf.ToOpsString());
            }

            if (Else != null)
            {
                opsString.AddRange(Else.ToOpsString());
            }

            if (Next != null)
            {
                opsString.AddRange(Next.ToOpsString());
            }
            
            return opsString;
        }

        public override void CalculateNumber()
        {
            _counter++;
            Number = _counter;

            if (Child != null)
            {
                Child.CalculateNumber();
            }

            foreach (var elseIf in ElseIfList)
            {
                elseIf.CalculateNumber();
            }

            if (Else != null)
            {
                Else.CalculateNumber();
            }

            if (Next != null)
            {
                Next.CalculateNumber();
            }
        }

        public override string PrettyPrinter(EPrettyPrinterMode mode = EPrettyPrinterMode.WithBody,
            bool printNext = false, int tabLevel = 0)
        {
            SemanticItem bodyItem = _items.LastOrDefault(x => x.HasType(EItemType.Body)) ?? _items.Last();

            var firstLine = "";
            for (var i = 0; i < tabLevel; ++i)
            {
                firstLine += SystemNames.Tab;
            }

            for (var i = 0; i < _items.Count && _items[i] != bodyItem; ++i)
            {
                firstLine += _items[i].PrettyPrinter();
            }

            firstLine += bodyItem.PrettyPrinter();

            var childText = "";
            if (Child != null)
            {
                childText = "\n" + Child.PrettyPrinter(mode, true, tabLevel + 1);
            }

            foreach (var elseIf in ElseIfList)
            {
                childText += "\n" + elseIf.PrettyPrinter(mode, true, tabLevel);
            }

            if (Else != null)
            {
                childText += "\n" + Else.PrettyPrinter(mode, true, tabLevel);
            }

            var lastLine = "";
            if (bodyItem != _items.Last())
            {
                lastLine += "\n";
                for (var i = 0; i < tabLevel; ++i)
                {
                    lastLine += SystemNames.Tab;
                }

                var index = _items.IndexOf(bodyItem);
                for (var i = index + 1; i < _items.Count; ++i)
                {
                    lastLine += _items[i].PrettyPrinter();
                }
            }

            var nextText = "";
            if (printNext && Next != null)
            {
                nextText = "\n" + Next.PrettyPrinter(mode, true, tabLevel);
            }

            return mode switch
            {
                EPrettyPrinterMode.FirstLine => firstLine,
                EPrettyPrinterMode.LastLine => lastLine,
                EPrettyPrinterMode.WithoutBody => firstLine + lastLine,
                EPrettyPrinterMode.WithBody => firstLine + childText + lastLine + nextText,
                _ => null,
            };
        }

        public override SemanticItem GetLastOperator(SemanticOperator @operator)
        {
            if (@operator != Child)
            {
                SemanticOperator current;
                if (Else != null && !ElseIfList.Contains(@operator) && Else != @operator)
                {
                    current = Else;
                }
                else if (ElseIfList.Count > 0 && !ElseIfList.Contains(@operator))
                {
                    current = ElseIfList.First();
                }
                else
                {
                    current = Child;
                }

                if (current != null && current != @operator && !IsCollapsed)
                {
                    while (current.Next != null)
                    {
                        current = current.Next;
                    }

                    return current.GetLastOperator(@operator);
                }
            }

            return this;
        }
    }
}