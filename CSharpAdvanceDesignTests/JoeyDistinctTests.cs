﻿using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
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
            var actual = JoeyDistinctWithEqualityComparer(numbers, EqualityComparer<int>.Default);

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

            var actual = JoeyDistinctWithEqualityComparer(employees, new EmployeeEqualityComparer());

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TSource> JoeyDistinctWithEqualityComparer<TSource>(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            var hashSet = new HashSet<TSource>(comparer);
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
        }

        private IEnumerable<TSource> JoeyDistinct<TSource>(IEnumerable<TSource> source, EqualityComparer<TSource> equalityComparer)
        {
            return new HashSet<TSource>(source, equalityComparer);
        }
    }

    internal class EmployeeEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.LastName == y.LastName && x.FirstName == y.FirstName;
        }

        public int GetHashCode(Employee obj)
        {
            return Tuple.Create(obj.FirstName, obj.FirstName).GetHashCode();
        }
    }
}