using NSubstitute;
using System;
using System.Windows.Input;
using Xunit;

namespace XUnitTestProject1
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			//Create
			var calculator = Substitute.For<ICalculator>();
			//Set a return value
			calculator.Add(1, 2).Returns(3);
			Assert.Equal(3, calculator.Add(1, 2));

			//Check received calls:
			calculator.Received().Add(1, Arg.Any<int>());
			calculator.DidNotReceive().Add(2, 2);

			calculator.Mode.Returns("DEC");
			Assert.Equal("DEC", calculator.Mode);

			calculator.Mode = "HEX";
			Assert.Equal("HEX", calculator.Mode);

			calculator.Add(10, -5);
			calculator.Received().Add(10, Arg.Any<int>());
			calculator.Received().Add(10, Arg.Is<int>(x => x < 0));

			calculator.Add(Arg.Any<int>(), Arg.Any<int>())
				.Returns(x => (int)x[0] + (int)x[1]);
			Assert.Equal(15, calculator.Add(5, 10));

			calculator.Mode.Returns("HEX", "DEC", "BIN");
			Assert.Equal("HEX", calculator.Mode);
			Assert.Equal("DEC", calculator.Mode);
			Assert.Equal("BIN", calculator.Mode);
			//Raise events
			bool eventWasRaised = false;
			calculator.PoweringUp += (sender, args) => eventWasRaised = true;
			calculator.PoweringUp += Raise.Event();
			Assert.True(eventWasRaised);

			calculator.Bar(0, "").ReturnsForAnyArgs(x => "Hello " + x.Arg<string>());
			Assert.Equal("Hello World", calculator.Bar(1, "World"));
			//Received (or not) with specific arguments

		}
		[Fact]
		public void Shoule_execute_command_the_number_of_times_specified()
		{
			var command = Substitute.For<ICommand>();
			var repeater = new CommandRepeater(command, 3);
			//Act
			repeater.Execute();
			//Assert
			command.Received(3).Execute(1);
		}
		[Fact]
		public void ReceivedWithSpecifiedArguments()
		{
			//Create
			var calculator = Substitute.For<ICalculator>();
			calculator.Add(1, 2);
			calculator.Add(-100, 100);
			//check received with second arg of 2 and any first arg:
			calculator.Received().Add(Arg.Any<int>(), 2);
			//check received with first arg less than 0,and second arg of 100:
			calculator.Received().Add(Arg.Is<int>(x => x < 0), 100);
			//check did not receive a call where second arg is >=500 and any first arg:
			calculator.DidNotReceive().Add(Arg.Any<int>(), Arg.Is<int>(x => x >= 500));
		}
		//Often it is easiest to use a lambda for this,as shown in the following test
		[Fact]
		public void ShouldRaiseLowFuel_WithoutNSub()
		{
			var fuelManagement = new FuelManagement();
			var eventWasRaised = false;
			fuelManagement.LowFuelDetected += (o, e) => eventWasRaised = true;
			fuelManagement.DoSomething();
			Assert.True(eventWasRaised);
		}
		[Fact]
		public void ShouldRaiseLowFuel()
		{
			var fuelManagement = new FuelManagement();
			var handler = Substitute.For<EventHandler<LowFuelWarningEventArgs>>();
			fuelManagement.LowFuelDetected += handler;
			fuelManagement.DoSomething();
			handler.Received().Invoke(fuelManagement, Arg.Is<LowFuelWarningEventArgs>(x => x.PercentLeft < 20));
		}
	}
}
