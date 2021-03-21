using CmsDemo.Dbo;
using Dapper;
using System;


namespace CmsDemo.Data.Repositories
{
	public class LoginRepository : RepositoryBase, ILoginRepository
	{
		public void CreateLogin(LoginDbo login)
		{
			using (var connection = GetConnection())
			{
				connection.Insert("Login", login);
			}
		}

		public LoginDbo GetLogin(Guid id)
		{
			using (var connection = GetConnection())
			{
				return connection.QueryFirstOrDefault<LoginDbo>("SELECT * FROM Login WHERE Id = @Id", new { Id = id.ToByteArray() });
			}
		}
	}
}
