using CmsDemo.Poco;
using Microsoft.AspNetCore.Mvc;
using System;
using CmsDemo.Data;
using CmsDemo.Helpers;
using System.Linq;
using CmsDemo.Dbo;

namespace CmsDemo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LoginsController : ControllerBase
	{
		private readonly IUserRepository _userRepo;
		private readonly ILoginRepository _loginRepo;

		public LoginsController(IUserRepository userRepo, ILoginRepository loginRepo)
		{
			_userRepo = userRepo;
			_loginRepo = loginRepo;
		}

		IActionResult GetInvalidLogin()
		{
			return BadRequest(new ErrorResponse
			{
				Message = "User name or password is incorrect"
			});
		}

		[HttpPost]
		public IActionResult Post(LoginRequest request)
		{
			UserReadDbo user = _userRepo.GetUserByUserName(request.UserName);

			if (user == null)
			{
				return GetInvalidLogin();
			}

			byte[] hash = Hash.Get(request.Password, user.Nonce);

			if (!hash.SequenceEqual(user.Password))
			{
				return GetInvalidLogin();
			}

			DateTime now = DateTime.UtcNow;

			Guid id = Guid.NewGuid();

			LoginDbo dbo = new LoginDbo
			{
				Id = id.ToByteArray(),
				UserId = user.Id,
				CreatedAt = now
			};

			_loginRepo.CreateLogin(dbo);

			LoginResponse ret = new LoginResponse
			{
				LoginId = id.ToString()
			};

			return Ok(ret);
		}
	}
}
