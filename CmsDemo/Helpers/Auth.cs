namespace CmsDemo.Helpers
{
	public static class Auth
	{
		public static string Get(string header)
		{
			if (header == null)
				return null;
			if (!header.StartsWith("BASIC "))
				return null;
			return header.Substring(6);
		}
	}
}
