﻿using AFTestApp.DtoModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AFTestApp.Wpf.Models
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private int productId { get; set; }
        private string code { get; set; }
        private string name { get; set; }
        private decimal price { get; set; }
        private int order { get; set; }
        private int count { get; set; }
        private decimal sum { get; set; }


        public ProductViewModel(ProductDto productDtoModel)
        {
            Count = productDtoModel.Count;
            Code = productDtoModel.Code;
            Name = productDtoModel.Name;
            ProductId = productDtoModel.ProductId;
            Price = productDtoModel.Price;
            Order = productDtoModel.Order;
        }

        public ProductViewModel(ProductViewModel source)
        {
            Count = source.Count;
            Code = source.Code;
            Name = source.Name;
            ProductId = source.ProductId;
            Price = source.Price;
            Order = source.Order;
        }

        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }

        public decimal Sum
        {
            get => sum;
            set
            {
                sum = value;
                OnPropertyChanged("Sum");
            }
        }


        public int Order
        {
            get => order;
            set
            {
                order = value;
                OnPropertyChanged("Order");
            }
        }


        public int ProductId
        {
            get => productId;
            set
            {
                productId = value;
                OnPropertyChanged("ProductId");
            }
        }

        public string Code
        {
            get => code;
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }

        public decimal Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
