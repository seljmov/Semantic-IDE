using System.Collections.Generic;

namespace Semantic.Solution
{
    /// <summary>
    ///     Интерфейс классов операторов имеющих имя
    /// </summary>
    public interface INameable
    {
        /// <summary>
        ///     Имя оператора
        /// </summary>
        Word NameWord { get; }
        HashSet<Word> Usages { get; }
    }
}
