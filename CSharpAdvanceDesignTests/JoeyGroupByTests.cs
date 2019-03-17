using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyGroupByTests
    {
        [Test]
        public void groupBy_lastName()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Lee"},
                new Employee {FirstName = "Eric", LastName = "Chen"},
                new Employee {FirstName = "John", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Lee"},
            };

            var actual = JoeyGroupBy(employees);
            Assert.AreEqual(2, actual.Count());
            var firstGroup = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Eric", LastName = "Chen"},
                new Employee {FirstName = "John", LastName = "Chen"},
            };

            firstGroup.ToExpectedObject().ShouldMatch(actual.First().ToList());
        }

        private IEnumerable<IGrouping<string, Employee>> JoeyGroupBy(IEnumerable<Employee> employees)
        {
            var lookup = new Dictionary<string, List<Employee>>();
            var enumerator = employees.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var employee = enumerator.Current;
                if (lookup.ContainsKey(employee.LastName))
                {
                    lookup[employee.LastName].Add(employee);
                }
                else
                {
                    lookup.Add(employee.LastName, new List<Employee>() { employee });
                }
            }

            return ConvertMultiGrouping(lookup);
        }

        private IEnumerable<IGrouping<string, Employee>> ConvertMultiGrouping(Dictionary<string, List<Employee>> lookup)
        {
            var enumerator = lookup.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var keyValueCurrent = enumerator.Current;
                yield return new MyGrouping(keyValueCurrent.Key, keyValueCurrent.Value);
            }
        }
    }

    internal class MyGrouping : IGrouping<string, Employee>
    {
        private readonly string _key;
        private readonly List<Employee> _value;

        public MyGrouping(string key, List<Employee> value)
        {
            _key = key;
            _value = value;
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            return _value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string Key { get; }
    }
}