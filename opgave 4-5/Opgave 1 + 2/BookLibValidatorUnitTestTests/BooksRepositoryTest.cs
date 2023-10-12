using ObligatoriskOpgave;
using IBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLib;

namespace ObligatoriskOpgave
{
    [TestClass()]
    public class BookTests
    {
        private IBookRepository _repo;
        private readonly Book _badBook = new() { Title = "Tom & Jerry", Price = 1250 };

        [TestInitialize()]
        public void Init()
        {
            _repo = new BookRepositoryList();

            _repo.Add(new Book() { Title = "Tom & Jerry", Price = 29 });
            _repo.Add(new Book() { Title = "Biljagten", Price = 309 });
            _repo.Add(new Book() { Title = "C# Masterlcass", Price = 999 });
            _repo.Add(new Book() { Title = "Guide to Fl Studio", Price = 899 });
            _repo.Add(new Book() { Title = "Mujafa og kamelen", Price = 99 });
        }

        [TestMethod()]
        public void GetTest()
        {
            IEnumerable<Book> books = _repo.Get();
            Assert.AreEqual(5, books.Count());
            Assert.AreEqual(books.First().Title, "Tom & Jerry");

            IEnumerable<Book> sortedBooks = _repo.Get(orderBy: "title");
            Assert.AreEqual(sortedBooks.First().Title, "Biljagten");

            IEnumerable<Book> sortedBooks2 = _repo.Get(orderBy: "price");
            Assert.AreEqual(books.First().Price, 29);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.IsNotNull( _repo.GetById(1));
            Assert.IsNull( _repo.GetById(20));
        }

        [TestMethod()]
        public void AddTest()
        {
            Book b = new() { Title = "test", Price = 100 };
            Assert.AreEqual(6, _repo.Add(b).Id);
            Assert.AreEqual(6, _repo.Get().Count());

            Assert.ThrowsException<ArgumentOutOfRangeException> (() => _repo.Add(_badBook));
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Assert.IsNull(_repo.Remove(20));
            Assert.AreEqual(1, _repo.Remove(1)?.Id);
            Assert.AreEqual(4, _repo.Get().Count());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.AreEqual(5, _repo.Get().Count());
            Book b = new() { Title = "Test", Price = 999 };
            Assert.IsNull(_repo.Update(100, b));
            Assert.AreEqual(1, _repo.Update(1, b)?.Id);
            Assert.AreEqual(5, _repo.Get().Count());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Update(1, _badBook));
        }




    }
}
