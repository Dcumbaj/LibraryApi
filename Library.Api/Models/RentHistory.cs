namespace Library.Api.Models
{
    public class RentHistory
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime DueDate { get; set; }

        public int RentStatusId { get; set; }
        public RentStatus RentStatus { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
