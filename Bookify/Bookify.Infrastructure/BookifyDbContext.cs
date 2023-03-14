using Bookify.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure
{
    public class BookifyDbContext : DbContext
    {
        public BookifyDbContext(DbContextOptions<BookifyDbContext> options) :
    base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Merchant> Merchants { get; set; }

        public override int SaveChanges()
        {
            HandleDelete();

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleDelete();

            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void HandleDelete()
        {
            var entities = ChangeTracker.Entries<ISoftDelete>()
                                .Where(e => e.State == EntityState.Deleted);

            foreach (var entity in entities)
            {
                entity.State = EntityState.Modified;

                var model = entity.Entity;

                model.IsDeleted = true;
                model.DeletedAt = DateTime.UtcNow;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(options =>
            {
                options.HasData(
                  new Author { FullName = "William Shakespeare", DateOfBirth = new DateTime(1564, 4, 26) },
                  new Author { FullName = "Charles Dickens" },
                  new Author { FullName = "Fyodor Dostoevsky" },
                  new Author { FullName = "Ernest Hemingway" },
                  new Author { FullName = "Hristo Botev", DateOfBirth = new DateTime(1848, 1, 6) });
            });

            modelBuilder.Entity<Merchant>(options =>
            {
                options.HasData(
                  new Merchant("Amazon") { DateFounded = new DateTime(1994, 7, 5) },
                  new Merchant("Ozone") { DateFounded = new DateTime(2008, 10, 10) },
                  new Merchant("Powell's Books") { DateFounded = new DateTime(1971, 1, 1) });
            });
        }
    }
}