using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Dependable.Lib
{
	public class DependableContainer
	{
		ConcurrentDictionary<Type, TypeInfo> _registeredFunc;
		ConcurrentDictionary<Type, object> _singletonInstances;
		TransientCreator transientCreator;
		public DependableContainer()
		{
			_registeredFunc = new ConcurrentDictionary<Type, TypeInfo>();
			_singletonInstances = new ConcurrentDictionary<Type, object>();
			transientCreator = new TransientCreator();
		}
		public void Register<TIF, TIN>(LifeCycle lifeCycle = LifeCycle.Transient)
		{
			var intf = typeof(TIF);
			var inst = typeof(TIN);
			if(_registeredFunc.ContainsKey(intf))
			{
				throw new ArgumentException($"ReRegister Not allowed {inst.FullName} Can only be registered once");
			}

			var constructors = inst.GetConstructors();
			if (constructors.Length != 1)
			{
				throw new ArgumentException($"ConstructorCount:Type {inst.FullName} does not have exactly one public constructor");
			}

			TypeInfo typeInfo = new TypeInfo();
			switch (lifeCycle)
			{
				case LifeCycle.Transient:
					{
						typeInfo.Func = transientCreator.makeCreationFunc(constructors, this);
						typeInfo.LifeCycle = LifeCycle.Transient;
						typeInfo.Type = inst;
						break;
					}
				case LifeCycle.Singleton:
					{
						typeInfo.Func = MakeSingletonFunc(intf, constructors); ; 
						typeInfo.LifeCycle = LifeCycle.Singleton;
						typeInfo.Type = inst;
						break;
					}
					throw new ArgumentException($"LifeCyle {lifeCycle.ToString()} not implemented");
			}
			_registeredFunc.GetOrAdd(intf, typeInfo);

		}

		private Func<object> MakeSingletonFunc(Type intf, System.Reflection.ConstructorInfo[] constructors)
		{
			return () =>
			{
				if (_singletonInstances.ContainsKey(intf))
				{
					return _singletonInstances[intf];
				}
				var inst = transientCreator.makeCreationFunc(constructors, this).Invoke();
				_singletonInstances.GetOrAdd(intf, inst);
				return inst;
			};
		}

		public object Resolve<T>()
		{
			Type t = typeof(T);
			return Resolve(t);
		}
		public object Resolve(Type t)
		{
			if (_registeredFunc.ContainsKey(t))
				return _registeredFunc[t].Func.Invoke();
			throw new Exception($"NotRegistered: Register<{t.FullName}>() must be called before calling resolve");
		}

		protected bool CanResolve(Type t)
		{
			return _registeredFunc.ContainsKey(t);
		}

		protected Type[] GetResolvableTypes()
		{
			return _registeredFunc.Keys.ToArray();
		}

		protected Type GetResolvedType(Type t)
		{
			return _registeredFunc[t].Type;
		}
		protected LifeCycle GetLifeCycle(Type t)
		{
			return _registeredFunc[t].LifeCycle;
		}

	}
}
