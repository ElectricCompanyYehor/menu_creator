using System;
using System.Collections.Generic;
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

        string what_section(int number)
        {
            return "section" + number;
        }

        bool used;

        private void section1_Click(object sender, RoutedEventArgs e)
        {

        }
        private void section2_Click(object sender, RoutedEventArgs e)
        {

        }
        private void section3_Click(object sender, RoutedEventArgs e)
        {

        }
        private void section4_Click(object sender, RoutedEventArgs e)
        {

        }
        private void section5_Click(object sender, RoutedEventArgs e)
        {

        }
        private void section6_Click(object sender, RoutedEventArgs e)
        {

        }
        private void section7_Click(object sender, RoutedEventArgs e)
        {

        }
        private void section8_Click(object sender, RoutedEventArgs e)
        {

        }
        private void section9_Click(object sender, RoutedEventArgs e)
        {

        }
        private void section10_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_click(object sender, RoutedEventArgs e)
        {

        }


        class Menu
        {
            public void SaveAs()
            {

            }
            public void Save()
            {

            }
            public void Show()
            {

            }

        }

        int this_section = 1;
        

        public MainWindow()
        {

            InitializeComponent();


            ViewModel = new MainWindowViewModel();
            DataContext = this;
            if (used == false)
            {
                //tabs.Named[1] = true;
                // tabs.editor = true;
                used = true;
            }

            Tab first = new Tab();

            ViewModel.SelectedTab = first;

            ViewModel.Tabs.Add(first);

            first.Dishes.Add(new Dish() { Name = "Your meal/drink", Weight=250, Price=110, Currency= ViewModel.Currencies.First()});
            first.Name = "Menu name";


        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedTab.Dishes.Add(new Dish() {   Name = "New meal/drink", Weight=0, CountOfPieces=1, Price=0, Currency = ViewModel.Currency});
        }
    }
}
