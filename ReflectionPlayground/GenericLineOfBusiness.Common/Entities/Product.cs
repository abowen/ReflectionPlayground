
namespace GenericLineOfBusiness.Common.Entities
{
    public class Product : BaseNotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return string.Format("{0,-20} {1,10:C}", Name, Price);
        }
    }
}
