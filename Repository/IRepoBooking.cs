using LayeringBookAPI.Models;
namespace LayeringBookAPI.Repository
{
    public interface IRepoBooking<Booking>
    {
        public List<Booking> GetBookings();        
        public Booking Get(int id);
        public void Delete(Booking bk);
        public void Update(Booking bk);
        public void Add(Booking bk);
    }
}
