using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AFTestApp.ViewModels;

namespace AFTestApp.Wpf.Models
{
    public class EditDocumentModel : INotifyPropertyChanged
    {
        private Product selectedProduct;

        public ObservableCollection<Product> Products { get; set; }

        public ObservableCollection<Product> SelectedProducts { get; set; }

        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        private Product viewProduct { get; set; }

        public Product ViewProduct
        {
            get => viewProduct;
            set
            {
                viewProduct = value;
                OnPropertyChanged("ViewProduct");
            }
        }

        public EditDocumentModel(List<ProductViewModel> source, DocumentViewModel newDocument)
        {
            SelectedProducts = new ObservableCollection<Product>();
            Products = new ObservableCollection<Product>();
            Refresh(source);
        }

        public void Refresh(List<ProductViewModel> source)
        {
            foreach (var productDtoModel in source)
            {
                Products.Add(new Product
                {
                    Count = 1,
                    Code = productDtoModel.Code,
                    Name = productDtoModel.Name,
                    ProductId = productDtoModel.ProductId,
                    Price = productDtoModel.Price,
                    Order = productDtoModel.Order
                });
            }
        }

        public void AddSelectedProduct()
        {
            var existingItem = SelectedProducts.FirstOrDefault(x => x.ProductId == SelectedProduct.ProductId);
            if (existingItem != null)
            {
                existingItem.Count = existingItem.Count + SelectedProduct.Count;
                existingItem.Sum = existingItem.Price * existingItem.Count;
            }
            else
            {
                SelectedProduct.Sum = SelectedProduct.Price * SelectedProduct.Count;
                SelectedProducts.Add(new Product()
                {
                    Count = SelectedProduct.Count,
                    ProductId = SelectedProduct.ProductId,
                    Price = SelectedProduct.Price,
                    Name = SelectedProduct.Name,
                    Code = SelectedProduct.Code,
                    Sum = SelectedProduct.Sum,
                    Order = SelectedProduct.Order
                });
            }
            SelectedProduct.Count = 1;

            SelectedProduct = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
