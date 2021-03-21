using System;

namespace CmsDemo.Poco
{
	public class NewContactRequest
	{
		public string Name { get; set; }
		public string Birthdate { get; set; }
		public string Description { get; set; }
		public bool Favorite { get; set; }
		public int GroupId { get; set; }
	}
}
