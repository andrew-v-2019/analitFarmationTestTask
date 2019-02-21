using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AFTestApp.DtoModels;
using AFTestApp.Extensions;

namespace AFTestApp.Wpf.Models
{
    public class DocumentViewModel : INotifyPropertyChanged
    {
        private decimal documentSum;
        public decimal DocumentSum
        {
            get => documentSum;
            set
            {
                documentSum = value;
                OnPropertyChanged("DocumentSum");
            }
        }

        private int documentId;

        private int itemsCount;
        public int ItemsCount
        {
            get => itemsCount;
            set
            {
                itemsCount = value;
                OnPropertyChanged("ItemsCount");
            }
        }


        private string documentType;
        public string DocumentType
        {
            get => documentType;
            set
            {
                documentType = value;
                OnPropertyChanged("DocumentType");
            }
        }


        private string documentNumber;
        public string DocumentNumber
        {
            get => documentNumber;
            set
            {
                documentNumber = value;
                OnPropertyChanged("DocumentNumber");
            }
        }


        private int newProductId;
        public int NewProductId
        {
            get => newProductId;
            set
            {
                newProductId = value;
                OnPropertyChanged("NewProductId");
            }
        }

        private int newProductCount;
        public int NewProductCount
        {
            get => newProductCount;
            set
            {
                newProductCount = value;
                OnPropertyChanged("NewProductCount");
            }
        }

        public int DocumentTypeId { get; set; }
        public int DocumentStatusId { get; set; }

        public ObservableCollection<ProductViewModel> Products { get; set; }

        public ObservableCollection<ProductViewModel> SelectedProducts { get; set; }

        private ProductViewModel viewProduct { get; set; }

        public ProductViewModel ViewProduct
        {
            get => viewProduct;
            set
            {
                viewProduct = value;
                OnPropertyChanged("ViewProduct");
            }
        }

        public DocumentViewModel(List<ProductDto> source, DocumentDto newDocument)
        {
            SelectedProducts = new ObservableCollection<ProductViewModel>();
            ResetProductsSource(source);
            ResetDocument(newDocument);
        }

        public void ResetDocument(DocumentDto newDocument)
        {
            SelectedProducts.Clear();
            documentId = newDocument.DocumentId;
            DocumentStatusId = newDocument.DocumentStatusId;
            DocumentTypeId = newDocument.DocumentTypeId;
            DocumentNumber = newDocument.DocumentNumber;
            CalculateDocumentSum();
            CalculateItemsCount();
            DocumentType = GetDocumentTypeString(newDocument);
        }

        private static string GetDocumentTypeString(DocumentDto newDocument)
        {
            return $"{newDocument.DocumentType.GetDescription()} ({newDocument.DocumentStatus.GetDescription()})";
        }

        private void CalculateDocumentSum()
        {
            DocumentSum = 0;
            foreach (var p in SelectedProducts)
            {
                DocumentSum = DocumentSum + p.Sum;
            }
        }

        private void CalculateItemsCount()
        {
            ItemsCount = SelectedProducts.Count();
        }

        public void ResetProductsSource(List<ProductDto> source)
        {
            Products = new ObservableCollection<ProductViewModel>();
            foreach (var productDtoModel in source)
            {
                var viewModel = new ProductViewModel(productDtoModel)
                {
                    Count = 1
                };
                Products.Add(viewModel);
            }
        }

        public void AddSelectedProduct()
        {
            var existingItem = SelectedProducts.FirstOrDefault(x => x.ProductId == newProductId);
            if (existingItem != null)
            {
                existingItem.Count = existingItem.Count + newProductCount;
                CalculateProductSum(existingItem);
            }
            else
            {
                var productToAdd = Products.Where(x => x.ProductId == newProductId)
                    .Select(x => new ProductViewModel(x)).FirstOrDefault();
                if (productToAdd == null)
                {
                    return;
                }

                productToAdd.Count = newProductCount;
                CalculateProductSum(productToAdd);
                SelectedProducts.Add(productToAdd);
            }
            NewProductCount = 1;
            NewProductId = default(int);
            CalculateDocumentSum();
            CalculateItemsCount();
        }

        private static void CalculateProductSum(ProductViewModel product)
        {
            product.Sum = product.Price * product.Count;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public DocumentDto ToDto()
        {
            var documentDto = new DocumentDto
            {
                DocumentId = documentId,
                DocumentNumber = DocumentNumber,
                DocumentTypeId = DocumentTypeId,
                Products = new List<ProductDto>(),
            };

            var order = 1;

            foreach (var p in Products)
            {
                var dto = new ProductDto
                {
                    Count = p.Count,
                    ProductId = p.ProductId,
                    Order = order
                };
                documentDto.Products.Add(dto);
                order++;
            }

            return documentDto;
        }
    }
}
