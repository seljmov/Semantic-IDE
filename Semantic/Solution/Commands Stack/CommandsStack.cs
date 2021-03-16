using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public static class CommandsStack
    {
        private static readonly Stack<IdeCommand> _commands = new();

        public static IdeCommand Top() => _commands.FirstOrDefault();
    }
}