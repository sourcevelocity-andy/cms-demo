using CmsDemo.Dbo;

namespace CmsDemo.Data
{
	public interface IUserRepository
	{
		bool HasUser(string userName);
		void CreateUser(UserWriteDbo login);
		UserReadDbo GetUserByUserName(string userName);

		UserReadDbo GetUserById(int id);
	}
}
