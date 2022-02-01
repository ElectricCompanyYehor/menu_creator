using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace WPF_Menu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; }

        private async void SaveButton_click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "My Restaurant Menu (*.mrm) | *.mrm; ";
            openFileDialog.FileName = ViewModel.SelectedTab.Name;
            if (openFileDialog.ShowDialog() == true)
            {
                var filename = openFileDialog.FileName;
                var tab = new TabSerializable()
                {
                    Currency = ViewModel.Currency,
                    Name = this.ViewModel.SelectedTab.Name,
                    Dishes = new List<DishSerializable>(),
                    Comment = ViewModel.Comment
                };

                foreach (var dish in ViewModel.SelectedTab.Dishes)
                {
                    tab.Dishes.Add(new DishSerializable() { Name = dish.Name, Currency = dish.Currency, Price = dish.Price, Weight = dish.Weight, Unit = dish.Unit });
                }

                string output = JsonConvert.SerializeObject(tab);
                await File.WriteAllTextAsync(filename, output);
            }

        }

        private async void OpenButton_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var filename = openFileDialog.FileName;
                await LoadFromFile(filename);
            }
        }

        private async System.Threading.Tasks.Task LoadFromFile(string filename)
        {
            var data = await File.ReadAllTextAsync(filename);
            var loadedTab = JsonConvert.DeserializeObject<TabSerializable>(data);


            ViewModel.SelectedTab.Name = loadedTab.Name;
            ViewModel.SelectedTab.Dishes.Clear();
            foreach (var dish in loadedTab.Dishes)
            {
                ViewModel.SelectedTab.Dishes.Add(new Dish() { Name = dish.Name, Currency = dish.Currency, Price = dish.Price, Weight = dish.Weight, Unit = dish.Unit });
            }

            ViewModel.Currency = loadedTab.Currency;
                ViewModel.Comment = loadedTab.Comment;
        }

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;

            ViewModel = new MainWindowViewModel();
            DataContext = this;

            Tab first = new Tab();

            ViewModel.SelectedTab = first;

            ViewModel.Tabs.Add(first);

            first.Dishes.Add(new Dish() { Name = "Your first meal/drink", Weight = 0, CountOfPieces = 1, Pennies = 0, Price = 0, Currency = ViewModel.Currencies.First(), Unit = ViewModel?.Units?.FirstOrDefault() });
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private static readonly Regex _penniesRegex = new Regex("[^0-9.-]{1,2}");
        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                var fileName = args[1];
                if (File.Exists(fileName))
                {
                    var extension = Path.GetExtension(fileName);
                    if (extension == ".mrm")
                    {
                        LoadFromFile(fileName);
                    }
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled =! IsTextAllowed(e.Text);
        }
    }
}
