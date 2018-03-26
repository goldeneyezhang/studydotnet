using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnNetCore
{
    public static class RequestIPExtensions
    {
		public static IApplicationBuilder UseRequestIP(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<RequestIPMiddleware>();
		}
    }
}
