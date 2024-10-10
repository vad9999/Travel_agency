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
        public User User { get; set; } = null!;
        public Tours Tours { get; set; } = null!;
        public Hotels Hotels { get; set; } = null!;
    }
}
