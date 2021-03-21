using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace CmsDemo
{
	public class RepositoryBase
	{
		private IConfiguration _configuration;
		

		protected SqlConnection GetConnection()
		{
			SqlConnection ret = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

			ret.Open();

			return ret;
		}

		public IConfiguration Configuration { set => _configuration = value; }
	}
}
