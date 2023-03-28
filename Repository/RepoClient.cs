using LayeringBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LayeringBookAPI.Repository
{
    public class RepoClient : IRepoClient<Client>
    {
        private readonly LibraryContext db;
        public RepoClient(LibraryContext _db) 
        {
            db = _db;
        }
        public void Add(Client cl)
        {
            try
            {
                db.Clients.Add(cl);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                string error = e.InnerException.ToString();
                if (error.Contains("unique_UId"))
                {
                    throw new Exception("Email Id already exists");
                }
                throw new Exception(e.Message);
            }
        }

        public Client Get(int id)
        {
            return db.Clients.Find(id);
        }
        public void Update(Client cl)
        {
            try
            {
                db.Entry(cl).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception e) 
            {
                string error = e.InnerException.ToString();
                if (error.Contains("unique_UId"))
                {
                    throw new Exception("Email Id already exists");
                }
                throw new Exception(e.Message);
            }
            
        }
        public List<Client> GetClients()
        {
            return db.Clients.ToList();
        }

    }
}
