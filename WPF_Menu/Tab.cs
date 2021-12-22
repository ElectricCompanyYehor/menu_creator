using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace WPF_Menu
{
    public class Tab : ViewModelBase
    {
        public string Name { get; set; }

        // public bool editor;
        private ObservableCollection<Dish> _dishes = new ObservableCollection<Dish>();

        //  public bool[] Named { get; set; } = new bool[11];
        public ObservableCollection<Dish> Dishes { get => _dishes; }

        public Tab()
        {

        }
    }
}
