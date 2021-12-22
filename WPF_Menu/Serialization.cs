
using System;
using System.Collections.Generic;

namespace WPF_Menu
{
    public class DishSerializable
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string Unit { get; set; }
    }

    public class TabSerializable
    {
        public string Name { get; set; }
        public string Currency { get; set; }
        public List<DishSerializable> Dishes { get; set; }
    }
}