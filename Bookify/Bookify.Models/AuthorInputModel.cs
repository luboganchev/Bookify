namespace Bookify.Models
{
    public class AuthorInputModel
    {
        public string FullName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }
    }
}
