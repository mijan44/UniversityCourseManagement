using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityCourseManagement.Data;
using UniversityCourseManagement.Helpers;
using UniversityCourseManagement.Models;

namespace UniversityCourseManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public UserController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult>PostUser(User userRequest)
		{
			PasswordHasher hash = new PasswordHasher();
			var existUser = _context.Users.FirstOrDefault(x => x.UserPassword == userRequest.UserPassword || x.UserEmail == userRequest.UserEmail);
			if (existUser == null)
			{
				var pass = hash.checkPasswordStrength(userRequest.UserPassword);
				if(!string.IsNullOrEmpty(pass))
				{
					return BadRequest(new { message = pass });
				}

				if (userRequest.Id == null || userRequest.Id == new Guid("00000000-0000-0000-0000-000000000000"))
				{
					var user = new User();
					user.Id = Guid.NewGuid();
					user.UserName = userRequest.UserName;
					user.UserAddress = userRequest.UserAddress;
					user.UserEmail = userRequest.UserEmail;
					user.UserContact = userRequest.UserContact;
					
					user.UserPassword = hash.HashPassword(userRequest.UserPassword);


					_context.Users.Add(user);

					await _context.SaveChangesAsync();
					return Ok(user);
				}
				
			}
			return Ok("User Already Exist");

		}







		[HttpPost]
		[Route("UserLogin")]
		public async Task<IActionResult> UserLogin(string UserEmail, string UserPassword)
		{
			var checkPassword = false;
			PasswordHasher hash = new PasswordHasher();
			var userLogin = _context.Users.FirstOrDefault(x=>x.UserEmail == UserEmail );
			if (userLogin != null)
			{
				checkPassword = hash.VerifyPassword(UserPassword,userLogin.UserPassword);
			}
			return Ok(checkPassword);

		}
		


	}
}
