using LayeringBookAPI.Models;
using LayeringBookAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LayeringBookAPI.Services
{
    public class SerClient : ISerClient<Client>
    {
        private readonly IRepoClient<Client> _repoCl;
        public SerClient()
        {
        }
        public SerClient(IRepoClient<Client> repoCl) 
        {
            _repoCl = repoCl;
        }

        public Client Auth(login l)
        {
            var c = _repoCl.GetClients();
            var result = (from i in c
                          where i.Userid == l.Userid && i.Password == l.Password
                          select i).SingleOrDefault();
            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }
        public void Add(Client cl)
        {
            try
            {
                _repoCl.Add(cl);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Client Get(int id)
        {
            return _repoCl.Get(id);
        }
        public void Update(Client cl)
        {
            try
            {
                _repoCl.Update(cl);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<Client> GetClients()
        {
            return _repoCl.GetClients();
        }
    }
}
