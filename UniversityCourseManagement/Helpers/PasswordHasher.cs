using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace UniversityCourseManagement.Helpers
{
	public class PasswordHasher
	{
		private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
		private static readonly int saltsize = 16;
		private static readonly int hashsize = 20;
		private static readonly int iteration = 10000;

		public  string HashPassword(string password)
		{
			byte[] salt;
			rng.GetBytes(salt = new byte[saltsize]);
			var key = new Rfc2898DeriveBytes(password,salt, iteration);
			var hash = key.GetBytes(hashsize);

			var hashbyte = new byte[saltsize +  hashsize];
			Array.Copy(salt,0, hashbyte, 0, saltsize);
			Array.Copy(hash,0, hashbyte, saltsize, hashsize);

			var base64hash = Convert.ToBase64String(hashbyte);

			return base64hash;

		}


		public bool VerifyPassword(string password,string base64hash)
		{
			var hashBytes = Convert.FromBase64String(base64hash );

			var salt = new byte[saltsize];
			Array.Copy(hashBytes,0, salt, 0, saltsize);

			var key = new Rfc2898DeriveBytes(password,salt, iteration);
			byte[] hash = key.GetBytes(hashsize);

			for(var i=0; i<hashsize; i++)
			{
				if (hashBytes[i+ saltsize]!= hash[i])
					return false;
				
			}
			return true;

		}


		public string checkPasswordStrength(string password)
		{
			StringBuilder sb = new StringBuilder();
			if (password.Length <8)
			{
				sb.Append("Minimum Password Length Should be 8" + Environment.NewLine);
			}
			if (!(Regex.IsMatch(password,"[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]") ))
			{
				sb.Append("Password Should Be Alphanumeric" + Environment.NewLine);
			}
			return sb.ToString();
		}


		public string CreateToken(string userName, string email)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("thisIsMySecurity..............");

			var identity = new ClaimsIdentity(new Claim[]
			{
				new Claim (ClaimTypes.Name, userName),
				new Claim(ClaimTypes.Email, email)

			});

			var credential = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);


			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = identity,
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = credential
			};

			var token = jwtTokenHandler.CreateToken(tokenDescriptor);
			return jwtTokenHandler.WriteToken(token);


		}


	}
}
