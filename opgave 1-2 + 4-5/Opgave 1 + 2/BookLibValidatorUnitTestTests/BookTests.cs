using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObligatoriskOpgave;

namespace ObligatoriskOpgave.Tests
{
    [TestClass()]
    public class BookTests
    {
        private Book book = new Book { Id = 1, Title = "ABC", Price = 1 };
        private Book bookPrice = new Book { Id = 2, Title = "A", Price = 3000 };  
        private Book bookTitleNull = new Book { Id = 3, Title = null, Price = 200 };
        private Book bookTitleShort = new Book { Id = 4, Title = "2", Price = 500 };

        [TestMethod()]
        public void ToStringTest()
        {
            string str = book.ToString();   // act
            Assert.AreEqual("1 ABC 1", str);  // assert
        }

        [TestMethod()]
        public void ValidatePriceTest()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bookPrice.ValidatePrice());

        }

        [TestMethod()]
        public void ValidateTitleTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => bookTitleNull.ValidateTitle());
            Assert.ThrowsException<ArgumentException>(() => bookTitleShort.ValidateTitle());
        }

        [TestMethod()]
        public void ValidateTest()
        {
            book.Validate();
        }


    }
}