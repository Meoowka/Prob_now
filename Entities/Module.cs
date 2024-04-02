using System;
using System.IO;

namespace Prob_now.Entities
{
    public class Module
    {

        public string Name_File { get; set; }
        public string? Versio { get; set; }
        public DateTime? Date { get; set; }
        public string Extension { get; set; }
        public string Id_ver { get; set; }
        public DirectoryInfo Directory { get; set; }
        public Module(DirectoryInfo directory)
        {
            Directory = directory;
        }
        public Module(string name_file, string version, DateTime date, string extension)
        {
            Name_File = name_file;
            Versio = version;
            Date = date;
            Extension = extension;
            Id_ver = name_file + extension;
        }
       

    }
}
