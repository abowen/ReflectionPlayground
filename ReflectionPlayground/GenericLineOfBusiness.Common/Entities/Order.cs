using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace GenericLineOfBusiness.Common.Entities
{
    public class Order : BaseNotifyPropertyChanged
    {
        #region Fields

        private Person customer;
        private ObservableCollection<OrderItem> orderItems;
        private int orderDiscount;

        #endregion

        public Person Customer
        {
            get { return customer; }
            set
            {
                if (customer == value)
                    return;
                customer = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<OrderItem> OrderItems
        {
            get { return orderItems; }
            set
            {
                if (orderItems == value)
                    return;
                orderItems = value;
                OnPropertyChanged();
            }
        }

        public int OrderDiscount
        {
            get { return orderDiscount; }
            set
            {
                if (orderDiscount == value)
                    return;
                orderDiscount = value;
                OnPropertyChanged();
                OnPropertyChanged("TotalPrice");
            }
        }

        public decimal TotalPrice
        {
            get
            {
                var total = orderItems.Sum(p => p.ItemTotal);
                if (OrderDiscount >= 0)
                    return total - (total * OrderDiscount / 100);
                return total;
            }
        }

        public Order()
        {
            OrderItems = new ObservableCollection<OrderItem>();
            OrderItems.CollectionChanged += OrderItems_CollectionChanged;
        }

        void OrderItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("TotalPrice");
        }
    }
}
