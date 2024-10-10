using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public class Hotels
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Tours Tours { get; set; } = null!;
        public List<Reservation> Reservations { get; set; } = null!;
    }
}
