using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WPF_Menu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; }

        JsonSerializerSettings settingsWithAllReferenceHandling = new JsonSerializerSettings
        {
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            TypeNameHandling = TypeNameHandling.Auto
        };

        private async void SaveButton_click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "My Restaurant Menu (*.mrm) | *.mrm; ";
            if (openFileDialog.ShowDialog() == true)
            {
                var filename = openFileDialog.FileName;
                var tab = new TabSerializable()
                {
                    Currency = ViewModel.Currency,
                    Name = this.ViewModel.SelectedTab.Name,
                    Dishes = new List<DishSerializable>()
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
                var data = await File.ReadAllTextAsync(filename);
                var loadedTab = JsonConvert.DeserializeObject<TabSerializable>(data);


                ViewModel.SelectedTab.Name = loadedTab.Name;
                ViewModel.SelectedTab.Dishes.Clear();
                foreach (var dish in loadedTab.Dishes)
                {
                    ViewModel.SelectedTab.Dishes.Add(new Dish() { Name = dish.Name, Currency = dish.Currency, Price = dish.Price, Weight = dish.Weight, Unit = dish.Unit });
                }

                ViewModel.Currency = loadedTab.Currency;
            }
        }

        public MainWindow()
        {

            InitializeComponent();


            ViewModel = new MainWindowViewModel();
            DataContext = this;

            Tab first = new Tab();

            ViewModel.SelectedTab = first;

            ViewModel.Tabs.Add(first);

            first.Dishes.Add(new Dish() { Name = "Your meal/drink", Weight = 250, Price = 110, Currency = ViewModel.Currencies.First(), Unit = ViewModel?.Units?.FirstOrDefault() });
        }


        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var texttoChange = e.Text;
            if (!string.IsNullOrEmpty(e.Text))
            {
                foreach (char item in e.Text)
                {
                    if (!char.IsDigit(item))
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedTab.Dishes.Add(new Dish() { Name = "New meal/drink", Weight = 0, Price = 0, Currency = ViewModel.Currency });
        }
    }
}
