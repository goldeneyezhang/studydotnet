using LearnNetCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnNetCore.Controllers
{
	[SessionLog]
	public class OperationsController : Controller
	{
		private readonly OperationService _operationService;
		private readonly IOperationTransient _transientOperation;
		private readonly IOperationScoped _scopedOperation;
		private readonly IOperationSingleton _singletonOperation;
		private readonly IOperationSingletonInstance _singletonInstanceOperation;
		private readonly IRequestEntry _requestEntryMethod;
		private readonly IMemoryCache _memoryCache;
		public OperationsController(OperationService operationService, IOperationTransient transientOperation, IOperationScoped scopedOperation, IOperationSingleton singletonOperation, IOperationSingletonInstance singletonInstanceOperation, IRequestEntry requestEntryMethod,IMemoryCache memoryCache)
		{
			this._operationService = operationService;
			this._transientOperation = transientOperation;
			this._scopedOperation = scopedOperation;
			this._singletonOperation = singletonOperation;
			this._singletonInstanceOperation = singletonInstanceOperation;
			this._requestEntryMethod = requestEntryMethod;
			this._memoryCache = memoryCache;
		}
		public IActionResult Index()
		{
			ViewBag.Transient = _transientOperation;
			ViewBag.Scoped = _scopedOperation;
			ViewBag.Singleton = _singletonOperation;
			ViewBag.SingletonInstance = _singletonInstanceOperation;
			//operation service has its own requested services
			ViewBag.Service = _operationService;
			return View();
		}
		public IActionResult Session1()
		{
			var collection = _requestEntryMethod.GetOrCreateEntries(ControllerContext.HttpContext);
			ViewBag.Session = collection;
			string cacheKey = "key";
			string result;
			if (!_memoryCache.TryGetValue(cacheKey, out result))
			{
				result = $"Yibo:{DateTime.Now}";
				MemoryCacheEntryOptions memoryCacheOptions = new MemoryCacheEntryOptions();
				memoryCacheOptions.SlidingExpiration = TimeSpan.FromSeconds(5);
				_memoryCache.Set(cacheKey, result, memoryCacheOptions);
			}
			ViewBag.Cache = result;
			return View();
		}
	}
}
