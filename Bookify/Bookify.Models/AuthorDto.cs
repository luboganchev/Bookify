namespace Bookify.Models
{
    public class AuthorDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }
    }
}
