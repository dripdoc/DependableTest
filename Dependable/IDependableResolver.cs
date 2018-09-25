using System;
using System.Collections.Generic;
using System.Text;

namespace Dependable.Lib
{
	interface IDependableResolver
	{
		T Resolve<T>();
	}
}
