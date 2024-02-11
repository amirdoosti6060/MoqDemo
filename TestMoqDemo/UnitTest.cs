using Moq;
using MoqDemo;

namespace TestMoqDemo
{
    public class UnitTest
    {
        private Mock<ILogger> moqLogger;

        /*
         * In the constructor, I instantiate moqLogger and I mocked ILogger
         * then I set up all methods and properties of this interface
         */
        public UnitTest() 
        {
            moqLogger = new Mock<ILogger>();
            moqLogger.Setup(x => x.Log(It.IsAny<string>()));
            moqLogger.Setup(x => x.LogAsync(It.IsAny<string>()));
            moqLogger.SetupProperty(x => x.IsVerbose);
        }

        /*
         * In this example I showed how we can use a mocked interface and pass it to another class
         */
        [Theory]
        [InlineData(10, 2, 12)]
        public void Calculator_Add_Ok(int a, int b, int c)
        {
            //Arrange
            var calc = new Calculator(moqLogger.Object);

            //Act
            int d = calc.Add(a, b);

            //Assert
            Assert.Equal(c, d);
        }

        /*
         * In this test I showed that how to mock a property and then check it really assigned
         */
        [Theory]
        [InlineData(10, 2, 12)]
        public void Calculator_Add_WithVerbos_Ok(int a, int b, int c)
        {
            //Arrange
            moqLogger.Object.IsVerbose = true;
            var calc = new Calculator(moqLogger.Object);

            //Act
            int d = calc.Add(a, b);

            //Assert
            Assert.True(moqLogger.Object.IsVerbose);
            Assert.Equal(c, d);
        }

        /*
         * This test check that the Log method of ILogger is called al least once or not
         * and as we see the test doesn't pass because we commented the Log call in Subtract method
         */
        [Fact]
        public void Calculator_Subtract_HasLog_Ok()
        {
            //Arrange
            var calc = new Calculator(moqLogger.Object);

            //Act
            calc.Subtract(10, 20);

            //Assert
            moqLogger.Verify(foo => foo.Log(It.IsAny<string>()), Times.AtLeastOnce);
        }

        /*
         * In this test I showed that how we can mock an async method
         */
        [Theory]
        [InlineData(10, 2, 20)]
        public async void Calculator_Multiply_Ok(int a, int b, int c)
        {
            //Arrange
            var calc = new Calculator(moqLogger.Object);

            //Act
            int d = await calc.Multiply(a, b);

            //Assert
            Assert.Equal(c, d);
        }

        /* 
         * In this test I mocked a concrete class (Calculator)
         * Then I mocked an event in this class (OnError)
         * and finally we called Devide with parameters to produce "Devide By Zero" error
         * and you can see when we checked that OnError is called at least once, it is verified
         * while we didn't defined an error handler directly for moqCalc.Object
         */
        [Theory]
        [InlineData(10, 0, 0)]
        public void Calculator_Devide_By_Zero_Event(int a, int b, int c)
        {
            //Arrange
            var moqCalc = new Mock<Calculator>(moqLogger.Object);
            moqCalc.SetupAdd(x => x.OnError += It.IsAny<EventHandler>());
            moqCalc.Object.OnError += (sender, args) => Console.WriteLine("Division by zero error!");

            //Act
            int d = moqCalc.Object.Devide(a, b);

            //Assert
            moqCalc.VerifyAdd(x => x.OnError += It.IsAny<EventHandler>(), Times.Once);
            Assert.Equal(c, d);
        }

        /*
         * In this test I showed how can defined a callback
         * Every time that Log() method is called, the call variable increases by one
         * Then I called Add and Devide. Each on these methods will call Log() once
         * so we called Log() twice. At the end I checked the number of calls
         */
        [Theory]
        [InlineData(10, 2, 2)]
        public void Calculator_Callback_check(int a, int b, int numCalls)
        {
            //Arrange
            int call = 0;
            moqLogger.Setup(x => x.Log(It.IsAny<string>()))
                .Callback(() => call++);

            var calc = new Calculator(moqLogger.Object);

            //Act
            calc.Add(a, b);
            calc.Devide(a, b);

            //Assert
            Assert.Equal<int>(call, numCalls);
        }
    }
}