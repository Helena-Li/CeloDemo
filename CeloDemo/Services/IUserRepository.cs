using CeloDemo.Entities;
using CeloDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeloDemo.Services
{
    public interface IUserRepository
    {
        Task<User> Get(int id);
        Task<IEnumerable<User>> GetUserList(FilterViewModel filter);
        void Add(User user);
        void Update(User user);
        Task Delete(int id);

        IQueryable<User> FilterUser(IQueryable<User> model, FilterViewModel filter);
    }
}
