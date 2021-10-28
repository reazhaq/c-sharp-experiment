using System;
using Xunit;
using Xunit.Abstractions;

namespace c_sharp_07
{
    public class E6RefReturn
    {
        private readonly ITestOutputHelper testOutputHelper;
        private int[] someNumbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public E6RefReturn(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ToUpdate_an_element_of_an_array_find_index_first()
        {
            var valueToLookFor = 3;

            // before: get the index and then 
            var indexOf3 = FindElementWithValueAndReturnIndex(valueToLookFor, someNumbers);
            Assert.Equal(2, indexOf3);
            Assert.Equal(valueToLookFor, someNumbers[indexOf3]);

            // change the value
            someNumbers[indexOf3] = 99;
            // now search for the same value throws exception;
            var exception = Record.Exception(() => FindElementWithValueAndReturnIndex(valueToLookFor, someNumbers));
            Assert.Equal($"{valueToLookFor} not found", exception.Message);
        }

        private int FindElementWithValueAndReturnIndex(int numberToFind, int[] sourceWhereToLook)
        {
            for (int i = 0; i < sourceWhereToLook.Length; i++)
                if (numberToFind == sourceWhereToLook[i])
                    return i;

            throw new Exception($"{numberToFind} not found");
        }

        [Fact]
        public void ToUpdate_an_element_of_an_array_use_ref_return_that_returns_a_direct_reference_to_the_element()
        {
            var valueToLookFor = 4;

            var indexOf4 = FindElementWithValueAndReturnIndex(valueToLookFor, someNumbers);
            ref var elementWithValue4 = ref FindElementWithValueAndReturnRef(valueToLookFor, someNumbers);

            // i can access the element by index
            Assert.Equal(valueToLookFor, someNumbers[indexOf4]);
            // i also have direct access
            Assert.Equal(valueToLookFor, elementWithValue4);
            // update the element value directly - no need to access by index
            elementWithValue4 = 99;
            // validate correct element got updated
            Assert.Equal(99, someNumbers[indexOf4]);
            // now search for the same value throws exception;
            var exception = Record.Exception(() => FindElementWithValueAndReturnIndex(valueToLookFor, someNumbers));
            Assert.Equal($"{valueToLookFor} not found", exception.Message);
        }

        // note "ref" in return signature - and ref in the return statement
        private ref int FindElementWithValueAndReturnRef(int numberToFind, int[] sourceWhereToLook)
        {
            for (int i = 0; i < sourceWhereToLook.Length; i++)
                if (numberToFind == sourceWhereToLook[i])
                    return ref sourceWhereToLook[i];

            throw new Exception($"{numberToFind} not found");
        }
    }
}
