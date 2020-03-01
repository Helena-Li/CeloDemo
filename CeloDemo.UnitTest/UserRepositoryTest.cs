using System;
using Xunit;
using CeloDemo.Services;
using CeloDemo.Data;
using CeloDemo.Models;
using Microsoft.EntityFrameworkCore;
using CeloDemo.Entities;

namespace CeloDemo.UnitTest
{
    public class UserRepositoryTest : IDisposable
    {
        private readonly string[] _names =
        {
            "Alice", "Bob", "Cris", "David", "Emma", "Allen", "Oliver", "Olivia", "Ollie", "Oli",
            "Jessica", "Peter", "Cristiano", "Linda", "Pat"
        };

        private readonly UserDbContext _context;
        private readonly UserRepository _userRepository;

        public UserRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: "UserDb")
                .Options;
            _context = new UserDbContext(options);
            for (var i = 0; i < 15; i++)
            {
                _context.Users.Add(new User
                {
                    Id = i + 1,
                    Title = i % 3 == 0 ? "Mr" : "Miss",
                    FirstName = _names[i],
                    LastName = _names[14 - i],
                    Email = $"{_names[i]}@gmail.com",
                    Birth = new DateTime(1981, 3, 1),
                    Phone = "02102373321",
                    LargePicture = $"https://randomuser.me/api/portraits/women/{i}.jpg",
                    ThumbnailPicture = $"https://randomuser.me/api/portraits/women/{i}.jpg"
                });
            }
            _context.SaveChanges();
            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public void NoQuery()
        {
            FilterViewModel filter = new FilterViewModel();
            var expect = 10;
            var actual = _userRepository.FilterUser(_context.Users, filter).CountAsync().Result;
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void FilterFirstName()
        {
            FilterViewModel filter = new FilterViewModel() { FirstName = "oli" };
            var expect = 3;
            var actual = _userRepository.FilterUser(_context.Users, filter).CountAsync().Result;
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void FilterLastName()
        {
            FilterViewModel filter = new FilterViewModel() { LastName = "CRIS" };
            var expect = 2;
            var actual = _userRepository.FilterUser(_context.Users, filter).CountAsync().Result;
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void AllQuery()
        {
            FilterViewModel filter = new FilterViewModel()
            {
                Number=2,
                LastName="Oli",
                FirstName="a"
            };
            var expect = 2;
            var actual = _userRepository.FilterUser(_context.Users, filter).CountAsync().Result;
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void InvalidNumber()
        {
            FilterViewModel filter = new FilterViewModel()
            {
                Number = -200,
                LastName = "oli",
                FirstName = "a"
            };
            var expect = 1;
            var actual = _userRepository.FilterUser(_context.Users, filter).CountAsync().Result;
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void ZeroNumber()
        {
            FilterViewModel filter = new FilterViewModel()
            {
                Number = 0,
                LastName = "oli",
                FirstName = "a"
            };
            var expect = 1;
            var actual = _userRepository.FilterUser(_context.Users, filter).CountAsync().Result;
            Assert.Equal(expect, actual);
        }

        public void Dispose()
        {
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();
        }

    }
}
