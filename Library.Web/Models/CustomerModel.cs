namespace Library.Web.Models
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        public CustomerModel()
        {
        }

        public CustomerModel(int customerID, string firstName, string lastName, string email)
        {
            CustomerID = customerID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
