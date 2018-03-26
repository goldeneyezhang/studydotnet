using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppMapper
{
    public interface IConvert
    {
		/// <summary>
		/// 转换方法
		/// </summary>
		/// <typeparam name="S"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="s"></param>
		/// <returns></returns>
		 T Convert<S, T>(S s);
		/// <summary>
		/// 尝试转换
		/// </summary>
		/// <typeparam name="S"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="s"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		bool TryConvert<S, T>(S s, ref T t);
    }
}
