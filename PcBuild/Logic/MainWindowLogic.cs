using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PcBuild.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuild.Logic
{
    public class MainWindowLogic
    {
        private IMessenger messenger;
        public IList<Part> bask;

        public MainWindowLogic(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public void LoadData(IList<Part> store, RelayCommand command)
        {
            var lines = File.ReadAllLines("parts.txt");

            foreach (var line in lines)
            {
                var splitted = line.Split(' ');
                store.Add(new Part(WhatType(splitted[0]), int.Parse(splitted[1]), splitted[2]));

                command.NotifyCanExecuteChanged();
            }
        }

        private PartType WhatType(string p)
        {
            if (p == "CPU")
                return PartType.CPU;
            if (p == "SSD")
                return PartType.SSD;
            if (p == "RAM")
                return PartType.RAM;
            else
                return PartType.MOTHERBOARD;
            
                
        }

        public int AllCost 
        { 
            get 
            {
                if (bask.Count == 0)
                    return 0;
                else
                    return bask.Sum(p => p.Price);
            } 
        }


        public void Summary(IList<Part> basket)
        {
            StreamWriter writer = new StreamWriter("recpt.txt");
            foreach (var item in basket)
            {
                writer.WriteLine(item.ToString());
            }
            writer.Close();
            new Summary(basket).Show();
        }

        public void Sale(Part p)
        {
            p.Price = (int)(p.Price * 0.9);
        }

        public void DeleteFrom(Part p, IList<Part> basket)
        {
            basket.Remove(p);
            messenger.Send("PartDeleted", "PartInfo");
        }

        public void AddTo(Part p, IList<Part> basket)
        {
            if(p.Type == PartType.CPU)
            {
                bool dup = false;
                foreach (var item in basket)
                {
                    if (item.Type == PartType.CPU)
                    {
                        dup = true;
                    }
                                       
                }

                if (dup == false)
                {
                    basket.Add(p);
                    messenger.Send("PartAdded", "PartInfo");
                }
            }
            else if(p.Type == PartType.MOTHERBOARD)
            {
                bool dup = false;
                foreach (var item in basket)
                {
                    if (item.Type == PartType.MOTHERBOARD)
                    {
                        dup = true;
                    }                 
                        
                }

                if (dup == false)
                {
                    basket.Add(p);
                    messenger.Send("PartAdded", "PartInfo");
                }
            }
            else
            {
                basket.Add(p);
                messenger.Send("PartAdded", "PartInfo");
            }


        }

    }
}
