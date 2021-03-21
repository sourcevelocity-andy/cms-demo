using CmsDemo.Dbo;
using Dapper;

namespace CmsDemo.Data.Repositories
{
	public class UserRepository : RepositoryBase, IUserRepository
	{
		public void CreateUser(UserWriteDbo login)
		{
			using (var connection = GetConnection())
			{
				connection.Insert("User", login);
			}
		}

		public UserReadDbo GetUserByUserName(string userName)
		{
			using (var connection = GetConnection())
			{
				return connection.QueryFirstOrDefault<UserReadDbo>("SELECT * FROM [User] WHERE UserName = @UserName", new { UserName = userName });
			}
		}

		public UserReadDbo GetUserById(int id)
		{
			using (var connection = GetConnection())
			{
				return connection.QueryFirstOrDefault<UserReadDbo>("SELECT * FROM [User] WHERE Id = @Id", new { Id = id });
			}
		}

		public bool HasUser(string userName)
		{
			using (var connection = GetConnection())
			{
				int count = connection.QueryFirst<int>("SELECT COUNT(*) FROM [User] WHERE UserName = @UserName", new { UserName = userName });
				return count > 0;
			}
		}
	}
}
