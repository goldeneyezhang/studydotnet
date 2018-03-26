using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAutofac
{
	public class ProductService: IProduct
	{
		private ILog _log;
		public ProductService(ILog log)
		{
			this._log = log;
		}
		public void Run()
		{
			this._log.SaveLog("Product Log");
		}
	}
}
