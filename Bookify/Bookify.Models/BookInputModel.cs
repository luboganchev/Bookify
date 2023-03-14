namespace Bookify.Models
{
    public class BookInputModel
    {
        public string Title { get; set; } = string.Empty;

        public int PagesCount { get; set; }

        public Guid AuthorId { get; set; }
    }
}
