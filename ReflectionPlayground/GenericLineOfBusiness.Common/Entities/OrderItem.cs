namespace GenericLineOfBusiness.Common.Entities
{
    public class OrderItem : BaseNotifyPropertyChanged
    {
        private Product productItem;
        private int quantity;

        public Product ProductItem
        {
            get { return productItem; }
            set
            {
                if (productItem == value)
                    return;
                productItem = value;
                OnPropertyChanged();
                OnPropertyChanged("ItemTotal");
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity == value)
                    return;
                quantity = value;
                OnPropertyChanged();
                OnPropertyChanged("ItemTotal");
            }
        }
        public decimal ItemTotal
        {
            get
            {
                return ProductItem.Price * Quantity;
            }
        }

        public override string ToString()
        {
            return string.Format("{0,20} x{1,2} = {2,4:C}", ProductItem, Quantity, ItemTotal);
        }
    }
}
