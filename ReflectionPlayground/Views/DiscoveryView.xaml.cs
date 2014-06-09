using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using GenericLineOfBusiness.Common.Entities;
using GenericLineOfBusiness.Common.Interfaces;
using ReflectionPlayground.Annotations;

namespace ReflectionPlayground.Views
{
    /// <summary>
    /// Interaction logic for DiscoveryView.xaml
    /// </summary>
    public partial class DiscoveryView : UserControl, INotifyPropertyChanged
    {
        public DiscoveryView()
        {            
            InitializeComponent();
            
            var personRepository = GetPersonRepository();
            People = personRepository.GetAll().ToArray();

            var productRepository = GetProductRepository();
            Products = productRepository.GetAll().ToArray();

            Order = new Order();
        }


        private IEnumerable<Person> _people;
        public IEnumerable<Person> People
        {
            get { return _people; }
            set { _people = value; OnPropertyChanged(); }
        }

        private IEnumerable<Product> _products;
        public IEnumerable<Product> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(); }
        }
        

        private Order _order;
        public Order Order
        {
            get { return _order; }
            set { _order = value; OnPropertyChanged(); }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(); }
        }
        

        private void AddProduct_OnClick(object sender, RoutedEventArgs e)
        {
            var orderItem = Order.OrderItems.FirstOrDefault(oi => oi.ProductItem == SelectedProduct);            
            if (orderItem == null)
            {
                orderItem = new OrderItem { ProductItem = SelectedProduct, Quantity = 1};                
            }
            else
            {
                orderItem.Quantity++;
                Order.OrderItems.Remove(orderItem);                
            }
            Order.OrderItems.Add(orderItem);    
        }


        public static IPersonRepository GetPersonRepository()
        {
            var repoTypeName = "GenericLineOfBusiness.RepositoryCsv.PersonRepository, GenericLineOfBusiness.RepositoryCsv, Version=1.0.0.0, Culture=neutral";            
            var repoType = Type.GetType(repoTypeName);
            var repoInstance = Activator.CreateInstance(repoType);
            var repo = repoInstance as IPersonRepository;
            return repo;
        }

        public static IProductRepository GetProductRepository()
        {
            var repoTypeName = "GenericLineOfBusiness.RepositoryCsv.ProductRepository, GenericLineOfBusiness.RepositoryCsv, Version=1.0.0.0, Culture=neutral";
            var repoType = Type.GetType(repoTypeName);
            var repoInstance = Activator.CreateInstance(repoType);
            var repo = repoInstance as IProductRepository;
            return repo;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
