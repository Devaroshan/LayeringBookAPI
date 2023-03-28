using LayeringBookAPI.Models;
using LayeringBookAPI.Repository;

namespace LayeringBookAPI.Services
{
    public class SerBooking : ISerBooking<Booking>
    {
        private readonly IRepoBooking<Booking> _repoBk;
        public SerBooking(IRepoBooking<Booking> repoBk) 
        {
            _repoBk = repoBk;
        }
        public void Add(Booking bk)
        {
            try
            {
                _repoBk.Add(bk);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Booking Orders(Booking b) 
        {
            var all_order = _repoBk.GetBookings();
            var bk1 = (from i in all_order
                       where i.Bid == b.Bid && i.Cid == b.Cid && i.Status == 0
                       select i).SingleOrDefault();
            if (bk1 != null )
            {
                return bk1;
            }
            else
            {
                return null;
            }

        }
        public List<Booking> UPOrders(Client cl)
        {
            var all_order = _repoBk.GetBookings();
            var bk1 = (from i in all_order
                       where i.Cid == cl.Cid && i.Status == 0
                       select i).ToList();
            if (bk1 != null)
            {
                return bk1;
            }
            else
            {
                return null;
            }

        }
        public List<Booking> POrders(Client cl)
        {
            var all_order = _repoBk.GetBookings();
            var bk1 = (from i in all_order
                       where i.Cid == cl.Cid && i.Status == 1
                       select i).ToList();
            if (bk1 != null)
            {
                return bk1;
            }
            else
            {
                return null;
            }

        }
        public void Delete(Booking bk)
        {
            try
            {
                _repoBk?.Delete(bk);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Booking Get(int id)
        {
            return _repoBk.Get(id);
        }

        public List<Booking> GetBookings()
        {
            return _repoBk.GetBookings();
        }

        public void Update(Booking bk)
        {
            try
            {
                _repoBk.Update(bk);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
