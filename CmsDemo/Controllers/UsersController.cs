using CmsDemo.Poco;
using Microsoft.AspNetCore.Mvc;
using System;
using CmsDemo.Data;
using CmsDemo.Helpers;
using CmsDemo.Global;
using System.Linq;
using CmsDemo.Dbo;

namespace CmsDemo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly IUserRepository _userRepo;
		private readonly ILoginRepository _loginRepo;

		public UsersController(IUserRepository userRepository, ILoginRepository loginRepository)
		{
			_userRepo = userRepository;
			_loginRepo = loginRepository;
		}

		[HttpPost]
		public IActionResult Post(LoginRequest request)
		{
			if (string.IsNullOrEmpty(request.UserName))
			{
				return Error.BadRequest("User name is required");
			}

			if (request.UserName.Length > Max.UserName)
			{
				return Error.BadRequest("User name is too long");
			}

			if (string.IsNullOrEmpty(request.Password))
			{
				return Error.BadRequest("Password is required");
			}

			if (request.Password.Length > Max.Password)
			{
				return Error.BadRequest("Password is too long");
			}

			if (request.Password.Length < Min.Password)
			{
				return Error.BadRequest("Password is too short");
			}

			if (request.UserName == request.Password)
			{
				return Error.BadRequest("User name and password cannot be the same");
			}

			if (_userRepo.HasUser(request.UserName))
			{
				return BadRequest(new ErrorResponse { Message = "User name is already in use" });
			}

			if (!request.Password.Any(c => char.IsNumber(c)))
			{
				return Error.BadRequest("Password must contain a number");
			}

			if (!request.Password.Any(c => !char.IsNumber(c) & !char.IsLetter(c)))
			{
				return Error.BadRequest("Password must contain a symbol");
			}

			if (!request.Password.Any(c => char.IsUpper(c)))
			{
				return Error.BadRequest("Password must contain an upper-case letter");
			}

			if (!request.Password.Any(c => char.IsLower(c)))
			{
				return Error.BadRequest("Password must contain a lower-case letter");
			}

			DateTime now = DateTime.UtcNow;

			long nonce = Hash.RandomLong();

			UserWriteDbo dbo = new UserWriteDbo
			{
				UserName = request.UserName,
				Nonce = nonce,
				Password = Hash.Get(request.Password, nonce),
				CreatedAt = now
			};

			_userRepo.CreateUser(dbo);

			UserReadDbo user = _userRepo.GetUserByUserName(request.UserName);

			Guid id = Guid.NewGuid();

			LoginDbo loginDbo = new LoginDbo
			{
				Id = id.ToByteArray(),
				UserId = user.Id,
				CreatedAt = now
			};

			_loginRepo.CreateLogin(loginDbo);

			LoginResponse response = new LoginResponse
			{
				LoginId = id.ToString()
			};

			return Ok(response);
		}
	}
}
