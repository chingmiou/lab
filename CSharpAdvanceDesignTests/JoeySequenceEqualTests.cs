using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySequenceEqualTests
    {
        [Test]
        public void compare_two_numbers_equal()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_different()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 1, 2, 3 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_first_length_more_then_second()
        {
            var first = new List<int> { 3, 2 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_first_length_short()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_second_length_short()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_contains_0()
        {
            var first = new List<int> { 3, 2, 0 };
            var second = new List<int> { 3, 2 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        //        [Test]
        //        public void two_empty_numbers()
        //        {
        //            var first = new List<int> { };
        //            var second = new List<int> { };
        //
        //            var actual = JoeySequenceEqual(first, second);
        //
        //            Assert.IsTrue(actual);
        //        }

        [Test]
        [Ignore("not yet")]
        public void two_employees_sequence_equal()
        {
            var first = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen", Phone = "123"},
                new Employee() {FirstName = "Tom", LastName = "Li", Phone = "456"},
                new Employee() {FirstName = "David", LastName = "Wang", Phone = "789"},
            };

            var second = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen", Phone = "123"},
                new Employee() {FirstName = "Tom", LastName = "Li", Phone = "123"},
                new Employee() {FirstName = "David", LastName = "Wang", Phone = "123"},
            };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        private bool JoeySequenceEqual<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();

            while (true)
            {
                var firstFlag = firstEnumerator.MoveNext();
                var secondFlag = secondEnumerator.MoveNext();
                if (IsLengthDifferent(firstFlag, secondFlag))
                {
                    return false;
                }

                if (IsEnd(firstFlag))
                {
                    return true;
                }

                var comparer = new JoeyEqualityComparerName();

                if (!firstEnumerator.Current.Equals(secondEnumerator.Current))
                {
                    return false;
                }
            }

            //            while (firstEnumerator.MoveNext())
            //            {
            //                if (secondEnumerator.MoveNext())
            //                {
            //                    if (firstEnumerator.Current != secondEnumerator.Current)
            //                    {
            //                        return false;
            //                    }
            //                }
            //                else
            //                {
            //                    return false;
            //                }
            //            }
            //
            //            if (secondEnumerator.MoveNext())
            //            {
            //                return false;
            //            }
            //
            //            return true;
        }

        private static bool IsLengthDifferent(bool firstFlag, bool secondFlag)
        {
            return firstFlag != secondFlag;
        }

        private static bool IsEnd(bool firstFlag)
        {
            return !firstFlag;
        }
    }
}