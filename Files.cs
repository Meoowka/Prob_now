using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Prob_now
{
    public class Files
    {
        
        public string Name_File { get; set; }
        public string? Versio { get; set; }
        public DateTime? Date { get; set; }
        public string? Autor_ver { get; set; }
        public string Extension { get; set; }

        public string Id_ver { get; set; }

       
        public Files(string name_file, string version, DateTime date, string autor_ver, string extension) {
            Name_File = name_file;
            Versio = version;
            Date = date;
            Autor_ver = autor_ver;
            Extension = extension;
        
            Id_ver = name_file + autor_ver + extension;

        }
    }
}
