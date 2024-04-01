using Aspose.Html.Accessibility;
using Aspose.Html.Toolkit.Markdown.Syntax;
using Prob_now.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Prob_now
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ModulesProvider modulesProvider = new ModulesProvider();

        public MainWindow()
        {
            InitializeComponent();
        }

        public Module[] list_files = Array.Empty<Module>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List_view.Items.Clear();
            DirectoryInfo currentdirectory = new DirectoryInfo(@"E:\Folder_update");
            list_files = modulesProvider.ListModules(currentdirectory);
            foreach (var item in list_files)
            {
                List_view.Items.Add(item);
            }
        }

        private void List_view_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            List_view_info.Items.Clear();
            var point = List_view.SelectedItem as Module;
            DirectoryInfo currentdirectory = new DirectoryInfo(@"E:\Folder_update");

            ListView view = sender as ListView;

            var index = view.SelectedIndex;
            if (index == -1)
                return;

            var items = modulesProvider.ListModuleInfo(new Guid(list_files[index].Id_ver), currentdirectory);
            foreach (var item in items)
            {
                List_view.Items.Add(item);
            }
        }
    }
}
