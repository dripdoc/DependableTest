using System;
using System.Collections.Generic;
using System.Text;

namespace Dependable.Tests
{
	public interface IIntf1
	{
		string intf1String { get; }
	}
	public class ClassNoParm : IIntf1
	{
		public ClassNoParm()
		{
			intf1String = nameof(ClassNoParm);
		}
		public string intf1String { get; }
	}
	public interface IIntf2
	{
		IIntf1 IIntf1Inst { get; }
		int ParameterCount { get; }
	}
	public class ClassOneParm : IIntf2
	{
		public ClassOneParm(IIntf1 className)
		{
			IIntf1Inst = className;
			ParameterCount = 1;
		}
		public int ParameterCount { get; }
		public IIntf1 IIntf1Inst { get; }
	}

	public interface IIntf3
	{
		IIntf1 IIntf1Inst { get; }
		IIntf2 IIntf2Inst { get;  }
		int ParameterCount { get; }
	}
	public class ClassTwoParm : IIntf3
	{
		public ClassTwoParm(IIntf1 iintf3, IIntf2 iintf2)
		{
			IIntf2Inst = iintf2;
			IIntf1Inst = iintf3;
			ParameterCount = 1;
		}
		public int ParameterCount { get; }
		public IIntf1 IIntf1Inst { get; }
		public IIntf2 IIntf2Inst { get; }
	}
	public class ClassTwoConstructors : IIntf1
	{
		public ClassTwoConstructors()
		{ }
		public ClassTwoConstructors(string s)
		{ }
		public string intf1String => throw new NotImplementedException();
	}
	public class ClassWithNonPublicConstructor :IIntf1
	{
		protected ClassWithNonPublicConstructor()
		{ }
		public string intf1String => throw new NotImplementedException();
	}

}
