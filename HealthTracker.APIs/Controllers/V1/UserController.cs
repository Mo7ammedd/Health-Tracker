using HealthTracker.APIs.DTOs;
using HealthTracker.Core;
using HealthTracker.Core.Entities;
using HealthTrracke.APIs.Controllers;
using HealthTrracke.APIs.Controllers.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthTracker.APIs.Controllers.V1
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Get users
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<User>>> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return Ok(users);
        }

        // Post user
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(UserDto user)
        {
            var _user = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
            await _unitOfWork.Repository<User>().AddAsync(_user);
            await _unitOfWork.CompleteAsync();

            return Ok(_user);
        }

        // Get user by id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _unitOfWork.Repository<User>().GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}