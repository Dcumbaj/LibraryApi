namespace Library.Api.Models.ViewModels
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int TotalOvedueInDays { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<UserContact> Contacts { get; set; }
    }
}
