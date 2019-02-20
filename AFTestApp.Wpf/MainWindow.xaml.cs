using AFTestApp.Services.Interfaces;
using AFTestApp.Services.Services.Unity;
using System.Windows;
using System.Windows.Controls;
using AFTestApp.Wpf.Models;

namespace AFTestApp.Wpf
{
    public partial class MainWindow : Window
    {
        private IProductService _productService;
        private IDocumentService _documentService;
        public EditDocumentModel ViewModel { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            InjectServices();

            var newDocument = _documentService.GetNewDocument();

            var productsDto = _productService.GetProducts();
            ViewModel = new EditDocumentModel(productsDto, newDocument);
            DataContext = ViewModel;
            SetAddProductButtonStatus();
        }


        private void SetAddProductButtonStatus()
        {
            var isNum = int.TryParse(ProductsCountTextBox.Text, out var num);
            if (ViewModel.SelectedProduct == null || ViewModel.SelectedProduct.Count <= 0 || !isNum || num <= 0)
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
            //ProductsCountTextBox.FormatTextBoxForOnlyDigits();
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
    }
}
