using LayeringBookAPI.Models;
using LayeringBookAPI.Repository;
using Library_MVC_API.Models;

namespace LayeringBookAPI.Services
{
    public class SerBook : ISerBook<Book>
    {
        private readonly IRepoBook<Book> _repoBook;
        public SerBook(IRepoBook<Book> repoBook) 
        {
            _repoBook = repoBook;
        }
        public void Add(Book book)
        {
            try
            {
                _repoBook.Add(book);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<Book> Fav(Fav b) 
        {
            var result = new List<Book>();
            var books = _repoBook.GetBooks();
            if (b.Author == null)
            {
                result = (from i in books
                          where i.Jonour == b.Jonour
                          select i).ToList();
                result.AddRange(from i in books
                                where i.Jonour != b.Jonour
                                select i);
            }
            else if (b.Jonour == null)
            {
                result = (from i in books
                          where i.Author == b.Author
                          select i).ToList();
                result.AddRange(from i in books
                                where i.Author != b.Author
                                select i);
            }
            else
            {
                result = (from i in books
                          where i.Author == b.Author && i.Jonour == b.Jonour
                          select i).ToList();
                result.AddRange(from i in books
                                where i.Author != b.Author || i.Jonour != b.Jonour
                                select i);
            }
            result = (from i in result
                      where i.NoofCopies > 0
                      select i).ToList();
            return result;

        }

        public void Delete(Book book)
        {
            try
            {
                _repoBook?.Delete(book);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Book Get(string id)
        {
            return _repoBook.Get(id);
        }

        public List<Book> GetBooks()
        {
            return _repoBook.GetBooks();
        }

        public void Update(Book book)
        {
            try
            {
                _repoBook.Update(book);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
