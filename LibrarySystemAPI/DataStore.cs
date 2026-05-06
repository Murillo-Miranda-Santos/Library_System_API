using LibrarySystemAPI.Models;

namespace LibrarySystemAPI
{
    public static class DataStore
    {
        public static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Clean Code", IsLoaned = false },
            new Book { Id = 2, Title = "The Pragmatic Programmer", IsLoaned = false },
            new Book { Id = 3, Title = "Domain-Driven Design", IsLoaned = true }
        };

        public static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "Marcus" },
            new User { Id = 2, Name = "Dom" },
            new User { Id = 3, Name = "Baird" }
        };

        public static List<Loan> loans = new List<Loan>
        {
            new Loan { Id = 1, BookId = 3, UserId = 2, LoanDate = DateTime.Now.AddDays(-2) }
        };
    }
}
