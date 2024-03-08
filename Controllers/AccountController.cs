using System;
using _netstore.Data;
using _netstore.DTO;
using _netstore.Models;
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

        public AccountController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
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
            if (user != null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (checkPassword)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Ok(new { Message = "Login was successful", User = user });
                    }
                }
                return Unauthorized("Invalid credentials");
            }
            return NotFound("User does not exist");
            
        }

        [HttpPost("signup")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Signup(SignupDTO signupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUser = await _userManager.FindByEmailAsync(signupDto.Email);
            if (existingUser != null)
            {
                var newUser = new User
                {
                    UserName = signupDto.Username,
                    Email = signupDto.Email,
                    PhoneNumber = signupDto.PhoneNumber
                };
                var result = await _userManager.CreateAsync(newUser, signupDto.Password);
                if (result.Succeeded)
                {
                    return Ok(new { Message = "New user has been created successfully" });
                }   
            }
            return Unauthorized(new { message = "User with these credentials already exists" });
        }

    }
}

