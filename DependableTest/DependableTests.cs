using System;
using Dependable.Lib;
using Xunit;

namespace Dependable.Tests{
    public class DependableTests
    {
		DependableContainer dependable;
		public DependableTests()
		{
			dependable = new DependableContainer();
		}

		 [Fact]
		public void ResolveUnregisteredTypeThrows()
		{
			var message = string.Empty;
			try
			{
				dependable.Resolve<ClassNoParm>();
			}
			catch (Exception e)
			{
				message = e.Message;
			}
			Assert.NotEmpty(message);
			Assert.Contains("NotRegistered", message);
		}

		[Fact]
		public void ClassTwoConstructorsThrows()
		{
			var message = string.Empty;
			try
			{
				dependable.Register<IIntf1,ClassTwoConstructors>();
			}
			catch (Exception e)
			{
				message = e.Message;
			}
			Assert.NotEmpty(message);
			Assert.Contains("ConstructorCount", message);
		}

		[Fact]
		public void ClassNonPublicThrows()
		{
			var message = string.Empty;
			try
			{
				dependable.Register<IIntf1, ClassWithNonPublicConstructor>();
			}
			catch (Exception e)
			{
				message = e.Message;
			}
			Assert.NotEmpty(message);
			Assert.Contains("ConstructorCount", message);
		}

		[Fact]
		public void ReRegisterTransientThrows()
		{
			string message = string.Empty;
			try
			{
				dependable.Register<IIntf1, ClassNoParm>(LifeCycle.Transient);
				dependable.Register<IIntf1, ClassNoParm>(LifeCycle.Transient);
			}
			catch(Exception e)
			{
				message = e.Message;
			}
			Assert.StartsWith("ReRegister", message);
		}

		[Fact]
		public void RegisterTransient()
		{
			dependable.Register<IIntf1, ClassNoParm>( LifeCycle.Transient);
			var instance1 = dependable.Resolve<IIntf1>() as ClassNoParm;
			Assert.NotNull(instance1);

			var instance2 = dependable.Resolve<IIntf1>() as ClassNoParm;
			Assert.NotNull(instance2);
			Assert.NotSame(instance1, instance2);

		}

		[Fact]
		public void RegisterTransient2Params()
		{
			dependable.Register<IIntf1, ClassNoParm>(LifeCycle.Transient);
			dependable.Register<IIntf2, ClassOneParm>(LifeCycle.Transient);
			dependable.Register<IIntf3, ClassTwoParm>(LifeCycle.Transient);

			var instance1 = dependable.Resolve<IIntf3>() as ClassTwoParm;
			Assert.NotNull(instance1);

			var instance2 = dependable.Resolve<IIntf3>() as ClassTwoParm;
			Assert.NotNull(instance2);
			Assert.NotSame(instance1, instance2);
			Assert.IsType< ClassNoParm>( instance1.IIntf1Inst);
			Assert.IsType< ClassOneParm>( instance1.IIntf2Inst);
			Assert.NotNull(instance1.IIntf1Inst);
			Assert.NotNull(instance1.IIntf2Inst);
		}

		[Fact]
		public void RegisterSingleton()
		{
			dependable.Register<IIntf1, ClassNoParm>(LifeCycle.Singleton);
			var instance1 = dependable.Resolve<IIntf1>() as ClassNoParm;
			Assert.NotNull(instance1);

			var instance2 = dependable.Resolve<IIntf1>() as ClassNoParm;
			Assert.NotNull(instance2);
			Assert.Same(instance1, instance2);
		}
	}
}
