using System;

namespace CmsDemo.Dbo
{
	public class ContactCreateDbo : ContactUpdateDbo
	{
		public int UserId { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
