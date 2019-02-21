using AFTestApp.Services.Interfaces;
using AFTestApp.Services.Services.Unity;
using System.Windows;
using System.Windows.Controls;
using AFTestApp.Wpf.Models;
using AFTestApp.ViewModels;
using NLog;

namespace AFTestApp.Wpf
{
    public partial class MainWindow : Window
    {
        private IProductService _productService;
        private IDocumentService _documentService;
        public DocumentViewModel ViewModel { get; set; }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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
            var dto = ViewModel.ToDto();
            var saveResult = _documentService.SubmitDocument(dto);
            var newDocument = _documentService.GetNewDocument();
            ViewModel.ResetDocument(newDocument);
            ShowResult(saveResult);
        }

        private static void ShowResult(SubmitResultDto result)
        {
            var message = $"Документ {result.Document.DocumentNumber} сохранён.\r\n Количество документов в базе данных {result.TotalDocumentsCount}";
            var messageBoxImage = MessageBoxImage.Information;
            var title = "Успешно";
            if (!result.Success)
            {
                message = "В ходе сохранения произошли ошибки";
                title = "Ошибка";
                messageBoxImage = MessageBoxImage.Error;
            }
            Logger.Info(message);
            MessageBox.Show(message, title, 0, messageBoxImage);
        }
    }
}
