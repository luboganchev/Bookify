using Bookify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.ApiTests.TestData
{
    public class TestData
    {
        public static BookInputModel DefauldBookData = new BookInputModel
        {
            Title = "Test Book",
            PagesCount = 100,
            AuthorId = Guid.Parse("86902C7C-C31A-4A4F-95E1-3EECA9C39E6E")
        };
    }
}
