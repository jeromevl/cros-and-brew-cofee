namespace Cros.DataAccess.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public required string CustomerNo { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public string? NameSuffix { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobilePhoneNo { get; set; }
        public string? Signature { get; set; }
    }
}