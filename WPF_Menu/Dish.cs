using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Menu
{
    public class Dish : ViewModelBase
    {
        private string _unit;

        private string _name;
        public string Name { get => _name; set => Set<string>(() => Name, ref _name, value); }

        private double _weight;

        public double Weight { get => _weight; set => Set<double>(()=>Weight, ref _weight, value); }

        private double _price;

        public double Price { get => _price; set => Set<double>(() => Price, ref _price, value); }

        private string _currency = MainWindowViewModel._currency;

        public string Currency { get => _currency; set => Set<string>(() => Currency, ref _currency, value); }

       public string Unit { get => _unit; set => Set<string>(() => Unit, ref _unit, value); }

        private int _CountOfPieces;

        public int CountOfPieces { get => _CountOfPieces; set => Set<int>(() => CountOfPieces, ref _CountOfPieces, value); }

        private string _CountOfPiecesComment;

        public string CountOfPiecesComment { get => _CountOfPiecesComment; set => Set<string>(() => CountOfPiecesComment, ref _CountOfPiecesComment, value); }

    }
}
