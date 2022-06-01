using Microsoft.EntityFrameworkCore;

namespace MinimalApiExample
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Publisher { get; set; }

        public int PublishYear { get; set; }
    }

    public class dbBook : DbContext
        {


        public dbBook(DbContextOptions<dbBook> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Book> Books { get; set; }

        

}
}
