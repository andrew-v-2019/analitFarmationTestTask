using AFTestApp.Services.Interfaces;
using AFTestApp.Services.Services.Unity;
using System.Windows;
using System.Windows.Controls;
using AFTestApp.Wpf.Models;
using System;

namespace AFTestApp.Wpf
{
    public partial class MainWindow : Window
    {
        private IProductService _productService;
        private IDocumentService _documentService;
        public DocumentViewModel ViewModel { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            InjectServices();
            var newDocument = _documentService.GetNewDocument();
            var productsDto = _productService.GetProducts();
            ViewModel = new DocumentViewModel(productsDto, newDocument);
            DataContext = ViewModel;
            SetAddProductButtonStatus();
        }


        private void SetAddProductButtonStatus()
        {
            var isNum = int.TryParse(ProductsCountTextBox.Text, out var num);
            if (ViewModel.NewProductId == default(int) || ViewModel.NewProductCount <= 0 || !isNum || num <= 0)
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

        private void ProductsCountTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            SetAddProductButtonStatus();
        }

        private void ProductComboBox_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            SetAddProductButtonStatus();
        }

        private void AddProductButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.AddSelectedProduct();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dto = ViewModel.ToDto();
                _documentService.SubmitDocument(dto);
                MessageBox.Show($"Документ {dto.DocumentNumber} сохранён", "Успешно", 0, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе сохранения произошли ошибки", "Ошибка", 0, MessageBoxImage.Error);
            }
        }
    }
}
