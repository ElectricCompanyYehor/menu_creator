using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace WPF_Menu
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static List<string> _currencies = new List<string>() { "USD", "UAH", "EUR", "TRY" };

        public List<Tab> Tabs { get; set; }
        private Tab _selectedTab;

        public Tab SelectedTab
        {
            get => _selectedTab;

            set
            {
                Set(() => SelectedTab, ref _selectedTab, value);
            }
        }

        private Dish _selectedDish;

        private static List<string> _units = new List<string>() { "g.", "kg.", "l.", "ml.", "pieces" };

        public List<string> Units { get => _units; }

        public Dish SelectedDish
        {
            get => _selectedDish;
            set => Set(() => SelectedDish, ref _selectedDish, value);
        }

        public static string _currency = _currencies.First();

        public string Currency
        {
            get => _currency;
            set
            {
                Set<string>(() => Currency, ref _currency, value);
                foreach (Dish dish in _selectedTab.Dishes)
                {
                    dish.Currency = _currency;
                }
            }
        }

        [JsonIgnore]
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
