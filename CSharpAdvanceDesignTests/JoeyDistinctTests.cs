using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyDistinctTests
    {
        [Test]
        public void distinct_numbers()
        {
            var numbers = new[] { 91, 3, 91, -1 };
            var actual = JoeyDistinct(numbers);

            var expected = new[] { 91, 3, -1 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void distinct_employees()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyDistinctWithEqualityComparer(employees);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
            };

            //            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyDistinctWithEqualityComparer(IEnumerable<Employee> employees)
        {
            var hashSet = new HashSet<Employee>();
            var enumerator = employees.GetEnumerator();
            var compare = new EmployyEqualityCompare();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
        }

        private IEnumerable<int> JoeyDistinct(IEnumerable<int> numbers)
        {
            return new HashSet<int>(numbers, EqualityComparer<int>.Default);
            //            var enumerator = numbers.GetEnumerator();
            //            while (enumerator.MoveNext())
            //            {
            //                var current = enumerator.Current;
            //                if (hashSet.Add(current))
            //                {
            //                    yield return current;
            //                }
            //            }
        }
    }

    internal class EmployyEqualityCompare : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.LastName == y.LastName && x.FirstName == y.FirstName;
        }

        public int GetHashCode(Employee obj)
        {
            //            return new Tuple<Employee>{obj.FirstName, obj.LastName}.GetHashCode();
            return obj.GetHashCode();
        }
    }
}