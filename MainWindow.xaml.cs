using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public void recursiveBuildDirectory(DirectoryInfo entrypoint)
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
                            List_view.Items.Add(item_file_on_disk); 
                    }
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo currentdirectory = new DirectoryInfo(@"E:\\Folder_update");
            recursiveBuildDirectory(currentdirectory);
        }
    }
}
