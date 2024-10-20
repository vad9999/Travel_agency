using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public class Reservation
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public int TourOrHotelId { get; set; }
        public string TourOrHotel { get; set; } = null!;
        public bool IsConfirm { get; set; }
    }
}
