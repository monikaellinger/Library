namespace Library.Web.Models
{
    public class BookModel
    {
        public int BookID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Pages { get; set; }
        public bool CurrentlyRented { get; set; }

        public BookModel()
        {
        }

        public BookModel(int bookID, string title, string author, int year, int pages)
        {
            BookID = bookID;
            Title = title;
            Author = author;
            Year = year;
            Pages = pages;
        }

    }
}
