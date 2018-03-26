using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore
{
    public class RequestEntry
    {
		public string Path { get; set; }
		public int Count { get; set; }
    }
	public class RequestEntryCollection
	{
		public List<RequestEntry> Entries { get; set; } = new List<RequestEntry>();
		public void RecordRequest(string requestPath)
		{
			var existingEntry = Entries.FirstOrDefault(e => e.Path == requestPath);
			if(existingEntry != null)
			{
				existingEntry.Count++;
				return;
			}
			var newEntry = new RequestEntry()
			{
				Path = requestPath,
				Count = 1
			};
			Entries.Add(newEntry);
		}
		public int TotalCount()
		{
			return Entries.Sum(e => e.Count);
		}
	}
	public interface IRequestEntry
	{
		RequestEntryCollection GetOrCreateEntries(HttpContext context);
		void SaveEntries(HttpContext context, RequestEntryCollection collection);
	}
	public class RequestEntryCollectionMethod:IRequestEntry
	{
		public RequestEntryCollection GetOrCreateEntries(HttpContext context)
		{
			RequestEntryCollection collection = null;
			byte[] requestEntriesBytes;
			context.Session.TryGetValue("RequestEntries", out requestEntriesBytes);
			if(requestEntriesBytes != null && requestEntriesBytes.Length > 0)
			{
				string json = System.Text.Encoding.UTF8.GetString(requestEntriesBytes);
				return JsonConvert.DeserializeObject<RequestEntryCollection>(json);
			}
			if(collection == null)
			{
				collection = new RequestEntryCollection();
			}
			return collection;
		}
		public void SaveEntries(HttpContext context, RequestEntryCollection collection)
		{
			string json = JsonConvert.SerializeObject(collection);
			byte[] serializedResult = System.Text.Encoding.UTF8.GetBytes(json);

			context.Session.Set("RequestEntries", serializedResult);
		}
	}
}
