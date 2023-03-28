using LayeringBookAPI.Models;
namespace LayeringBookAPI.Repository
{
    public interface IRepoClient<Client>
    {
        public List<Client> GetClients();        
        public Client Get(int id);
        //public void Delete(Book book);
        public void Update(Client cl);
        public void Add(Client cl);
    }
}
