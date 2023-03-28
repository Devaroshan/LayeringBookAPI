using LayeringBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LayeringBookAPI.Repository
{
    public class RepoBooking : IRepoBooking<Booking>
    {
        private readonly LibraryContext db;
        public RepoBooking(LibraryContext _db) 
        {
            db = _db;
        }
        public void Add(Booking bk)
        {
            try
            {
                db.Bookings.Add(bk);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(Booking bk)
        {
            try
            {
                db.Bookings.Remove(bk);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Booking Get(int id)
        {
            return db.Bookings.Find(id);
        }

        public List<Booking> GetBookings()
        {
            return db.Bookings.ToList();
        }

        public void Update(Booking bk)
        {
            try
            {
                db.Entry(bk).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
