using Xunit;
using Xunit.Abstractions;

namespace Misc
{
    public class Properties
    {
        private readonly ITestOutputHelper testOutputHelper;

        public Properties(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private class SomeClassWithProperty
        {
            public int PropertyWithGetAndSet { get; set; }

            // this is automatically read-only can only be assigned a value here or inside constructor
            public int PropertyWithGetOnly { get; }

            // this can only be assigned within the class
            public int PropertyWithGetAndPrivateSet { get; private set; }

            // this is like read-only but behind the scene there is a set-method but can only be used inside constructor
            public int PropertyWithGetAndInit { get; init; }

            public void SomeMethod()
            {
                PropertyWithGetAndSet = 1; // this is good
                //PropertyWithGetOnly = 1; // this is error
                PropertyWithGetAndPrivateSet = 1; // this is good
                //PropertyWithGetAndInit = 1; // this is error
            }
        }

        [Fact]
        public void Properties_with_public_set_returns_nonNull_when_GetSetMethod_is_called()
        {
            var type = typeof(SomeClassWithProperty);
            var propertyWithGetAndSetPropertyInfo = type.GetProperty("PropertyWithGetAndSet");

            var setMethod = propertyWithGetAndSetPropertyInfo.GetSetMethod();

            Assert.NotNull(setMethod);
            Assert.True(setMethod.IsPublic);
        }

        [Fact]
        public void Property_with_no_setter_returns_null_when_GetSetMethod_is_called()
        {
            var type = typeof(SomeClassWithProperty);
            var propertyWithGetOnlyPropertyInfo = type.GetProperty("PropertyWithGetOnly");

            var setMethod = propertyWithGetOnlyPropertyInfo.GetSetMethod();
            var privateSetMethod = propertyWithGetOnlyPropertyInfo.GetSetMethod(true);

            Assert.Null(setMethod);
            Assert.Null(privateSetMethod);
        }

        [Fact]
        public void Property_with_private_setter_returns_null_when_GetSetMethod_is_called()
        {
            var type = typeof(SomeClassWithProperty);
            var propertyWithPrivateSetOnlyPropertyInfo = type.GetProperty("PropertyWithGetAndPrivateSet");

            var setMethod = propertyWithPrivateSetOnlyPropertyInfo.GetSetMethod();
            var privateSetMethod = propertyWithPrivateSetOnlyPropertyInfo.GetSetMethod(true);

            // there is no public setter
            Assert.Null(setMethod);
            // there is a private setter
            Assert.NotNull(privateSetMethod);
            Assert.False(privateSetMethod.IsPublic);
        }

        [Fact]
        public void Property_with_init_setter_returns_nonNull_when_GetSetMethod_is_called()
        {
            var type = typeof(SomeClassWithProperty);
            var propertyWithInitPropertyInfo = type.GetProperty("PropertyWithGetAndInit");

            var setMethod = propertyWithInitPropertyInfo.GetSetMethod();

            // there is a setter
            Assert.NotNull(setMethod);
            // and it is public - but it can only be used by constructor or inline initializer
            Assert.True(setMethod.IsPublic);
        }
    }
}
