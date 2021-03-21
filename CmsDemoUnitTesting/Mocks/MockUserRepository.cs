using CmsDemo.Data;
using CmsDemo.Dbo;
using System.Collections.Generic;
using System.Linq;

namespace CmsDemoUnitTesting.Mocks
{
	class MockUserRepository : IUserRepository
	{
		private List<UserReadDbo> _dbo = new List<UserReadDbo>();
		private int _pk;
		public void CreateUser(UserWriteDbo login)
		{
			_pk++;

			_dbo.Add(new UserReadDbo
			{
				CreatedAt = login.CreatedAt,
				Id = _pk,
				Nonce = login.Nonce,
				UserName = login.UserName,
				Password = login.Password
			});
		}

		public UserReadDbo GetUserById(int id)
		{
			return _dbo.SingleOrDefault(x => x.Id == id);
		}

		public UserReadDbo GetUserByUserName(string userName)
		{
			return _dbo.SingleOrDefault(x => x.UserName == userName);
		}

		public bool HasUser(string userName)
		{
			return _dbo.Any(x => x.UserName == userName);
		}
	}
}
