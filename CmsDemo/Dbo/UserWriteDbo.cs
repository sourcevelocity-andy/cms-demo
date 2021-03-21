using System;

namespace CmsDemo.Dbo
{
	public class UserWriteDbo
	{
		public string UserName { get; set; }
		public byte[] Password { get; set; }
		public long Nonce { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
