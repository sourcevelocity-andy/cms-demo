using CmsDemo.Controllers;
using CmsDemo.Data;
using CmsDemo.Dbo;
using CmsDemo.Poco;
using CmsDemoUnitTesting.Helpers;
using CmsDemoUnitTesting.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace CmsDemoUnitTesting
{
	[TestClass]
	public class ContactsControllerTest
	{
		[TestMethod]
		public void Post_Succeeds()
		{
			var loginRepo = new MockLoginRepository();
			var userRepo = new MockUserRepository();
			var contactRepo = new Mock<IContactRepository>();

			ContactsController controller = new ContactsController(contactRepo.Object, userRepo, loginRepo);

			Guid key = Guid.NewGuid();

			userRepo.CreateUser(new UserWriteDbo { CreatedAt = DateTime.UtcNow });
			loginRepo.CreateLogin(new LoginDbo { Id = key.ToByteArray(), UserId = 1 });

			IActionResult result = controller.Post(new NewContactRequest { Name = "A Name" }, Auth.Get(key));

			Assert.IsNotNull(result);
			Assert.IsTrue(result is OkResult);
		}

		[TestMethod]
		public void Get_NotLoggedIn_ReturnsUnauthorized()
		{
			var loginRepo = new MockLoginRepository();
			var userRepo = new MockUserRepository();
			var contactRepo = new Mock<IContactRepository>();
			
			ContactsController controller = new ContactsController(contactRepo.Object, userRepo, loginRepo);

			Guid key = Guid.NewGuid();

			IActionResult result = controller.Get(Auth.Get(key));

			UnauthorizedObjectResult unauth = result as UnauthorizedObjectResult;

			Assert.IsNotNull(unauth, "Result is not unauthorized");
			Assert.IsNotNull(unauth.Value);
			Assert.IsTrue(unauth.Value is ErrorResponse);
			Assert.IsTrue(!string.IsNullOrEmpty((unauth.Value as ErrorResponse).Message));
		}

		[TestMethod]
		public void Post_NotLoggedIn_ReturnsUnauthorized()
		{
			var loginRepo = new MockLoginRepository();
			var userRepo = new MockUserRepository();
			var contactRepo = new Mock<IContactRepository>();

			ContactsController controller = new ContactsController(contactRepo.Object, userRepo, loginRepo);

			Guid key = Guid.NewGuid();

			IActionResult result = controller.Post(new NewContactRequest { Name = "A Name" }, Auth.Get(key));

			UnauthorizedObjectResult unauth = result as UnauthorizedObjectResult;

			Assert.IsNotNull(unauth, "Result is not unauthorized");
			Assert.IsNotNull(unauth.Value);
			Assert.IsTrue(unauth.Value is ErrorResponse);
			Assert.IsTrue(!string.IsNullOrEmpty((unauth.Value as ErrorResponse).Message));
		}

		[TestMethod]
		public void Put_NotLoggedIn_ReturnsUnauthorized()
		{
			var loginRepo = new MockLoginRepository();
			var userRepo = new MockUserRepository();
			var contactRepo = new Mock<IContactRepository>();

			ContactsController controller = new ContactsController(contactRepo.Object, userRepo, loginRepo);

			Guid key = Guid.NewGuid();

			IActionResult result = controller.Put(new UpdateContactRequest { Name = "A Name", Id = 1 }, Auth.Get(key));

			UnauthorizedObjectResult unauth = result as UnauthorizedObjectResult;

			Assert.IsNotNull(unauth, "Result is not unauthorized");
			Assert.IsNotNull(unauth.Value);
			Assert.IsTrue(unauth.Value is ErrorResponse);
			Assert.IsTrue(!string.IsNullOrEmpty((unauth.Value as ErrorResponse).Message));
		}
	}
}
