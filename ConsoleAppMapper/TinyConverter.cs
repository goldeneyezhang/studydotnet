using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppMapper
{
	public class TinyConverter : IConvert
	{
		public T Convert<S, T>(S s)
		{
			TinyMapper.Bind<S, T>();
			return TinyMapper.Map<S, T>(s);
		}

		public bool TryConvert<S, T>(S s, ref T t)
		{
			try
			{
				TinyMapper.Bind<S, T>();
				t = TinyMapper.Map<S, T>(s, t);
			}
			catch { return false; }
			return true;
		}
	}
}
