using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Dependable.Lib
{
	interface IDependencyCreator
	{
		Func<object> makeCreationFunc(ConstructorInfo[] constructors, DependableContainer container);
	}
}
