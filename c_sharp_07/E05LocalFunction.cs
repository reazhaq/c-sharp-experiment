using System;
using Xunit;
using Xunit.Abstractions;

namespace c_sharp_07
{
    public class E05LocalFunction
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E05LocalFunction(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private int CalculateFibonnacci(int number)
        {
            if (number <= 0) return 0;
            // here is the call to local function
            // which is like a embedded function generally organized
            // at the bottom of the method
            return LocalCalculateUsingRecursiveCall(number).current;

            ///////////////////////////////////////////////////////////////////////////
            // here is the local function
            // note the return type - it is a value tuple
            // and note how it is used to return from the main method
            (int current, int previous) LocalCalculateUsingRecursiveCall(int i)
            {
                if (i == 1) return (1, 0);
                // here comes the recursive call
                var (oldCurrent, oldPrevious) = LocalCalculateUsingRecursiveCall(i - 1);

                var newCurrent = oldCurrent + oldPrevious;
                var newPrevious = oldCurrent;
                return (newCurrent, newPrevious);
            }
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 5)]
        [InlineData(6, 8)]
        [InlineData(7, 13)]
        public void CalculateFibonnacci_for_i(int i, int expectedValue)
        {
            var fib = CalculateFibonnacci(i);

            testOutputHelper.WriteLine($"fibonnacci for {i}-th number = {fib}");
            Assert.Equal(expectedValue, fib);
        }

        private int ICanCalculateFibonnacci_UsingFunc_instead_of_local_function(int number)
        {
            if (number <= 0) return 0;

            //// trying to do it in one pass like we did for local function - doesn't work
            //// because of recursive call - OneLineFunkyFib is called within the body
            ///****
            //    Func<int, (int current, int previous)> OneLineFunkyFib = x =>
            //    {
            //        if (x == 0) return (1, 0);
            //        var (c, p) = OneLineFunkyFib(x - 1);
            //        return (c + p, c);
            //    };
            //****/

            // declaration needs to be on a separate line from the body
            Func<int, (int current, int previous)> FunkyFib = null;
            // actual body of the func - with recursive call
            FunkyFib = x =>
            {
                if (x == 1) return (1, 0);
                var (c, p) = FunkyFib(x - 1);
                return (c + p, c);
            };

            // unlike local function; this call has to go to the bottom
            return FunkyFib(number).current;
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 5)]
        [InlineData(6, 8)]
        [InlineData(7, 13)]
        public void CalculateFibonnacci_for_i_usingFuncStyleLocalFunction(int i, int expectedValue)
        {
            var fib = ICanCalculateFibonnacci_UsingFunc_instead_of_local_function(i);

            testOutputHelper.WriteLine($"fibonnacci for {i}-th number = {fib} using ICanCalculateFibonnacci_UsingFunc_instead_of_local_function");
            Assert.Equal(expectedValue, fib);
        }
    }
}
