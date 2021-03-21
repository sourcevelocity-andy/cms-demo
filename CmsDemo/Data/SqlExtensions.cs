using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CmsDemo.Data
{
	public static class SqlExtensions
	{
		public static void Insert<T>(this SqlConnection connection, string table, T data)
		{
			SqlCommand command = new SqlCommand
			{
				Connection = connection
			};

			PropertyInfo[] info = typeof(T).GetProperties();

			List<string> columnNames = new List<string>();

			List<string> valueNames = new List<string>();

			foreach (PropertyInfo i in info)
			{
				columnNames.Add(i.Name);

				valueNames.Add($"@{i.Name}");

				object value = i.GetValue(data);

				if (value is null)
					value = DBNull.Value;

				command.Parameters.AddWithValue(i.Name, value);
			}

			command.CommandText = $"INSERT INTO [{table}] ({ string.Join(",", columnNames) }) VALUES ({ string.Join(",", valueNames) })";

			command.ExecuteNonQuery();
		}

		public static bool Update<T>(this SqlConnection connection, string table, T data, string column, object value)
		{
			SqlCommand command = new SqlCommand
			{
				Connection = connection
			};

			PropertyInfo[] info = typeof(T).GetProperties();

			StringBuilder builder = new StringBuilder("UPDATE [");
			builder.Append(table);
			builder.Append("] SET ");

			bool first = true;

			foreach (PropertyInfo i in info)
			{
				if (first)
					first = false;
				else
					builder.Append(", ");

				builder.Append(i.Name);
				builder.Append(" = @");
				builder.Append(i.Name);

				object v = i.GetValue(data);

				if (v is null)
					v = DBNull.Value;

				command.Parameters.AddWithValue(i.Name, v);
			}

			builder.Append(" WHERE ");

			builder.Append(column);

			builder.Append(" = @");

			builder.Append(column);

			command.Parameters.AddWithValue(column, value);

			command.CommandText = builder.ToString();

			int effect = command.ExecuteNonQuery();

			return effect != 0;
		}

		public static bool Delete(this SqlConnection connection, string table, object where)
		{
			SqlCommand command = new SqlCommand
			{
				Connection = connection
			};

			StringBuilder builder = new StringBuilder("DELETE FROM [");
			builder.Append(table);
			builder.Append("] WHERE ");

			bool first = true;

			if (where != null)
			{
				PropertyInfo[] info = where.GetType().GetProperties();

				foreach (PropertyInfo i in info)
				{
					if (first)
						first = false;
					else
						builder.Append(" AND ");

					builder.Append(i.Name);
					builder.Append(" = @");
					builder.Append(i.Name);

					object v = i.GetValue(where);

					if (v is null)
						v = DBNull.Value;

					command.Parameters.AddWithValue(i.Name, v);
				}
			}

			command.CommandText = builder.ToString();

			int effect = command.ExecuteNonQuery();

			return effect != 0;
		}
	}
}
