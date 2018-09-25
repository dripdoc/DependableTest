using System;
using System.Collections.Generic;
using System.Text;
using Dependable.Lib;
using Xunit;

namespace Dependable.Tests
{
	public class TransientCreatorTest
	{
		DependableContainer dependable;
		TransientCreator transientCreator;
		public TransientCreatorTest()
		{
			dependable = new DependableContainer();
			transientCreator = new TransientCreator();
		}

		[Fact]
		public void TransientCreator_MakeCreator()
		{
			var f = transientCreator.makeCreationFunc(typeof(ClassNoParm).GetConstructors(), dependable);
			var instance = f();
			Assert.NotNull(instance);
			Assert.IsType<ClassNoParm>(instance);

			var instance2 = f();
			Assert.NotNull(instance);
			Assert.IsType<ClassNoParm>(instance);
			Assert.NotSame(instance, instance2);
		}

		[Fact]
		public void TransientCreator_MakeCreator_WithOneParameter_BeforeRegister()
		{
			string message = string.Empty;
			try
			{
				var f = transientCreator.makeCreationFunc(typeof(ClassOneParm).GetConstructors(), dependable);
			}
			catch (Exception e)
			{
				message = e.Message;
			}
			Assert.NotEmpty(message);
			Assert.StartsWith("NotRegistered", message);
		}

		[Fact]
		public void TransientCreator_MakeCreator_WithOneParameter()
		{
			dependable.Register<IIntf1, ClassNoParm>();
			var f = transientCreator.makeCreationFunc(typeof(ClassOneParm).GetConstructors(), dependable);
			var instance1 = f() as ClassOneParm;
			Assert.NotNull(instance1);
			Assert.NotNull(instance1.IIntf1Inst);

			var instance2 = f() as ClassOneParm;
			Assert.NotNull(instance2);
			Assert.NotNull(instance2.IIntf1Inst);
			Assert.NotSame(instance1, instance2);
		}
	}
}
