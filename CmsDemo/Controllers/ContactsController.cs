using CmsDemo.Poco;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using CmsDemo.Data;
using System.Linq;
using CmsDemo.Logic;
using CmsDemo.Helpers;
using CmsDemo.Global;
using CmsDemo.Dbo;

namespace CmsDemo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ContactsController : ControllerBase
	{
		private readonly IContactRepository _repo;
		private readonly AuthenticationLogic _auth;

		public ContactsController(IContactRepository repo, IUserRepository userRepository, ILoginRepository loginRepository)
		{
			_repo = repo;
			_auth = new AuthenticationLogic(userRepository, loginRepository);
		}

		[HttpGet]
		public IActionResult Get([FromHeader] string authorization)
		{
			UserReadDbo user = _auth.ValidateUser(authorization);

			if (user == null)
				return _auth.GetError();

			IEnumerable<Contact> ret = _repo.GetContacts(user.Id).Select(item => new Contact
			{
				Id = item.Id,
				Name = item.Name,
				Description = item.Description,
				Birthdate = item.Birthdate?.ToString("yyyy-MM-dd"),
				PrettyBirthdate = item.Birthdate?.ToString("MMMM dd, yyyy"),
				
				CreatedAt = item.CreatedAt.ToLocalTime().ToString("F"),
				UpdatedAt = item.CreatedAt.ToLocalTime().ToString("F"),

				Favorite = item.Favorite,
				GroupId = item.GroupId
			});

			return Ok(ret);
		}

		private IActionResult GetError(NewContactRequest request)
		{
			if (string.IsNullOrEmpty(request.Name))
				return Error.BadRequest("Name is required");

			if (request.Name.Length > Max.ContactName)
				return Error.BadRequest("Name is too long");

			if (request.Description != null && request.Description.Length > Max.ContactDescription)
				return Error.BadRequest("Description is too long");

			return null;
		}

		[HttpPut]
		public IActionResult Put(UpdateContactRequest request, [FromHeader] string authorization)
		{
			IActionResult error = GetError(request);

			if (error != null)
				return error;

			UserReadDbo user = _auth.ValidateUser(authorization);

			if (user == null)
				return _auth.GetError();

			DateTime? birthdate = null;

			if (!string.IsNullOrEmpty(request.Birthdate))
			{
				DateTime dt;
				if (DateTime.TryParse(request.Birthdate, out dt))
				{
					birthdate = dt;
				}
				else
				{
					return Error.BadRequest("Invalid birthdate");
				}
			}

			DateTime now = DateTime.UtcNow;

			ContactUpdateDbo dbo = new ContactUpdateDbo
			{
				Name = request.Name,
				Birthdate = birthdate,
				Favorite = request.Favorite,
				GroupId = request.GroupId,
				Description = request.Description,
				UpdatedAt = now,
			};

			if (!_repo.UpdateContact(dbo, request.Id))
				return NotFound();

			return Ok();
		}

		[HttpPost]
		public IActionResult Post(NewContactRequest request, [FromHeader] string authorization)
		{
			UserReadDbo user = _auth.ValidateUser(authorization);

			if (user == null)
				return _auth.GetError();

			DateTime? birthdate = null;

			if (!string.IsNullOrEmpty(request.Birthdate))
			{
				DateTime dt;
				if (DateTime.TryParse(request.Birthdate, out dt))
				{
					birthdate = dt;
				}
				else
				{
					return Error.BadRequest("Invalid birthdate");
				}
			}

			DateTime now = DateTime.UtcNow;

			ContactCreateDbo dbo = new ContactCreateDbo
			{
				UserId = user.Id,
				Name = request.Name,
				Birthdate = birthdate,
				Favorite = request.Favorite,
				GroupId = request.GroupId,
				Description = request.Description,
				CreatedAt = now,
				UpdatedAt = now,
			};

			_repo.CreateContact(dbo);

			return Ok();
		}

		[HttpDelete]
		public IActionResult Delete(DeleteContactRequest request, [FromHeader] string authorization)
		{
			UserReadDbo user = _auth.ValidateUser(authorization);

			if (user == null)
				return _auth.GetError();

			if (!_repo.DeleteContact(request.Id, user.Id))
				return NotFound();

			return Ok();
		}
	}
}
