using CmsDemo.Dbo;
using System;

namespace CmsDemo.Data
{
	public interface ILoginRepository
	{
		void CreateLogin(LoginDbo login);
		LoginDbo GetLogin(Guid id);
	}
}
