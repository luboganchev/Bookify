namespace Bookify.Entities
{
    public class Book :ISoftDelete
    {
        public Book(string title, int pagesCount, Guid authorId)
        {
            Id = Guid.NewGuid();
            Title = title;
            PagesCount = pagesCount;
            AuthorId = authorId;
            Merchants = new HashSet<Merchant>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int PagesCount { get; set; }

        public Guid AuthorId { get; set; }

        public Author Author { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Merchant> Merchants { get; set; }
    }
}