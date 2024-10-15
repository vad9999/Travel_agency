using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _context;
        public HotelRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddHotel(Hotels hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
        }
    }
}
