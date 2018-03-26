using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppMapper
{
	/// <summary>
	/// 构造函数
	/// </summary>
	public class AutoConverter : IConvert
	{
		/// <summary>
		/// 转换功能提供者
		/// </summary>
		private IMapper _provider;
		public AutoConverter(IMapper provider)
		{
			this._provider = provider;
		}
		public IMapper Provider { get { return _provider; } set { _provider = value; } }
		/// <summary>
		/// 转换方法
		/// </summary>
		/// <typeparam name="S"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="s"></param>
		/// <returns></returns>
		public T Convert<S, T>(S s)
		{
			return _provider.Map<S, T>(s);
		}
		/// <summary>
		/// 尝试转化
		/// </summary>
		/// <typeparam name="S"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="s"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		public bool TryConvert<S, T>(S s, ref T t)
		{
			try
			{
				t = _provider.Map<S, T>(s, t);
			}
			catch
			{
				return false;
			}
			return true;
		}
	}
}
