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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using static Project.Sort;

namespace Project
{

    public partial class MainWindow : Window
    {

        private readonly string FilePath = $"{Environment.CurrentDirectory}\\todoDataList.json";
        private BindingList<ToDoModel> ToDoModelList;
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
                ToDoModelList = fileIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            dgToDoList.ItemsSource = ToDoModelList;
            ToDoModelList.ListChanged += ToDoModelList_ListChanged;

            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, (object s, EventArgs ev) =>
            {
                this.myDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            }, this.Dispatcher);
            timer.Start();

        }

        private void ToDoModelList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
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
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text;
            List<ToDoModel> filteredItems = ToDoModelList.Where(item => item.Text.Contains(searchText)).ToList();
            dgToDoList.ItemsSource = filteredItems;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try 
            { // Применяем выбранный шрифт ко всем столбцам
            dgToDoList.FontFamily = fontComboBox.SelectedItem as FontFamily;
            }
            catch (Exception)
            {
                MessageBox.Show($"Ошибка: Выберите, пожалуйста, шрифт!");
            }
        }
        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)FontSizeComboBox.SelectedItem;
            string fontSize = selectedItem.Content.ToString();

            switch (fontSize)
            {
                case "Уменьшенный":
                    {
                        dgToDoList.FontSize = 12;
                        razmer.FontSize = 12;
                        FontSizeComboBox.FontSize = 12;
                        Shrift.FontSize = 12;
                        fontComboBox.FontSize = 12;
                        razmer.Width = 90;
                        Shrift.Width = 54;
                    }
                    break;
                case "По умолчанию":
                    {
                        dgToDoList.FontSize = 16;
                        razmer.FontSize = 16;
                        FontSizeComboBox.FontSize = 16;
                        Shrift.FontSize = 16;
                        fontComboBox.FontSize = 16;
                        razmer.Width = 119;
                        Shrift.Width = 67;
                    }
                    break;
                case "Увеличенный":
                    {
                        dgToDoList.FontSize = 22;
                        razmer.FontSize = 22;
                        FontSizeComboBox.FontSize = 22;
                        Shrift.FontSize = 22;
                        fontComboBox.FontSize = 22;
                        razmer.Width = 155;
                        Shrift.Width = 92;

                    }
                    break;

            }
        }
        private void sortButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем свойство "Text" для сортировки
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(ToDoModel))["IsDone"];

            // Сортируем элементы по свойству "Text"
            ToDoModelList = new BindingList<ToDoModel>(ToDoModelList.OrderBy(x => prop.GetValue(x)).ToList());

            // Обновляем источник данных таблицы
            dgToDoList.ItemsSource = ToDoModelList;
        }

        private void sortButton_Click2(object sender, RoutedEventArgs e)
        {
            // Получаем свойство "Text" для сортировки
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(ToDoModel))["Deadline"];

            // Сортируем элементы по свойству "Text"
            ToDoModelList = new BindingList<ToDoModel>(ToDoModelList.OrderBy(x => prop.GetValue(x)).ToList());

            // Обновляем источник данных таблицы
            dgToDoList.ItemsSource = ToDoModelList;
        }

    }

}
