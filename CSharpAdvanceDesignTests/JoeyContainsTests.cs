using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyContainsTests
    {
        [Test]
        public void contains_joey_chen()
        {
            var employees = new List<Employee>
            {
                new Employee(){FirstName = "Joey", LastName = "Wang"},
                new Employee(){FirstName = "Tom", LastName = "Li"},
                new Employee(){FirstName = "Joey", LastName = "Chen"},
            };

            var joey = new Employee() { FirstName = "Joey", LastName = "Chen" };

            var actual = JoeyContains(employees, joey);

            Assert.IsTrue(actual);
        }

        [Test]
        public void not_contains_joey1_chen()
        {
            var employees = new List<Employee>
            {
                new Employee(){FirstName = "Joey", LastName = "Wang"},
                new Employee(){FirstName = "Tom", LastName = "Li"},
                new Employee(){FirstName = "Joey", LastName = "Chen"},
            };

            var joey = new Employee() { FirstName = "Joey1", LastName = "Chen" };

            var actual = JoeyContains(employees, joey);

            Assert.IsFalse(actual);
        }

        private bool JoeyContains(IEnumerable<Employee> employees, Employee value)
        {
            var employeeEnumerator = employees.GetEnumerator();
            while (employeeEnumerator.MoveNext())
            {
                var employee = employeeEnumerator.Current;
                if (employee.FirstName == value.FirstName && employee.LastName == value.LastName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}