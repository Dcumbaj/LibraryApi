namespace Library.Api.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int NumberOfCopies { get; set; }
        private int? NumberOfRents { get; set; }
        public DateTime PublishDate { get; set; }
        public decimal BookPrice { get; set; }
        public int BibliothecaId { get; set; }
        public int BookCategoryId { get; set; }
        public Bibliotheca Bibliotheca { get; set; }
        public BookCategory BookCategory { get; set; }
        public List<RentHistory>? RentHistories { get; set; }
    }
}
