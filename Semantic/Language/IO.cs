using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Semantic.Language
{
    public static class IO
    {
        private static string _pathName = "\\Semantic IDE\\";
#if INSTALLER
        public static DirectoryInfo RootDirectory =
            new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + _pathName);
#else
        public static DirectoryInfo RootDirectory = new DirectoryInfo(Environment.CurrentDirectory + _pathName);
#endif

        public static Guid GetModuleId(FileInfo info)
        {
            try
            {
                var reader = XmlReader.Create(new MemoryStream(Encoding.Unicode.GetBytes(info.OpenText().ReadToEnd())));
                reader.ReadToFollowing("Root");
                var attr = reader.GetAttribute("Id");
                return attr != null ? Guid.Parse(attr) : Guid.Empty;
            }
            catch
            {
                return Guid.Empty;
            }
        }
        
        public static bool IsInstaller()
        {
#if INSTALLER
            return true;
#else
            return false;
#endif
        }
    }
}