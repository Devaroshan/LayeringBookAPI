using LayeringBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LayeringBookAPI.Repository
{
    public class RepoBook : IRepoBook<Book>
    {
        private readonly LibraryContext db;
        public RepoBook(LibraryContext _db) 
        {
            db = _db;
        }
        public void Add(Book book)
        {
            try
            {
                db.Books.Add(book);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                string error = e.InnerException.ToString();
                if (error.Contains("PRIMARY"))
                {
                    throw new Exception("Book Id already exists");
                }else if (error.Contains("unique_Bname"))
                {
                    throw new Exception("Book already exists");
                }
                throw new Exception(e.Message);
            }
        }

        public void Delete(Book book)
        {
            try
            {
                db.Books.Remove(book);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Book Get(string id)
        {
            return db.Books.Find(id);
        }

        public List<Book> GetBooks()
        {
            return db.Books.ToList();
        }

        public void Update(Book book)
        {
            try
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                string error = e.InnerException.ToString();
                if (error.Contains("unique_Bname"))
                {
                    throw new Exception("Book name already exists");
                }
                throw new Exception(e.Message);
            }
        }
    }
}
