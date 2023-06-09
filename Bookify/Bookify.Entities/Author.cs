﻿namespace Bookify.Entities
{
    public class Author : ISoftDelete
    {
        public Author()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<Book> Book { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
