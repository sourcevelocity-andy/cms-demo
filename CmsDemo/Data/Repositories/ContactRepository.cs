using CmsDemo.Dbo;
using Dapper;
using System.Collections.Generic;

namespace CmsDemo.Data.Repositories
{
	public class ContactRepository : RepositoryBase, IContactRepository
	{
		public IEnumerable<ContactReadDbo> GetContacts(int userId)
		{
			using (var connection = GetConnection())
			{
				return connection.Query<ContactReadDbo>("SELECT * FROM Contact WHERE UserId = @UserId ORDER BY Favorite desc, Name", new { UserId = userId });
			}
		}

		public void CreateContact(ContactCreateDbo contact)
		{
			using (var connection = GetConnection())
			{
				connection.Insert("Contact", contact);
			}
		}

		public bool UpdateContact(ContactUpdateDbo contact, int id)
		{
			using (var connection = GetConnection())
			{
				return connection.Update("Contact", contact, "Id", id);
			}
		}

		public bool DeleteContact(int contactId, int userId)
		{
			using (var connection = GetConnection())
			{
				return connection.Delete("Contact", new { Id = contactId, UserId = userId });
			}
		}
	}
}
