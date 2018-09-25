using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Dependable.Lib
{
	public class TransientCreator : IDependencyCreator
	{
		public Func<object> makeCreationFunc(ConstructorInfo[] constructors, DependableContainer container)
		{
			var constructor = constructors[0];

			var parameterInfos = constructor.GetParameters();
			var parameters = MakeParameters(parameterInfos, container);
			return () => constructor.Invoke(parameters);
		}
		private object[] MakeParameters(ParameterInfo[] parameterInfos, DependableContainer container)
		{
			var parameters = new object[parameterInfos.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				var pi = parameterInfos[i];
				parameters[i] = container.Resolve(pi.ParameterType);
			}
			return parameters;
		}
	}
}
