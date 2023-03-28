using LayeringBookAPI.Models;
namespace LayeringBookAPI.Repository
{
    public interface IRepoBook<Book>
    {
        public List<Book> GetBooks();        
        public Book Get(string id);
        public void Delete(Book book);
        public void Update(Book book);
        public void Add(Book book);
    }
}
