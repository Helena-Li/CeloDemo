using CeloDemo.Data;
using CeloDemo.Entities;
using CeloDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeloDemo.Services
{
    public class UserRepository:IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public async Task<User> Get(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetUserList(FilterViewModel filter)
        {
            var model = _context.Users.AsQueryable();
            var result = FilterUser(model, filter);
            return result;
        }

        public void Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var newUser = _context.Users.Attach(user);
            newUser.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<User> FilterUser(IQueryable<User> model, FilterViewModel filter)
        {
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                model = model.Where(x => x.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.LastName))
            {
                model = model.Where(x => x.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }
            int totalUser = model.Count();
            if (filter.Number <= 0)
            {
                return model.Take(1);
            }
            else if (totalUser <= filter.Number)
            {
                return model;
            }
            else
            {
                return model.Take(filter.Number);
            }
        }
    }
}
