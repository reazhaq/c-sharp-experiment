using Xunit;
using Xunit.Abstractions;

namespace c_sharp_08
{
    public class E02DefaultInterfaceMethod
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E02DefaultInterfaceMethod(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private interface ISomeInterface
        {
            void Method1(ITestOutputHelper testOutputHelper);

            // add new capability to the interface and not break existing implementations
            void Method2(ITestOutputHelper testOutputHelper)
            {
                testOutputHelper.WriteLine($"{nameof(ISomeInterface)} - {nameof(Method2)}");
            }

            //// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/default-interface-methods-versions
            // now you can have static member too - but this can cause problems
            public static string Something = "something";
            // also static methods are allowed - but this can also cause problems
            public static void SomeStaticMethod(string newSomething) => Something = newSomething;
        }

        private class SomeClass1 : ISomeInterface
        {
            public void Method1(ITestOutputHelper testOutputHelper)
            {
                testOutputHelper.WriteLine($"{nameof(SomeClass1)} - {nameof(Method1)}");
            }
        }

        [Fact]
        public void InterfaceImpl1()
        {
            var sut = new SomeClass1();

            sut.Method1(testOutputHelper);

            //// doesn't work cause sut don't know about method 2
            //sut.Method2(testOutputHelper);

            // but this one works
            (sut as ISomeInterface).Method2(testOutputHelper);
        }
    }
}
