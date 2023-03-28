using LayeringBookAPI.Models;
using LayeringBookAPI.Repository;

namespace LayeringBookAPI.Services
{
    public interface ISerBooking<Booking>
    {
        public List<Booking> GetBookings();
        public Booking Orders(Booking b);
        public List<Booking> UPOrders(Client cl);
        public List<Booking> POrders(Client cl);
        public Booking Get(int id);
        public void Delete(Booking bk);
        public void Update(Booking bk);
        public void Add(Booking bk);
    }
}
