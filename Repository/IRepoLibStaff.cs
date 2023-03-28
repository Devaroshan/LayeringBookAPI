using LayeringBookAPI.Models;
namespace LayeringBookAPI.Repository
{
    public interface IRepoLibStaff<LibStaff>
    {
        public List<LibStaff> GetStaff();
        public LibStaff Get(int id);
    }
}
