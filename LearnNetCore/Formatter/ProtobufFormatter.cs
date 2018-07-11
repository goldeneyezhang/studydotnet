using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using ProtoBuf;

namespace LearnNetCore.Formatter
{
    public class ProtobufFormatter:OutputFormatter
    {
		public string ContentType { get; private set; }
		public ProtobufFormatter()
		{
			ContentType = "application/proto";
			SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/proto"));
		}
		public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
		{
			if(context == null)
			{
				throw new ArgumentException(nameof(context));
			}
			var response = context.HttpContext.Response;
			Serializer.Serialize(response.Body, context.Object);
			return Task.FromResult(0);
		}
	}
}
