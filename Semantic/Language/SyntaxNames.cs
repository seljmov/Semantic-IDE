using System.Collections.Generic;

/// <summary>
///     Названия элементов синтаксиса
/// </summary>
public static class SyntaxNames
{
    public static List<string> ProhibitedNames { get; internal set; }
    public static string Module { get; internal set; }
    public static string Type { get; internal set; }
    public static string Function { get; internal set; }
    public static string Return { get; internal set; }
    public static string Procedure { get; internal set; }
    public static string Array { get; internal set; }
    public static string Variable { get; internal set; }
    public static string InitializedVariable { get; internal set; }
    public static string Constant { get; internal set; }
    public static string Call { get; internal set; }
    public static string Assign { get; internal set; }
    public static string If { get; internal set; }
    public static string Else { get; internal set; }
    public static string ElseIf { get; internal set; }
    public static string Beginning { get; internal set; }
    public static string While { get; internal set; }
    public static string Loop { get; internal set; }
    public static string Output { get; internal set; }
    public static string Input { get; internal set; }
    public static string Field { get; internal set; }
    public static string MethodFunction { get; internal set; }
    public static string MethodProcedure { get; internal set; }
    public static string DoNTimes { get; internal set; }
    public static string Text { get; internal set; }
    public static string KeyWordText { get; internal set; }
    public static string Import { get; internal set; }
    public static string Comment { get; internal set; }
    public static string EmptyOperator { get; internal set; }
    public static List<string> Keywords { get; internal set; }
    public static string Integer { get; set; }
    public static string Real { get; set; }
    public static string Boolean { get; set; }
    public static string String { get; set; }
    public static string Character { get; set; }
    public static string Undefined { get; set; }
    public static string Pointer { get; set; }
    public static string NullPointer { get; set; }
    public static List<string> Types { get; set; }
    public static string True { get; internal set; }
    public static string False { get; internal set; }
    public static List<string> Booleans { get; internal set; }
    public static string OrOperation { get; internal set; }
    public static string AndOperation { get; internal set; }
    public static string EqualOperation { get; internal set; }
    public static string NotEqualOperation { get; internal set; }
    public static string GreaterOperation { get; internal set; }
    public static string GreaterOrEqualOperation { get; internal set; }
    public static string LessOperation { get; internal set; }
    public static string LessOrEqualOperation { get; internal set; }
    public static string PlusOperation { get; internal set; }
    public static string MinusOperation { get; internal set; }
    public static string MultiplyOperation { get; internal set; }
    public static string DivideOperation { get; internal set; }
    public static string IntegerDivideOperation { get; internal set; }
    public static string ModOperation { get; internal set; }
    public static string IsOperation { get; internal set; }
    public static string AsOperation { get; internal set; }
    public static List<string> BinaryOperations { get; internal set; }
    public static string UnaryMinusOperation { get; internal set; }
    public static string NotOperation { get; internal set; }
    public static List<string> UnaryOperations { get; internal set; }
    public static string Private { get; internal set; }
    public static string Public { get; internal set; }
    public static string Readonly { get; internal set; }
    public static string In { get; internal set; }
    public static string Ref { get; internal set; }
    public static string Out { get; internal set; }
    public static string EndText { get; internal set; }
    public static string New { get; internal set; }
    public static string Nil { get; internal set; }
    public static string Delete { get; internal set; }

    public static string SystemType { get; set; }
}