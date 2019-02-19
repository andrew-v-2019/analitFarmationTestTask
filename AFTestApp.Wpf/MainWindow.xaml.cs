using AFTestApp.Services.Interfaces;
using AFTestApp.Services.Services.Unity;
using AFTestApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace AFTestApp.Wpf
{
    public partial class MainWindow : Window
    {
        private IProductService _productService;
        private IDocumentService _documentService;

        public List<ProductViewModel> Products { get; set; }

        public ProductViewModel Product { get; set; }

        public MainWindow()
        {
            Products = new List<ProductViewModel>();
            InitializeComponent();
            ServiceInjector.ConfigureServices();
            _productService = ServiceInjector.Retrieve<IProductService>();
            _documentService = ServiceInjector.Retrieve<IDocumentService>();
            DataContext = this;
            //Products = _productService.GetProducts().Result;
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            productComboBox.IsEnabled = false;
            _productService.GetProducts().ContinueWith(task =>
            {
                Products = task.Result;
                productComboBox.IsEnabled = true;
            },
             TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
