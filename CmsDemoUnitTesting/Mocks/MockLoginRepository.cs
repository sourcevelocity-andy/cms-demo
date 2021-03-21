using CmsDemo.Data;
using CmsDemo.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CmsDemoUnitTesting.Mocks
{
	class MockLoginRepository : ILoginRepository
	{
		private List<LoginDbo> _dbo = new List<LoginDbo>();

		public void CreateLogin(LoginDbo login)
		{
			_dbo.Add(new LoginDbo { Id = login.Id, CreatedAt = login.CreatedAt, UserId = login.UserId });
		}

		public LoginDbo GetLogin(Guid id)
		{
			byte[] bytes = id.ToByteArray();

			return _dbo.FirstOrDefault(x => x.Id.SequenceEqual(bytes));
		}
	}
}

