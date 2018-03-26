using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationAutofac
{
    public class GuidAppServiceBase:IGuidAppService
    {
		private readonly Guid _items;
		public GuidAppServiceBase()
		{
			_items = Guid.NewGuid();
		}
		public Guid GuidItem()
		{
			return _items;
		}
    }
	public class GuidTransientAppService:GuidAppServiceBase,IGuidTransientAppService
	{

	}
	public class GuidScopedAppService:GuidAppServiceBase,IGuidScopedAppService
	{

	}
	public class GuidSingletonAppService:GuidAppServiceBase,IGuidSingletonAppService
	{

	}
}
