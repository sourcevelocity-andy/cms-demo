using CmsDemo.Poco;
using Microsoft.AspNetCore.Mvc;

namespace CmsDemo.Helpers
{
	public static class Error
	{
		public static IActionResult BadRequest(string message)
		{
			return new BadRequestObjectResult(new ErrorResponse { Message = message });
		}
	}
}
