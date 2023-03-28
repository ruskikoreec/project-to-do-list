using Project.Models;
using Project.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Data;

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

        //загрузка окна
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
                this.myDateTime.Text = DateTime.Now.ToString("Время: HH:mm:ss Дата: dd.MM.yyyy");
            }, this.Dispatcher);
            timer.Start();

        }

        //изменения в списке
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

        //cтрока поиска
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text;
            List<ToDoModel> filteredItems = ToDoModelList.Where(item => item.Text.Contains(searchText)).ToList();
            dgToDoList.ItemsSource = filteredItems;
        }

        //изменение шрифта
        private void ShriftChanged(object sender, RoutedEventArgs e)
        {
            try 
            { 
            dgToDoList.FontFamily = fontComboBox.SelectedItem as FontFamily;
            }
            catch (Exception)
            {
                MessageBox.Show($"Ошибка: Выберите, пожалуйста, шрифт!");
            }
        }
        //изменение размера текста
        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)FontSizeComboBox.SelectedItem;
            string fontSize = selectedItem.Content.ToString();

            switch (fontSize)
            {
                case "Уменьшенный":
                    {
                        ChangeFontSize(12, 90, 54, 99);
                    }
                    break;
                case "По умолчанию":
                    {
                        ChangeFontSize(16, 119, 67, 131);
                    }
                    break;
                case "Увеличенный":
                    {
                        ChangeFontSize(22, 155, 92, 179);
                    }
                    break;
            }
        }

        private void ChangeFontSize(int fontSize, int razmerWidth, int shriftWidth, int sortWidth)
        {
            dgToDoList.FontSize = fontSize;
            razmer.FontSize = fontSize;
            FontSizeComboBox.FontSize = fontSize;
            Shrift.FontSize = fontSize;
            fontComboBox.FontSize = fontSize;
            Sorting.FontSize = fontSize;
            sort.FontSize = fontSize;
            razmer.Width = razmerWidth;
            Shrift.Width = shriftWidth;
            sort.Width = sortWidth;
        }

        //Сортировка столбцов
        private void SortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)Sorting.SelectedItem;
            string sorting = selectedItem.Content.ToString();

            switch (sorting)
            {
                case "Выполненным":
                    ChangeToDoListDIrection("IsDone", true);
                    break;
                case "Невыполненным":
                    ChangeToDoListDIrection("IsDone", false);
                    break;
                case "Новым задачам":
                    ChangeToDoListDIrection("Deadline", true);
                    break;
                case "Старым задачам":
                    ChangeToDoListDIrection("Deadline", false);
                    break;
                case "Неважным":
                    ChangeToDoListDIrection("Importance", true);
                    break;
                case "Важным":
                    ChangeToDoListDIrection("Importance", false);
                    break;

            }

        }

        private void ChangeToDoListDIrection(string property, bool sortingDescinding)
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(ToDoModel))[property];
            if (ToDoModelList != null)
            {
                if (sortingDescinding)
                {
                    ToDoModelList = new BindingList<ToDoModel>(ToDoModelList.OrderByDescending(x => prop.GetValue(x)).ToList());
                }
                else
                {
                    ToDoModelList = new BindingList<ToDoModel>(ToDoModelList.OrderBy(x => prop.GetValue(x)).ToList());
                }

                dgToDoList.ItemsSource = ToDoModelList;
            }
        }
    }
}
