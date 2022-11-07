using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PcBuild.Logic;
using PcBuild.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PcBuild.VMS
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private MainWindowLogic logic;

        public ObservableCollection<Part> Store { get; set; }
        public ObservableCollection<Part> Basket { get; set; }

        private Part selectedFromStore;

        public Part SelectedFromStore
        {
            get { return selectedFromStore; }
            set 
            { 
                SetProperty(ref selectedFromStore, value);
                (AddToBasket as RelayCommand).NotifyCanExecuteChanged();
                (Sale as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Part selectedFromBasket;

        public Part SelectedFromBasket
        {
            get { return selectedFromBasket; }
            set 
            {
                SetProperty(ref selectedFromBasket, value);
                (RemoveFromBasket as RelayCommand).NotifyCanExecuteChanged();

            }
        }

        public int AllCost 
        {
            get { return logic.AllCost; }
        }


        public ICommand AddToBasket { get; set; }

        public ICommand RemoveFromBasket { get; set; }

        public ICommand Sale { get; set; }

        public ICommand Summary { get; set; }

        public ICommand LoadData { get; set; }

        public MainWindowViewModel()
        {
            logic = new MainWindowLogic(Messenger);
            Store = new ObservableCollection<Part>();
            Basket = new ObservableCollection<Part>();

            logic.bask = Basket;

            //Store.Add(new Part(PartType.CPU, 120, "i7"));

            LoadData = new RelayCommand(
                () => logic.LoadData(Store, (LoadData as RelayCommand)),
                () => Store.Count == 0
                );

            AddToBasket = new RelayCommand(
                () => logic.AddTo(SelectedFromStore, Basket),
                () => SelectedFromStore != null
                );

            RemoveFromBasket = new RelayCommand(
                () => logic.DeleteFrom(SelectedFromBasket, Basket),
                () => SelectedFromBasket != null
                );

            Sale = new RelayCommand(
                () => logic.Sale(SelectedFromStore),
                () => SelectedFromStore != null
                );

            Summary = new RelayCommand(
                () => logic.Summary(Basket)
                );


            Messenger.Register<MainWindowViewModel, string, string>(this, "PartInfo", (recipient, msg) =>
            {
                OnPropertyChanged("AllCost");
            }
            );
        }

    }
}
