﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAutofac
{
	public interface ILog:ISingleton
	{
		void SaveLog(string message);
	}
}