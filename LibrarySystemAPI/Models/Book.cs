namespace LibrarySystemAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsLoaned { get; set; }
    }
}
