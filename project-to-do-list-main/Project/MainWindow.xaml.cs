using Project.Models;
using Project.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using System.Windows.Threading;
using System.Data;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //void SetProperties()
        //{
        //    this.Icon=
        //}
        private readonly string FilePath = $"{Environment.CurrentDirectory}\\todoDataList.json";
        private BindingList<ToDoModel> _todoDataList;
        private FileIOService fileIOService;
        public MainWindow()
        {
            InitializeComponent();
        }
 
            private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fileIOService = new FileIOService(FilePath);
            try
            {
                _todoDataList = fileIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
            
            dgToDoList.ItemsSource = _todoDataList;
            _todoDataList.ListChanged += _todoDataList_ListChanged;

            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, (object s, EventArgs ev) =>
            {
                this.myDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            }, this.Dispatcher);
            timer.Start();
            
        }

        private void _todoDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType== ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    fileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }
        private void dataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            e.Handled = true;

            DataGridColumn column = e.Column;
            ListSortDirection direction = (column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;

            column.SortDirection = direction;

            ICollectionView view = CollectionViewSource.GetDefaultView(dgToDoList.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription(column.SortMemberPath, direction));
            view.Refresh();
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text;
            List<ToDoModel> filteredItems = _todoDataList.Where(item => item.Text.Contains(searchText)).ToList();
            dgToDoList.ItemsSource = filteredItems;
        }
        //private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    ICollectionView cv = CollectionViewSource.GetDefaultView(dgToDoList.ItemsSource);
        //    if (cv == null)
        //        return;
        //    if (!string.IsNullOrEmpty(SearchBox.Text))
        //    {
        //        cv.Filter = o =>
        //        {
        //            if (o is ToDoModel item)
        //            {

        //                return item.Text.IndexOf(SearchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        //            }
        //            return false;
        //        };
        //    }
        //    else
        //    {
        //        cv.Filter = null;
        //    }
        //}

    }

}
