using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CmsDemo.Helpers
{
	public static class Hash
	{
		public static byte[] Get(string value, long nonce)
		{
			MemoryStream ms = new MemoryStream();
			ms.Write(Encoding.UTF8.GetBytes(value));
			ms.Write(BitConverter.GetBytes(nonce));
			SHA256 sha = SHA256.Create();
			return sha.ComputeHash(ms.ToArray());
		}

		public static long RandomLong()
		{
			Random random = new Random();
			
			long result = random.Next();

			return (result << 32) | (long)random.Next();
		}
	}
}
