using Xunit;
using Xunit.Abstractions;

namespace c_sharp_07
{
    internal struct PersonStruct
    {
        public string Name { get; set; }
    }

    public class E09InParamWithoutOverload
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E09InParamWithoutOverload(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private void NameInParam(in PersonStruct p)
        {
            //p.Name = "blah"; // error: can't assign
            testOutputHelper.WriteLine($"p.Name = {p.Name}");
        }

        [Fact]
        public void Caller_need_not_use_in_param()
        {
            var p = new PersonStruct { Name = "jon" };
            NameInParam(p); // don't need to do "(in p)"
            testOutputHelper.WriteLine($"p.Name = {p.Name}");
        }

        [Fact]
        public void Caller_use_in_param_is_optional()
        {
            var p = new PersonStruct { Name = "jon" };
            NameInParam(in p); // resolves to same method
            testOutputHelper.WriteLine($"p.Name = {p.Name}");
        }
    }

    /// <summary>
    /// "in" can be used in some overload - here is some unexpected side effects
    /// 
    /// imagine one method that has "in" param like one above. Users of that method
    /// doesn't need to use "in" when calling, and use of "in" from caller side is
    /// optional.
    /// 
    /// now imagine - sometime later same method got overloaded without the "in" (like one below)
    /// this is legal - methods can't be overloaded only by "in" - "out" - "ref" keywords,
    /// but two same methods, one with and one without is allowed.
    /// 
    /// as soon as the overload got added - all the existing code that didn't use
    /// "in" - all of a sudden starts using new overloaded method. since this was "struct"
    /// a copy shall be made and a potential performance hit surprise may await
    /// </summary>

    public class E09InParamWithOverload
    {
        private readonly ITestOutputHelper testOutputHelper;

        public E09InParamWithOverload(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private void NameInParam(in PersonStruct p)
        {
            //p.Name = "blah"; // error: can't assign
            testOutputHelper.WriteLine($"p.Name = {p.Name}");
        }

        private void NameInParam(PersonStruct p)
        {
            testOutputHelper.WriteLine($"inside {nameof(NameInParam)} - p.Name = {p.Name}");
            p.Name = "some name"; // this is allowed since the param is not "in"
            testOutputHelper.WriteLine($"inside {nameof(NameInParam)} - p.Name = {p.Name}");
        }

        [Fact]
        public void Caller_without_in_param_uses_copy_version()
        {
            var p = new PersonStruct { Name = "jon" };
            NameInParam(p); // this goes to method without "in" - potential expensive copy
            testOutputHelper.WriteLine($"p.Name = {p.Name}");
        }

        [Fact]
        public void Caller_with_in_param_uses_in_version()
        {
            var p = new PersonStruct { Name = "jon" };
            NameInParam(in p); // this goes to method with "in" - potential performance gain
            testOutputHelper.WriteLine($"p.Name = {p.Name}");
        }
    }

    public class E09InParamWithClass
    {
        private readonly ITestOutputHelper testOutputHelper;

        private class PersonClass
        {
            public string Name { get; set; }
        }

        public E09InParamWithClass(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private void NameInParam(in PersonClass p)
        {
            // in parameter is not changing, but object's property
            // ref. by "p" is changing - this is allowed
            // that's why "in" parameter with ref type
            // doesn't make much diff
            p.Name = "blah";
        }

        //// this is no good
        //private void NameInParam2(in PersonClass p)
        //{
        //    // trying to change value of p - compiler error
        //    p = new PersonClass { Name = "new name" };
        //}

        [Fact]
        public void ChangeName()
        {
            var p = new PersonClass { Name = "name" };
            NameInParam(in p);

            testOutputHelper.WriteLine($"p.Name: {p.Name}");
            Assert.Equal("blah", p.Name);
        }
    }
}
