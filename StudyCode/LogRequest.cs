using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudyCode
{
	public class LogRequest
	{
		public string Message { get; set; }
		public string DetailStr { get; set; }
		public string AppDomainName { get; set; }
		public int ProcessID { get; set; }
		public string ProcessName { get; set; }
		public string IPAddress { get; set; }
		public int ThreadID { get; set; }
		public DateTime CreateTime { get; set; }
		public string MachineName { get; set; }
		public string[] Tags { get; set; }
		public Dictionary<string, string> TagDict { get; set; }
		public string Source { get; set; }
		public string SystemCode { get; set; }
		public LogRequest(string message, string detail, string source, string systemcode, string level)
		{
			Process cur = Process.GetCurrentProcess();
			this.Message = message;
			this.DetailStr = detail;
			this.AppDomainName = "App Domain Name ZZZ";
			this.ProcessID = cur.Id;
			this.ProcessName = cur.ProcessName.ToString();
			this.MachineName = Dns.GetHostName();
			var myIPs = Dns.GetHostEntryAsync(this.MachineName).Result.AddressList.Where(x => !x.IsIPv6LinkLocal).ToList();
			this.IPAddress = myIPs.FirstOrDefault().ToString();
			this.CreateTime = DateTime.Now;
			this.Source = source;
			this.SystemCode = systemcode;
			this.ThreadID = Environment.CurrentManagedThreadId;
			TagDict = new Dictionary<string, string>();
			TagDict.Add("LevelName", level);
		}
	}
}
