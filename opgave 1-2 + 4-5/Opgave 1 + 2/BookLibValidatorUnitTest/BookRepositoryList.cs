using ObligatoriskOpgave;
using IBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookLib
{
    public class BookRepositoryList : IBookRepository
    {
        private int _nextId = 1;
        private readonly List<Book> _book = new();

        public BookRepositoryList()
        {

        }

        public IEnumerable<Book> Get(int? PriceFilter = null, string? TitleIncludes = null, string? orderBy = null)
        {
            IEnumerable<Book> result = new List<Book>(_book);
            // Filtering
            if (PriceFilter != null)
            {
                result = result.Where(b => b.Price > PriceFilter);
            }
            if (TitleIncludes != null)
            {
                result = result.Where(b => b.Title.Contains(TitleIncludes));
            }

            // Ordering aka. sorting
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "title": // fall through to next case
                    case "title_asc":
                        result = result.OrderBy(b => b.Title);
                        break;
                    case "title_desc":
                        result = result.OrderByDescending(b => b.Title);
                        break;
                    case "price":
                    case "price_asc":
                        result = result.OrderBy(b => b.Price);
                        break;
                    case "price_desc":
                        result = result.OrderByDescending(b => b.Price);
                        break;
                    default:
                        break; // do nothing
                        //throw new ArgumentException("Unknown sort order: " + orderBy);
                }
            }
            return result;
        }

        public Book? GetById(int id)
        {
            return _book.Find(book => book.Id == id);
        }

        public Book Add(Book book)
        {
            book.Validate();
            book.Id = _nextId++;
            _book.Add(book);
            return book;
        }

        public Book? Remove(int id)
        {
            Book? book = GetById(id);
            if (book == null)
            {
                return null;
            }
            _book.Remove(book);
            return book;
        }

        public Book? Update(int id, Book book)
        {
            book.Validate();
            Book? existingBook = GetById(id);
            if (existingBook == null)
            {
                return null;
            }
            existingBook.Title = book.Title;
            existingBook.Price = book.Price;
            return existingBook;
        }
    }
}
