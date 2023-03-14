namespace Bookify.Models
{
    public class BookDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int PagesCount { get; set; }

        public string AuthorName { get; set; } = string.Empty;
    }
}