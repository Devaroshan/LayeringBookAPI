using LayeringBookAPI.Models;

namespace LayeringBookAPI.Repository
{
    public class RepoLibStaff : IRepoLibStaff<LibStaff>
    {
        private readonly LibraryContext db;
        public RepoLibStaff(LibraryContext _db) 
        {
            db = _db;
        }
        public List<LibStaff> GetStaff() 
        {
            return db.LibStaffs.ToList();
        }
        public LibStaff Get(int id)
        {
            return db.LibStaffs.Find(id);
        }
    }
}
