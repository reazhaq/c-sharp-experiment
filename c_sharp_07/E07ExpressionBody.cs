using System;
using Xunit;
using Xunit.Abstractions;

namespace c_sharp_07
{
    /// <summary>
    /// expression body => means one liner - get rid of those curly braces
    /// </summary>
    public class E07ExpressionBody
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E07ExpressionBody(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private class SomeClass : IDisposable
        {
            private readonly ITestOutputHelper testOutputHelper;

            public SomeClass(string name, ITestOutputHelper testOutputHelper)
            {
                MyName = name;
                this.testOutputHelper = testOutputHelper;
            }

            // expression body call - very simple one line
            ~SomeClass() => InternalDispose(false);

            // property backed by private field
            private string myName;
            public string MyName
            {
                // expression body
                get => myName;
                // expression body with some validation
                set => myName = value ?? throw new ArgumentNullException(nameof(value));
            }

            // another one-liner
            public void Dispose() => InternalDispose(true);

            protected void InternalDispose(bool isDisposing)
            {
                if (isDisposing)
                {
                    testOutputHelper.WriteLine($"IDisposable.Dispose called");
                    GC.SuppressFinalize(this);
                }
                else
                {
                    testOutputHelper.WriteLine($"this call came from finalizer");
                }
            }
        }

        [Fact]
        public void Dispose_calls_internal_using_oneLine_expressionBody()
        {
            using (var someClass = new SomeClass("john doe", testOutputHelper))
            {
                testOutputHelper.WriteLine($"my name is: {someClass.MyName}");
                testOutputHelper.WriteLine($"variable someclass going out of scope - dispose shall be called - here");
            }
        }

        [Fact]
        public void Try_to_set_name_to_NULL_throws_exception()
        {
            var someClass = new SomeClass("john doe", testOutputHelper);
            var exception = Record.Exception(() => someClass.MyName = null);

            testOutputHelper.WriteLine($"exception.message: {exception.Message}");
            Assert.NotNull(exception);
        }
    }
}
