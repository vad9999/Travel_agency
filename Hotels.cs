﻿using System;
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
        public string Country {  get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public string PathImage { get; set; } = null!;
        public bool IsArchive { get; set; }
        public List<Reservation> Reservations { get; set; } = null!;
    }
}
