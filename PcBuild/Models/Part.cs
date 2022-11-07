using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuild.Models
{
    
    public enum PartType { CPU, SSD, MOTHERBOARD, RAM}

    public class Part : ObservableObject
    {
        public PartType Type { get; set; }

        public int Price { get; set; }

        public string Name { get; set; }

        public Part(PartType type, int price, string name)
        {
            Type = type;
            Price = price;
            Name = name;
        }

        public override string ToString()
        {
            return Type + " " + Name + " " + Price;
        }
    }
}
