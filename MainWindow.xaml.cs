using Aspose.Html.Accessibility;
using Aspose.Html.Toolkit.Markdown.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace Prob_now
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public ObservableCollection<Files> dir_r { get; set; }

        public void Create_md(List<string> file_md)
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
       public List<Files> list_files = new List<Files>();

        public void Directory_info(DirectoryInfo entrypoint)
        {
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
                
                var item_file_on_disk = new Files(FL.Name.Split('.')[0],
                            version,
                            FL.CreationTime,
                            autor_r,
                            FL.Extension);
                list_files.Add(item_file_on_disk);
                List_view.Items.Add(item_file_on_disk);
            }
            Create_md(list);
        }
        public void File_info_on_version(DirectoryInfo entrypoint, string ID_inf)
        {
            
            foreach (FileInfo FL in entrypoint.GetFiles("*.*", System.IO.SearchOption.AllDirectories))
            {
                var fvi = FileVersionInfo.GetVersionInfo(FL.FullName);
                string? autor_r = fvi.CompanyName;
                string? version = fvi.FileVersion;
                var item_file_on_disk = new Files(FL.Name.Split('.')[0],
                            version,
                            FL.CreationTime,
                            autor_r,
                            FL.Extension);

                if (ID_inf == item_file_on_disk.Id_ver)
                {
                    for (int i = 0; i < list_files.Count; i++)
                    { 
                        if (list_files[i].Name_File.Contains(item_file_on_disk.Name_File))
                        {
                            List_view_info.Items.Add(item_file_on_disk);
                        }

                    }
                }

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List_view.Items.Clear();
            DirectoryInfo currentdirectory = new DirectoryInfo(@"E:\Folder_update");
            Directory_info(currentdirectory);
        }

        private void List_view_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            List_view_info.Items.Clear();
            var point = List_view.SelectedItem as Files;
            DirectoryInfo currentdirectory = new DirectoryInfo(@"E:\Folder_update"); 
            File_info_on_version(currentdirectory, point.Id_ver);
        }
    }
}
