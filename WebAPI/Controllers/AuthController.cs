using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.DTOs;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Services.Abstractions;
using BC = BCrypt.Net.BCrypt;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper mapper;
        public AuthController(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterUserDto regUser)
        {
            if (_userRepository.GetUserByEmail(regUser.Email) != null)
            {
                return BadRequest("User already exists");
            }
            var user = mapper.Map<User>(regUser);
            user.PasswordHash = BC.HashPassword(user.PasswordHash);
            user.IsActive = true;
            user.CreatedAt = DateTime.Now;
            if(regUser.UserRoles!=null && regUser.UserRoles.Count>0)
            {
                 foreach(string r in regUser.UserRoles)
               {
                    user.Roles.Add(new Role() { RoleName = r });
               }
           }
            if (_userRepository.RegisterUser(user))
            {
                return Ok("User registered successfully");
            }
            return BadRequest("Failed to register user");
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto user)
        {
            var existingUser = _userRepository.GetUserByEmail(user.Email);
            if (existingUser == null)
            {
                return BadRequest("User does not exist");
            }
            bool isPasswordMatched = BC.Verify(user.PasswordHash, existingUser.PasswordHash);
            if (isPasswordMatched)
            {
                UserDto userModel = new UserDto
                {
                    Name = existingUser.Username,
                    Email = existingUser.Email,
                    Roles = existingUser.Roles.Select(x => x.RoleName).ToArray()
                };
                userModel.Token = _tokenService.CreateToken(existingUser);
                return Ok(userModel);
            }
            return BadRequest("Invalid credentials");
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userRepository.GetAll());
        }
    }
}
