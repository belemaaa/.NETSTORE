using System;
using _netstore.Data;
using _netstore.DTO;
using _netstore.Models;
using _netstore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _netstore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public AccountController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, TokenService tokenService)
        {
            this._context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
            this._tokenService = tokenService;
        }

        [HttpPost("login")]
        [ProducesResponseType(200, Type=typeof(UserDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, ModelState);
            }
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return StatusCode(401, new {message = "Invalid credentials"});
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if (!result.Succeeded)
            {
                return StatusCode(401, new { message = "Invalid credentials" });
            }
            var dto = new UserDTO
            {
                Id = user.Id,
                Token = await _tokenService.GenerateToken(user),
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return StatusCode(200, new { message = "Login was successful", User = dto });
        }
            

        [HttpPost("signup")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Signup(SignupDTO signupDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, ModelState);
            }

            var existingEmail = await _userManager.FindByEmailAsync(signupDto.Email);
            var existingUsername = await _userManager.FindByNameAsync(signupDto.Username);
            if (existingEmail != null)
            {
                return StatusCode(409, new { message = "User with these credentials already exists" });
            }
            else if (existingUsername != null)
            {
                return StatusCode(409, new { message = "User with these credentials already exists" });
            }

            var newUser = new User
            {
                UserName = signupDto.Username,
                Email = signupDto.Email,
                PhoneNumber = signupDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(newUser, signupDto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }
            await _userManager.AddToRoleAsync(newUser, "Member");
            return StatusCode(201, new { message = "New user has been created successfully" });
        }


        [Authorize]
        [HttpGet("currentUser")]
        [ProducesResponseType(200, Type=typeof(UserDTO))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return StatusCode(401, new { message = "Unauthorized" });
            }
            var dto = new UserDTO
            {
                Token = await _tokenService.GenerateToken(user),
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
            return StatusCode(200, new { user = dto });
        }

    }
}

 