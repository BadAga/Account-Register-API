﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using UniversalUserAPI.Models;
using UniversalUserAPI.Models.DTOs;
using UniversalUserAPI.Models.Mappers;
using UniversalUserAPI.Models.Password_Manager;

namespace UniversalUserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UsersController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] RegisterDto _userDto)
        {
            //validation check            
            if(!ModelState.IsValid)
            {
                return BadRequest("Wrong input format");
            }
            //checking if its a new user--> checking if PESEL, email and phone number is unique 
            if (!UniqueEmail(_userDto.Email))
            {
                return BadRequest("Email is not unique");
            }
            if(!UniquePesel(_userDto.Pesel))
            {
                return BadRequest("PESEL is not unique");
            }
            if (!UniquePhoneNumber(_userDto.PhoneNumber))
            {
                return BadRequest("Phone number is not unique");
            }

            int newId = GetNewUserId();
            //password hashing
            PasswordManager passManager = new PasswordManager(_userDto.Password);
            _userDto.Password = passManager.GetComputedHashedPassword();
            User user = UserMapper.RegisterDtoToUser(ref _userDto,ref newId);
            
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        private int GetNewUserId()
        {
            if(!_context.Users.Any())
            {
                return 1;
            }

            return _context.Users.Max(x => x.UserId) + 1;
        }

        private bool UniqueEmail(String email)
        {
            return !_context.Users.Any(x => x.Email == email);
        }

        private bool UniquePesel(String pesel)
        {
            return !_context.Users.Any(x => x.Pesel == pesel);
        }

        private bool UniquePhoneNumber(String phoneNumber)
        {
            return !_context.Users.Any(x => x.PhoneNumber == phoneNumber);
        }
    }
}