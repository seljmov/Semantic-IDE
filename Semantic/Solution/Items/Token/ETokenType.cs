namespace Semantic.Solution
{   
    /// <summary>
    ///     Перечисление типов лексем
    /// </summary>
    public enum ETokenType
    {
        /// <summary>
        ///     Операнд
        /// </summary>
        Operand,

        /// <summary>
        ///     Бинарная операция
        /// </summary>
        BinaryOperation,

        /// <summary>
        ///     Унарная операция
        /// </summary>
        UnaryOperation,

        /// <summary>
        ///     Левая круглая скобка
        /// </summary>
        LeftBracket,

        /// <summary>
        ///     Правая круглая скобка
        /// </summary>
        RightBracket,

        /// <summary>
        ///     Левая квадратная скобка
        /// </summary>
        LeftIndex,

        /// <summary>
        ///     Правая квадратная скобка
        /// </summary>
        RightIndex,

        /// <summary>
        ///     Левая скобка функции
        /// </summary>
        LeftFuncBracket,

        /// <summary>
        ///     Правая скобка функции
        /// </summary>
        RightFuncBracket,

        /// <summary>
        ///     Пробел
        /// </summary>
        Space,

        /// <summary>
        ///     Функция
        /// </summary>
        Function,

        /// <summary>
        ///     Запятая
        /// </summary>
        Comma,

        /// <summary>
        ///     Имя массива
        /// </summary>
        Array,

        /// <summary>
        ///     Точка (область видимости)
        /// </summary>
        Dot,

        /// <summary>
        ///     Операция индексации
        /// </summary>
        Indexation,

        /// <summary>
        ///     Операция вызова функции
        /// </summary>
        Call,

        /// <summary>
        ///     Лексема, полученная искусственно в результате анализа
        /// </summary>
        Fake,

        /// <summary>
        ///     Символ, который инициализирует вставку функции DLR
        ///     Хранится в ...
        /// </summary>
        SystemSymbol
    }
}