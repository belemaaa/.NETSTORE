using System;
using _netstore.Data;
using _netstore.DTO;
using _netstore.Models;
using AutoMapper;
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

        public AccountController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            this._context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
        }

        [HttpPost("login")]
        [ProducesResponseType(200, Type=typeof(User))]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid credentials");
            }
            var mappedUser = _mapper.Map<UserDto>(user);
            return Ok(new { message = "Login was successful", User = mappedUser });
        }
            

        [HttpPost("signup")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Signup(SignupDTO signupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUser = await _userManager.FindByEmailAsync(signupDto.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "User with this email already exists" });
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
            return Ok(new { message = "New user has been created successfully" });
        }

    }
}

