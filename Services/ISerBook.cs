using LayeringBookAPI.Repository;
using Library_MVC_API.Models;

namespace LayeringBookAPI.Services
{
    public interface ISerBook<Book>
    {
        public List<Book> GetBooks();
        public List<Book> Fav(Fav b);
        public Book Get(string id);
        public void Delete(Book book);
        public void Update(Book book);
        public void Add(Book book);
    }
}
