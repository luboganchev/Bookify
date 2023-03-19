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
            AuthorId = Guid.Parse("2788DCBA-EEBF-4C87-9D73-35DD187C2277")
        };
    }
}
