using Prob_now.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Prob_now.Entities;
using Aspose.Html.Toolkit.Markdown.Syntax;
using System.Diagnostics;
using System.IO;
using System.Net;
using FluentFTP;
using GroupDocs.Parser;
using GroupDocs.Parser.Data;


namespace Prob_now
{
    public class ModulesProvider : IModulesProvider
    {
        DirectoryInfo entrypoint = new DirectoryInfo(@"E:\Folder_updates");
        FtpConnectionHandler ftp = new FtpConnectionHandler();

        List<Module> modules = new List<Module>();
        List<Module> modules_ftp = new List<Module>();
        List<Module> mod = new List<Module>();

        string path_load = "E:\\Folder_updates";
        string path_download = "/AutoUp/";
       

        public Module[] ListModuleInfo(Guid id)
        {
            
           
            foreach (FileInfo FL in  entrypoint.GetFiles("*.*", System.IO.SearchOption.AllDirectories))
            {
                var fvi = FileVersionInfo.GetVersionInfo(FL.FullName);
                string? version = fvi.FileVersion;
                var item_file_on_disk = new Module(FL.Name.Split('.')[0],
                            version,
                            FL.CreationTime,
                            FL.Extension);


                if (id.ToString() == item_file_on_disk.Id_ver)
                {
                    var files = ListModules();
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
        public Module GetModule(Guid id)
        {
            var modules = ListModules();

            return modules?.Where(x => x.Equals(id.ToString())).FirstOrDefault();
        }
        public Module[] ListModules()
        {
          
            List<string> list = new List<string>();
            foreach (FileInfo FL in entrypoint.GetFiles("*.*"))
            {
                var fvi = FileVersionInfo.GetVersionInfo(FL.FullName);

                string? version = fvi.FileVersion;

                var item_file_on_disk = new Module(FL.Name.Split('.')[0],
                            version,
                            FL.CreationTime,
                            FL.Extension);
                modules.Add(item_file_on_disk);

                if (version != null)
                {
                    list.Add(FL.Name + '\t' + version);
                }
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



        public FtpConnectionHandler GetFtp()
        {
            ftp.ConnectFtp("192.168.56.1", new NetworkCredential("Meoowka","020509"));
            return ftp;
            
        }

        public Module[] Listing_ftp()
        {
            var listing = ftp.client.GetListing("", FtpListOption.Recursive);
            foreach (var item in listing)
                {
                    try
                    { 
                    var file_extension = Path.GetExtension(item.FullName);
                        var file_siz = ftp.client.GetFileSize(item.FullName);
                        var file_size = file_siz / 1024 / 1024;

                    switch (item.Type)
                        {
                            case FtpObjectType.File:
                           
                            var item_file_on_disk = new Module(item.Name.Split('.')[0],
                                file_size.ToString(), ftp.client.GetModifiedTime(item.FullName),
                                file_extension);
                                modules_ftp.Add(item_file_on_disk);
                                break;
                        }

                    }
                catch (UnauthorizedAccessException) { }
                }
            return modules_ftp.ToArray();
        }



        public object Ftp_load(string f)
        {
            return ftp.client.DownloadFile(path_load+"\\"+f,path_download+"/"+f);
        }
    }
}
        

            

           
        







