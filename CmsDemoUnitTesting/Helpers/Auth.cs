using System;

namespace CmsDemoUnitTesting.Helpers
{
	public static class Auth
	{
		public static string Get(Guid key)
		{
			return "BASIC " + key.ToString();
		}
	}
}
