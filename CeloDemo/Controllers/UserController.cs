using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using AutoMapper;
using CeloDemo.Services;
using CeloDemo.Entities;
using CeloDemo.Models;

namespace CeloDemo.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        static readonly HttpClient client = new HttpClient();
        public Rootobject Branches { get; private set; }

        public UserController(IHttpClientFactory clientFactory,
            IMapper mapper, IUserRepository userRepository)
        {
            this._clientFactory = clientFactory;
            this._mapper = mapper;
            this._userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository)); 
        }

        // summary: fetch data from https://randomuser.me , each time fetch 5 users.
        // add users and save data to database
        [HttpGet("index", Name =nameof(FetchNewUsers))]
        public async Task<IActionResult> FetchNewUsers()
        {
            //  generate 5 random users 
            string url = "https://randomuser.me/api/?results=5";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Branches = await response.Content
                .ReadAsAsync<Rootobject>();

                // add user
                var userList = _mapper.Map<IEnumerable<User>>(Branches.results);
                foreach (var item in userList)
                {
                    _userRepository.Add(item);
                }
            }
            return CreatedAtRoute(nameof(FetchNewUsers), Branches);
        }

        // get user list, default number of the users is 10
        // there should be one or more query string, including "number", "firstname" or "lastname"
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] FilterViewModel filter)
        {
            try
            {
                var users = await _userRepository.GetUserList(filter);
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userRepository.Get(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody]UserViewModel model, int userId)
        {
            var user = await _userRepository.Get(userId);
            if (user == null)
            {
                return NotFound();
            }
            _mapper.Map(model, user);
            _userRepository.Update(user);
            return Ok(user);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _userRepository.Get(userId);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.Delete(userId);
            return NoContent();
        }
    }
}