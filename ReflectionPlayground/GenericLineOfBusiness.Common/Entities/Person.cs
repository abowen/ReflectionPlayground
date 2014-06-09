namespace GenericLineOfBusiness.Common.Entities
{
    public class Person : BaseNotifyPropertyChanged
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("{0,-10} {1,-10}", FirstName, LastName);
        }
    }
}
