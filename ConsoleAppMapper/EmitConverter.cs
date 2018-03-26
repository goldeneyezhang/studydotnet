using EmitMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppMapper
{
    public class EmitConverter : IConvert
	{
		public EmitConverter()
		{
			_provider = ObjectMapperManager.DefaultInstance;//默认值
		}
		private ObjectMapperManager _provider;
		/// <summary>
		/// 转换功能提供者
		/// </summary>
		public ObjectMapperManager Provider
		{
			get { return _provider; }
			set { _provider = value; }
		}
		public T Convert<S,T>(S s)
		{
			ObjectsMapper<S, T> mapper = _provider.GetMapper<S, T>();
			return mapper.Map(s);
		}
		public bool TryConvert<S,T>(S s,ref T t)
		{
			try
			{
				ObjectsMapper<S, T> mapper = _provider.GetMapper<S, T>();
				t = mapper.Map(s,t);
			}
			catch { return false; }
			return true;
		}
    }
}
