using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityCourseManagement.Data;
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
			var existUser = _context.Users.FirstOrDefault(x => x.UserPassword == userRequest.UserPassword || x.UserEmail == userRequest.UserEmail);
			if (existUser == null)
			{
				if (userRequest.Id == null || userRequest.Id == new Guid("00000000-0000-0000-0000-000000000000"))
				{
					var user = new User();
					user.Id = Guid.NewGuid();
					user.UserName = userRequest.UserName;
					user.UserAddress = userRequest.UserAddress;
					user.UserEmail = userRequest.UserEmail;
					user.UserContact = userRequest.UserContact;
					user.UserPassword = userRequest.UserPassword;


					_context.Users.Add(user);

					await _context.SaveChangesAsync();
					return Ok(user);
				}
				
			}
			return Ok("User Already Exist");

		}
		


	}
}
