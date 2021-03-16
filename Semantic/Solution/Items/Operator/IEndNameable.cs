namespace Semantic.Solution
{
    /// <summary>
    ///     Интерфейс классов, имеющих повторяющееся в конце имя,
    ///     после слова "конец"
    /// </summary>
    public interface IEndNameable : INameable
    {
        /// <summary>
        ///     Имя оператора, повторяющееся после слова "конец"
        /// </summary>
        Word EndNameWord { get; }
    }
}