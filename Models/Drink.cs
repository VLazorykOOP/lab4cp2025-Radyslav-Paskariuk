using System;
using System.ComponentModel;

namespace SoftAlcoholApp.Models
{
    public class Drink : INotifyPropertyChanged
    {
        private int id;
        private string type;
        private string brand;
        private string manufacturer;
        private string supplier;
        private DateTime expirationDate;
        private decimal price;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Type
        {
            get => type;
            set { type = value; OnPropertyChanged(nameof(Type)); }
        }

        public string Brand
        {
            get => brand;
            set { brand = value; OnPropertyChanged(nameof(Brand)); }
        }

        public string Manufacturer
        {
            get => manufacturer;
            set { manufacturer = value; OnPropertyChanged(nameof(Manufacturer)); }
        }

        public string Supplier
        {
            get => supplier;
            set { supplier = value; OnPropertyChanged(nameof(Supplier)); }
        }

        public DateTime ExpirationDate
        {
            get => expirationDate;
            set { expirationDate = value; OnPropertyChanged(nameof(ExpirationDate)); }
        }

        public decimal Price
        {
            get => price;
            set { price = value; OnPropertyChanged(nameof(Price)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
