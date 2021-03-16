namespace Semantic.Solution
{
    // TODO: Переписать в интерфейс
    /// <summary>
    ///     Базовый класс команды
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        ///     Выполнить команду
        /// </summary>
        internal abstract void Execute();

        /// <summary>
        ///     Отменить действие
        /// </summary>
        internal abstract void Undo();
    }
}