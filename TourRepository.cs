using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public class TourRepository : ITourRepository
    {
        private readonly AppDbContext _context;
        public TourRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Tours> GetAllTours()
        {
            return _context.Tours.ToList();
        }
        public void AddTour(Tours tour)
        {
            _context.Tours.Add(tour);
            _context.SaveChanges();
        }
        public Tours GetTourById(int id)
        {
            return _context.Tours.Find(id);
        }
        public void DeleteTour(int id)
        {
            var tour = GetTourById(id);
            if (tour != null)
            {
                _context.Tours.Remove(tour);
                _context.SaveChanges();
            }
        }
        public List<Tours> GetAllToursNonArchive()
        {
            List<Tours> tours = GetAllTours();
            List<Tours> nonarchive = new List<Tours>();
            for(int i = 1; i <= tours[tours.Count - 1].Id; i++)
            {
                if(GetTourById(i) != null)
                {
                    if (GetTourById(i).IsArchive == false)
                    {
                        nonarchive.Add(GetTourById(i));
                    }
                }
            }
            return nonarchive;
        }
        public List<Tours> GetAllToursArchive()
        {
            List<Tours> tours = GetAllTours();
            List<Tours> archive = new List<Tours>();
            for (int i = 1; i <= tours[tours.Count - 1].Id; i++)
            {
                if (GetTourById(i) != null)
                {
                    if (GetTourById(i).IsArchive == true)
                    {
                        archive.Add(GetTourById(i));
                    }
                }
            }
            return archive;
        }
        public void UpdateTour(Tours tour)
        {
            _context.Tours.Update(tour);
            _context.SaveChanges();
        }
    }
}
