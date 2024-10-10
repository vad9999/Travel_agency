using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public bool GetBlockUser(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email).Blocking;
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetUserByEmail(string email) 
        {
            return _context.Users.FirstOrDefault(u => u.Email == email); /*return _context.Users.Find(email);*/
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
