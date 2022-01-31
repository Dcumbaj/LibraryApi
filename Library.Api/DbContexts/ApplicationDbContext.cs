using Library.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentHistory>()
                .HasOne(x => x.Book)
                .WithMany(y => y.RentHistories)
                .HasForeignKey(z => z.BookId);


            modelBuilder.Entity<RentHistory>()
                .HasOne(x => x.User)
                .WithMany(y => y.RentHistories)
                .HasForeignKey(z => z.UserId);
        }

        public DbSet<Bibliotheca> Bibliothecas { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }
        public DbSet<RentHistory> RentHistories  { get; set; }
        public DbSet<RentStatus> RentStatuses { get; set; }

    }
}
