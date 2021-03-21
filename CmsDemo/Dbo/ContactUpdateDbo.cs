using System;

namespace CmsDemo.Dbo
{
	public class ContactUpdateDbo
	{
		public string Name { get; set; }
		public DateTime? Birthdate { get; set; }
		public string Description { get; set; }
		public bool Favorite { get; set; }
		public int GroupId { get; set; }
		
		public DateTime UpdatedAt { get; set; }
	}
}
