namespace Library.Api.Models
{
    public class RentStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public enum RentStatusType
    { 
        None,
        Returned,
        NotReturned
    }
}
