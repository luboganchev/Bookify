using FluentValidation;

namespace Bookify.Models
{
    public class AuthorInputModel
    {
        public string FullName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }
    }

    public class AuthorInputModelValidator : AbstractValidator<AuthorInputModel>
    {
        public AuthorInputModelValidator()
        {
            RuleFor(x => x.FullName).NotNull().NotEmpty().Length(1, 100);
            RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(DateTime.UtcNow.AddYears(-10));
        }
    }
}
