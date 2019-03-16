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
        public void two_numbers_all_empty()
        {
            var first = new List<int> { };
            var second = new List<int> { };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        private bool JoeySequenceEqual(IEnumerable<int> first, IEnumerable<int> second)
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

                if (IsValidDifferent(firstEnumerator, secondEnumerator))
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

        private static bool IsValidDifferent(IEnumerator<int> firstEnumerator, IEnumerator<int> secondEnumerator)
        {
            return firstEnumerator.Current != secondEnumerator.Current;
        }

        private static bool IsEnd(bool firstFlag)
        {
            return !firstFlag;
        }
    }
}