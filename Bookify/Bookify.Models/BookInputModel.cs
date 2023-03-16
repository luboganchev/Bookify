using FluentValidation;

namespace Bookify.Models
{
    public class BookInputModel
    {
        public string Title { get; set; } = string.Empty;

        public int PagesCount { get; set; }

        public Guid AuthorId { get; set; }
    }

    public class BookInputModelValidator : AbstractValidator<BookInputModel>
    {
        public BookInputModelValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().Length(1, 256);
            RuleFor(x => x.AuthorId).NotNull().NotEmpty();
            RuleFor(x => x.PagesCount).GreaterThan(0).LessThan(5000);
        }
    }
}
