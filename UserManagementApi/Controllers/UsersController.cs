using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Models;
using UserManagementApi.Validators;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> _users = new List<User>();
        private static int _nextId = 1;
        private readonly UserValidator _validator = new UserValidator();

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return Ok(_users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            ValidationResult result = _validator.Validate(user);
            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            user.Id = _nextId++;
            _users.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, User user)
        {
            var existing = _users.FirstOrDefault(u => u.Id == id);
            if (existing == null) return NotFound();

            user.Id = id;
            ValidationResult result = _validator.Validate(user);
            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            existing.Name = user.Name;
            existing.Email = user.Email;
            existing.Password = user.Password;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            _users.Remove(user);
            return NoContent();
        }
    }
} 