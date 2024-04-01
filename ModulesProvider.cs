using Prob_now.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prob_now.Entities;
using Aspose.Html.Toolkit.Markdown.Syntax;
using Prob_now;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;


namespace Prob_now
{
    public class ModulesProvider : IModulesProvider
    {
        public Module[] ListModuleInfo(Guid id, DirectoryInfo entrypoint)
        {
            List<Module> modules = new List<Module>();
            foreach (FileInfo FL in entrypoint.GetFiles("*.*", System.IO.SearchOption.AllDirectories))
            {
                var fvi = FileVersionInfo.GetVersionInfo(FL.FullName);
                string? autor_r = fvi.CompanyName;
                string? version = fvi.FileVersion;
                var item_file_on_disk = new Module(FL.Name.Split('.')[0],
                            version,
                            FL.CreationTime,
                            FL.Extension);


                if (id.ToString() == item_file_on_disk.Id_ver)
                {
                    var files = ListModules(entrypoint);
                    for (int i = 0; i < files.Count(); i++)
                    {
                        if (files[i].Name_File.Contains(item_file_on_disk.Name_File))
                        {
                            modules.Add(item_file_on_disk);
                        }
                    }
                }
            }
                return modules.ToArray();
        }

        public Module GetModule(Guid id, DirectoryInfo entrypoint)
        {
            var modules = ListModules(entrypoint);

            return modules.Where(x => x.Equals(id.ToString())).FirstOrDefault();
        }


        public Module[] ListModules(DirectoryInfo entrypoint)
        {
            List<Module> modules = new List<Module>();
            List<string> list = new List<string>();
            foreach (FileInfo FL in entrypoint.GetFiles("*.*", System.IO.SearchOption.AllDirectories))
            {
                var fvi = FileVersionInfo.GetVersionInfo(FL.FullName);
                string? autor_r = fvi.CompanyName;
                string? version = fvi.FileVersion;

                if (version != null)
                {
                    list.Add(version);
                }

                var item_file_on_disk = new Module(FL.Name.Split('.')[0],
                            version,
                            FL.CreationTime,
                            FL.Extension);
                modules.Add(item_file_on_disk);
            }
            CreateMd(list);     //side effect
            return modules.ToArray();

        }
        private void CreateMd(List<string> file_md)
        {
            const string file_path = @"E:\";
            var md = new MarkdownSyntaxTree(new Aspose.Html.Configuration());
            var mdf = md.SyntaxFactory;
            for (int i = 0; i < file_md.Count; i++)
            {
                var head = mdf.AtxHeading(file_md[i] + '\n');
                md.AppendChild(head);
            }
            string savePath = Path.Combine(file_path, "version.md");
            md.Save(savePath);
        }



    }
}
        

            

           
        







