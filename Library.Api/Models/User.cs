namespace Library.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int? TotalOvedueInDays { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<RentHistory>? RentHistories { get; set; }
        public List<UserContact> Contacts { get; set; }
    }
}
