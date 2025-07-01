using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using SoftAlcoholApp.Commands;
using SoftAlcoholApp.Models;
using SoftAlcoholApp.Services;

namespace SoftAlcoholApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService dbService;
        private Drink? selectedDrink;
        private ObservableCollection<Drink> drinks;

        public ObservableCollection<Drink> Drinks
        {
            get => drinks;
            set { drinks = value; OnPropertyChanged(nameof(Drinks)); }
        }

        public Drink? SelectedDrink
        {
            get => selectedDrink;
            set { selectedDrink = value; OnPropertyChanged(nameof(SelectedDrink)); }
        }

        // Команди
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand LoadCommand { get; }

        public MainViewModel()
        {
            string connStr = "server=localhost;user=root;password=KiCo72L9gi7g;database=SoftAlcoholProduction;";
            dbService = new DatabaseService(connStr);

            Drinks = new ObservableCollection<Drink>(dbService.GetAllDrinks());

            AddCommand = new RelayCommand(_ => AddDrink());
            UpdateCommand = new RelayCommand(_ => UpdateDrink(), _ => SelectedDrink != null);
            DeleteCommand = new RelayCommand(_ => DeleteDrink(), _ => SelectedDrink != null);
            LoadCommand = new RelayCommand(_ => LoadDrinks());
        }

        private void LoadDrinks()
        {
            Drinks = new ObservableCollection<Drink>(dbService.GetAllDrinks());
        }

        private void AddDrink()
        {
            if (SelectedDrink == null)
            {
                MessageBox.Show("Оберіть або введіть новий напій.");
                return;
            }

            dbService.AddDrink(SelectedDrink);
            LoadDrinks();
        }

        private void UpdateDrink()
        {
            if (SelectedDrink == null)
            {
                MessageBox.Show("Оберіть напій для редагування.");
                return;
            }

            dbService.UpdateDrink(SelectedDrink);
            LoadDrinks();
        }

        private void DeleteDrink()
        {
            if (SelectedDrink == null)
            {
                MessageBox.Show("Оберіть напій для видалення.");
                return;
            }

            dbService.DeleteDrink(SelectedDrink.Id);
            LoadDrinks();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
