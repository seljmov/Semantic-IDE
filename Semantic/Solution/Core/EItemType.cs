using System;

namespace Semantic.Solution
{
    /// <summary>
    ///     Тип слова
    /// </summary>
    [Flags]
    public enum EItemType : short
    {
        /// <summary>
        ///     Ключевое слово
        /// </summary>
        KeyWord = 1,

        /// <summary>
        ///     Общий элемент
        /// </summary>
        General = 2,

        /// <summary>
        ///     Слово "конец"
        /// </summary>
        End = 4,

        /// <summary>
        ///     Слово, повторяющее имя оператора
        /// </summary>
        EndName = 8,

        /// <summary>
        ///     Имя элемента
        /// </summary>
        Name = 16,

        /// <summary>
        ///     Элемент, по которому вставляется дочерний элемент при нажатии Enter
        /// </summary>
        Body = 32,

        /// <summary>
        ///     Тип элемента
        /// </summary>
        Type = 64,

        /// <summary>
        ///     Первое слово в операторе
        /// </summary>
        First = 128,

        /// <summary>
        ///     Модификатор параметра (константный/переменный)
        /// </summary>
        ModeModifier = 256,

        /// <summary>
        ///     Модификаторы видимости (открытый/закрытый)
        /// </summary>
        VisibilityModifier = 512,

        /// <summary>
        ///     Параметр класса
        /// </summary>
        ClassParameter = 1024,
        FakeWord = 2048
    }
}