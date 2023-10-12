using System.Diagnostics;

namespace ObligatoriskOpgave
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return Id + " " + Title + " " + Price;
        }

        public void ValidatePrice()
        {
            if (0 > Price || Price >= 1200)
                throw new ArgumentOutOfRangeException("price can't be under 1 and over 1200: " + Price);
        }

        public void ValidateTitle()
        {
            if (Title == null)
                throw new ArgumentNullException("title is null");
            if (Title.Length < 3)
                throw new ArgumentException("title must be at least 3 character: " + Title);
        }

        public void Validate()
        {
            ValidatePrice();
            ValidateTitle();
        }
    }
}