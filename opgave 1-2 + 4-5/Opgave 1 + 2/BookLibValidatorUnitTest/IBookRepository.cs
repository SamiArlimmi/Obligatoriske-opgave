using BookLib;
using ObligatoriskOpgave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBook
{
    public interface IBookRepository
    {
        Book Add(Book book);
        Book? Remove(int id);
        Book? Update (int id, Book book);
        IEnumerable<Book> Get(int? PriceFilter = null, string? TitleIncludes = null, string? orderBy = null);
        Book? GetById (int id);

    }
}
