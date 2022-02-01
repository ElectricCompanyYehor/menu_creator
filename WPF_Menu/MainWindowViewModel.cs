using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

        private string _comment;

        public string Comment
        {
            get => _comment;

            set
            {
                Set(() => Comment, ref _comment, value);
            }
        }

        public RelayCommand Add { get; set; }
        private string _addCount = "1";

        public string AddCount
        {
            get => _addCount;

            set
            {
                Set(() => AddCount, ref _addCount, value);
            }
        }
        public MainWindowViewModel()
        {
            Tabs = new List<Tab>();
            Add = new RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(_addCount))
                {
                    AddCount.Replace("-", "");

                    for (int i = 0; i < int.Parse(AddCount); i++)
                    {
                        SelectedTab.Dishes.Add(new Dish() { Name = "New meal/drink", Weight = 0, Price = 0, Currency = this.Currency, CountOfPieces = 1, Unit = Units?.FirstOrDefault() });
                    }

                }
                else
                {
                    AddCount = "1";
                }

            });
        }
    }

}
