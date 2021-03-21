using CmsDemo.Dbo;
using System.Collections.Generic;

namespace CmsDemo.Data
{
	public interface IContactRepository
	{
		IEnumerable<ContactReadDbo> GetContacts(int userId);
		bool UpdateContact(ContactUpdateDbo contact, int id);
		void CreateContact(ContactCreateDbo contact);
		bool DeleteContact(int contactId, int userId);
	}
}
