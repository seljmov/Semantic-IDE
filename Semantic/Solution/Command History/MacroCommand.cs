using System.Collections.Generic;

namespace Semantic.Solution
{
    public class MacroCommand
    {
        private readonly List<Command> _commands = new();
        private int _endOffset;
        private SemanticItem? _endSelectedItem;
        private int _startOffset;
        private SemanticItem? _startSelectedItem;
        private IdeCommand _ideCommand;

        internal void Redo()
        {
            foreach (var command in _commands)
            {
                command.Execute();
            }

            if (_ideCommand is IModificatorCommand modificatorCommand)
            {
                modificatorCommand.Rescan();
            }
            
            if (_endSelectedItem != null)
            {
                SemanticCursor.Instance.Focus(_endSelectedItem);
                SemanticCursor.Instance.SetOffset(_endOffset);
            }
        }

        internal void Undo()
        {
            foreach (var command in _commands)
            {
                command.Undo();
            }

            if (_ideCommand is IModificatorCommand modificatorCommand)
            {
                modificatorCommand.Rescan();
            }

            if (_startSelectedItem != null)
            {
                SemanticCursor.Instance.Focus(_startSelectedItem);
                SemanticCursor.Instance.SetOffset(_startOffset);
            }
        }

        internal void AddCommand(Command command)
        {
            if (_commands.Count == 0)
            {
                _startSelectedItem = SemanticCursor.Instance.CurrentItem;
                _startOffset = SemanticCursor.Instance.Offset;
                _ideCommand = CommandsStack.Top();
            }
            
            _commands.Add(command);
        }

        internal bool IsEmpty() => _commands.Count == 0;

        internal void SaveCursor()
        {
            _endSelectedItem = SemanticCursor.Instance.CurrentItem;
            _endOffset = SemanticCursor.Instance.Offset;
        }
    }
}