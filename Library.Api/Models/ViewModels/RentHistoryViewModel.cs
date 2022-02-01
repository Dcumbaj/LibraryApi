namespace Library.Api.Models.ViewModels
{
    public class RentHistoryViewModel
    {
        public DateTime RentDate { get; set; }
        public DateTime DueDate { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int RentStatusId { get; set; }
    }
}
