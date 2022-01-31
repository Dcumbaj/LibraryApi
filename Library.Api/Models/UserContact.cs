namespace Library.Api.Models
{
    public class UserContact
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
