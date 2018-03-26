using log4net.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudyCode
{
	public static class HttpClientUtil
	{
		// http://www.infoq.com/cn/news/2016/09/HttpClient
		// HttpClient 必须是静态的
		private static readonly HttpClient HttpClient;

		static HttpClientUtil()
		{
			HttpClient = new HttpClient();

			HttpClient.DefaultRequestHeaders.Accept.Clear();
			HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			HttpClient.Timeout = TimeSpan.FromMilliseconds(30 * 1000);
		}
		public static async Task<T> PostAsJson<T, R>(string url, R request, int timeout = 10000, ILogger _logger = null)
		{
			return await RequestAsJsonAsync<T, R>(request, url, true, timeout, _logger);
		}

		public static async Task<T> GetAsJson<T>(string url, int timeout = 10000, ILogger _logger = null)
		{
			return await RequestAsJsonAsync<T, object>(null, url, false, timeout, _logger);
		}

		public static async Task<T> RequestAsJsonAsync<T, R>(R data, string url,
			bool isHttpPost = true, int timeout = 10000, ILogger _logger = null)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			string status = string.Empty;
			DateTime startTime = DateTime.Now;
			var cts = new CancellationTokenSource();
			if (timeout > 0)
			{
				cts.CancelAfter(timeout);
			}
			HttpResponseMessage response = null;

			try
			{
				if (isHttpPost)
				{
					response = await HttpClient.PostAsJsonAsync(url, data, cts.Token);
				}
				else
				{
					response = await HttpClient.GetAsync(url, cts.Token);
				}

				if (response.IsSuccessStatusCode)
				{
					status = "Success";
					return await response.Content.ReadAsJsonAsync<T>();
				}
				else
				{
					status = response.StatusCode.ToString();
				}
			}
			catch (TaskCanceledException ex)
			{
				status = ex.CancellationToken == cts.Token ? "TaskCanceledException-Token" : "TaskCanceledException-Other";
				//_logger?.LogError($"RequestAsJsonAsync, url: {url}, {status} {ex.ToString()}");
			}
			catch (Exception ex)
			{
				status = "Exception";
				var responseContent = await response?.Content?.ReadAsStringAsync();
				//_logger?.LogError($"RequestAsJsonAsync, url: {url}, Exception ==> {ex.ToString()}, request ==> {data.ToJSON()}, responseContent ==> {responseContent}");
			}
			finally
			{
				sw.Stop();
				//_logger?.LogInformation($"RequestAsJsonAsync, url: {url}, StatusCode: {status}, timecost: {sw.ElapsedMilliseconds}");
			}

			return default(T);
		}
		private static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data, CancellationToken token)
		{
			string dataAsString = data != null ? JsonConvert.SerializeObject(data) : string.Empty;
			Console.WriteLine(dataAsString);
			var content = new StringContent(dataAsString);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			return await httpClient.PostAsync(url, content, token);
		}

		private static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
		{
			var dataAsString = await content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(dataAsString);
		}
		public static string ToJSON(this object obj)
		{
			try
			{
				if (obj != null)
				{
					return JsonConvert.SerializeObject(obj);
				}
			}
			catch
			{
			}

			return string.Empty;
		}
	}
}
