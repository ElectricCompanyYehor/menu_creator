using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;

namespace WPF_Menu
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static List<string> _currencies = new List<string>() { "USD", "UAH", "EUR", "TRY" };

        public List<Tab> Tabs { get; set; }
        private Tab selectedTab;

        public Tab SelectedTab
        {
            get => selectedTab;

            set
            {
                selectedTab = value;
            }
        }

        private Dish _selectedDish;


        public Dish SelectedDish
        {
            get => _selectedDish;
            set => _selectedDish = value;
        }

        public static string _currency = _currencies.First();

        public string Currency
        {
            get => _currency;
            set
            {
                Set<string>(() => Currency, ref _currency, value);
                foreach (Dish dish in selectedTab.Dishes)
                {
                    dish.Currency = _currency;
                }
            }
        }

        public List<string> Currencies
        {
            get => _currencies;
            set
            {
                Set<List<string>>(() => Currencies, ref _currencies, value);
            }
        }

        public MainWindowViewModel()
        {
            Tabs = new List<Tab>();
        }
    }

}
