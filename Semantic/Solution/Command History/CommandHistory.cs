using System;
using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class CommandHistory
    {
        private readonly List<MacroCommand> _macroCommands = new();
        private int _currentIndex = -1;

        public CommandHistory()
        {
            Clear();
        }

        internal void Clear()
        {
            _macroCommands.Clear();
            _currentIndex = -1;
            _macroCommands.Add(new MacroCommand());
        }

        internal void AddCommand(Command command) => _macroCommands.Last().AddCommand(command);

        internal void Redo()
        {
            if (_currentIndex != _macroCommands.Count - 2)
            {
                try
                {
                    _currentIndex++;
                    _macroCommands[_currentIndex].Redo();
                }
                catch (Exception e)
                {
                    // TODO: write here Crushes
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        internal void Undo()
        {
            if (_currentIndex != _macroCommands.Count - 2)
            {
                try
                {
                    _macroCommands[_currentIndex].Undo();
                    _currentIndex--;
                }
                catch (Exception e)
                {
                    // TODO: write here Crushes
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        internal void Save()
        {
            if (IsLastCommandIsEmpty())
            {
                return;
            }

            if (IsCurrentCommandIsEmpty())
            {
                AddNewMacroCommand();
            }
            else
            {
                RemoveForgottenCommands();
                AddNewMacroCommand();
            }
        }

        private bool IsLastCommandIsEmpty() => _macroCommands.Last().IsEmpty();
        
        private bool IsCurrentCommandIsEmpty() => _currentIndex == _macroCommands.Count - 2;

        private void RemoveForgottenCommands()
            => _macroCommands.RemoveRange(_currentIndex + 1, _macroCommands.Count - _currentIndex - 2);

        private void AddNewMacroCommand()
        {
            var last = _macroCommands.Last();
            if (last != null)
            {
                last.SaveCursor();
            }
            
            _macroCommands.Add(new MacroCommand());
            _currentIndex++;
        }
    }
}