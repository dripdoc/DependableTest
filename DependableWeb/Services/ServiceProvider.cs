using ControllerDI.Services;
using Dependable.Lib;
using DependableWeb.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependableWebCore.Services
{
	public class DependableServiceProvider : DependableContainer
	{

		public object GetService(Type serviceType)
		{
			if (CanResolve(serviceType))
				return Resolve(serviceType);
			throw new Exception("ServiceNotRegistered");
		}
		public  void Build(IServiceCollection services)
		{
			var Types = GetResolvableTypes();
			foreach(var t in Types)
			{
				var instType = GetResolvedType(t);
				var LifeCycle = GetLifeCycle(t);
				if (LifeCycle == LifeCycle.Transient)
					services.AddTransient(t, instType);
				else if (LifeCycle == LifeCycle.Singleton)
					services.AddSingleton(t, instType);
			}
			
		}

	}
}
