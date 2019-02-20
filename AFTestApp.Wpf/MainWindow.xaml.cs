using System;
using AFTestApp.Services.Interfaces;
using AFTestApp.Services.Services.Unity;
using AFTestApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AFTestApp.Wpf
{
    public partial class MainWindow : Window
    {
        private IProductService _productService;
        private IDocumentService _documentService;
        public List<ProductViewModel> Products { get; set; }
        public ProductViewModel CurrentProduct { get; set; }
        public DocumentViewModel CurrentDocument { get; set; }


        public MainWindow()
        {
            Products = new List<ProductViewModel>();
            InitializeComponent();
            InjectServices();
            DataContext = this;
            RefreshProducts();
            CreateNewDocument();
            SetAddProductButtonStatus();
        }


        private void CreateNewDocument()
        {
            CurrentDocument= _documentService.GetNewDocument();
        }

        private void RefreshProducts()
        {
            Products = _productService.GetProducts();
        }

        private void SetAddProductButtonStatus()
        {
            if (CurrentProduct == null || ProductsCountTextBox.Text.Trim() == string.Empty)
            {
                AddProductButton.IsEnabled = false;
            }
            else
            {
                AddProductButton.IsEnabled = true;
            }
        }

        private void InjectServices()
        {
            ServiceInjector.ConfigureServices();
            _productService = ServiceInjector.Retrieve<IProductService>();
            _documentService = ServiceInjector.Retrieve<IDocumentService>();
        }

        private void ProductsCountTextBox_FormatText(object sender, RoutedEventArgs e)
        {
            ProductsCountTextBox.FormatTextBoxForOnlyDigits();
            SetAddProductButtonStatus();
        }

        private void ProductComboBox_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentProduct.Count = 1;
            SetAddProductButtonStatus();
        }
    }
}
