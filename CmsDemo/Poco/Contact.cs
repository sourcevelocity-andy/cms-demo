using System;

namespace CmsDemo.Poco
{
	public class Contact
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Birthdate { get; set; }
		public string PrettyBirthdate { get; set; }
		public string Description { get; set; }
		public bool Favorite { get; set; }
		public int GroupId { get; set; }
		public string CreatedAt { get; set; }
		public string UpdatedAt { get; set; }
	}
}
