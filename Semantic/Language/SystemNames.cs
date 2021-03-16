using System.Collections.Generic;

public static class SystemNames
{
    public const string StringsTypeNameRussian = "Строки.Строка";
    public const string StringsTypeNameEnglish = "Strings.String";

    public static List<string> ArraySizes { get; } = new()
    {
        ArraySizeRussian,
        ArraySizeEnglish,
    };

    public static List<string> StringsTypeNames { get; } = new()
    {
        StringsTypeNameRussian,
        StringsTypeNameEnglish,
    };

    public static string SystemFuncDog => "@";

    public static string BasePostfix => "База";

    public static string LayoutFileName => "layout.txt";
    
    public static string DefaultLayoutFileName => "default_layout.txt"; 
    
    public static string RecentProjectsFileName => "recent_projects.txt";

    public static string ProjectExtension => "prj";
    
    public static string ModuleExtension => "sl";
    
    public static string RefreshForm => "RefreshDrawingForm";
    
    public static string Eof => "#EOF";
    
    public static string ArraySizeRussian => "размер";
    
    public static string ArraySizeEnglish => "size";
    
    public static string Tab => "    ";
}