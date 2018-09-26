using Dependable.Lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dependable
{
    public struct TypeInfo
    {
		public Func<object> Func { get; set; }
		public LifeCycle LifeCycle { get; set; }
		public Type Type { get; set; }
    }
}
