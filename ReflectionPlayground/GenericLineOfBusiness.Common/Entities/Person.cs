namespace GenericLineOfBusiness.Common.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("{0,3} {1,10} {2,10}", Id, FirstName, LastName);
        }
    }
}
