using CmsDemo.Data;
using CmsDemo.Dbo;
using CmsDemo.Poco;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CmsDemo.Logic
{
	public class AuthenticationLogic
	{
		private IUserRepository _userRepository;
		private ILoginRepository _loginRepository;

		public AuthenticationLogic(IUserRepository userRepository, ILoginRepository loginRepository)
		{
			_userRepository = userRepository;
			_loginRepository = loginRepository;
		}

		public UserReadDbo ValidateUser(string authHeader)
		{
			string auth = _GetAuth(authHeader);

			if (auth == null)
				return null;

			Guid guid;

			if (!Guid.TryParse(auth, out guid))
				return null;

			LoginDbo login = _loginRepository.GetLogin(guid);

			if (login == null)
				return null;

			return _userRepository.GetUserById(login.UserId);
		}

		private static string _GetAuth(string header)
		{
			if (header == null)
				return null;
			if (!header.StartsWith("BASIC "))
				return null;
			return header.Substring(6);
		}

		public IActionResult GetError()
		{
			return new UnauthorizedObjectResult(new ErrorResponse { Message = "You are not logged in" });
		}
	}
}
