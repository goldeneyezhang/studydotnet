using Autofac;
using Autofac.Core.Lifetime;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppMapper
{
    public static class ContainerManager
    {
		private static IContainer _container;
		public static void SetContainer(IContainer container)
		{
			_container = container;
		}
		public static IContainer Container
		{
			get { return _container; }
		}
		public static T Resolve<T>(string key="",ILifetimeScope scope=null) where T:class
		{
			if(scope==null)
			{
				scope =  Scope();
			}
			if(string.IsNullOrEmpty(key))
			{
				return scope.Resolve<T>();
			}
			return scope.ResolveKeyed<T>(key);
		}
		public static object Resolve(Type type, ILifetimeScope scope =null)
		{
			if (scope == null)
			{
				scope = Scope();
			}
			return scope.Resolve(type);
		}
		public static ILifetimeScope Scope()
		{
			try
			{
				return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
			}
			catch(Exception)
			{
				return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
			}

		}
	}
}
