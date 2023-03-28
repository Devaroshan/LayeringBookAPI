using LayeringBookAPI.Models;
using LayeringBookAPI.Repository;

namespace LayeringBookAPI.Services
{
    public interface ISerClient<Client>
    {
        public List<Client> GetClients();
        public Client Auth(login l);
        public Client Get(int id);
        public void Update(Client cl);
        public void Add(Client cl);
    }
}
