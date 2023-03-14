namespace Bookify.Entities
{
    public class Merchant
    {
        public Merchant(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Books = new HashSet<Book>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime? DateFounded { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
