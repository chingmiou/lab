using Lab.Entities;
using System;
using System.Collections.Generic;

namespace Lab
{
    public static class MyOwnLinq
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> predicate)
        {
            foreach (var source in sources)
            {
                if (predicate(source))
                    yield return source;
            }
        }

        public static IEnumerable<TSource> JoeyWhere<TSource>(IEnumerable<TSource> sources, Func<TSource, int, bool> predicate)
        {
            var index = 0;
            foreach (var source in sources)
            {
                if (predicate(source, index))
                {
                    yield return source;
                }

                index++;
            }
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> sources, Func<TSource, TResult> selector)
        {
            foreach (var source in sources)
            {
                yield return selector(source);
            }
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> sources, Func<TSource, int, TResult> selector)
        {
            var index = 0;
            foreach (var source in sources)
            {
                yield return selector(source, index);
                index++;
            }
        }

        public static IEnumerable<Employee> JoeyTake(this IEnumerable<Employee> employees, int takeCount)
        {
            var employeeEnumerator = employees.GetEnumerator();
            var index = 0;
            while (employeeEnumerator.MoveNext())
            {
                var employee = employeeEnumerator.Current;
                if (index < takeCount)
                {
                    yield return employee;
                }
                else
                {
                    yield break;
                }
                index++;
            }
        }

        public static TSource JoeyFirstOrDefault<TSource>(this IEnumerable<TSource> employees)
        {
            var employeeEnumerator = employees.GetEnumerator();
            if (employeeEnumerator.MoveNext())
                return employeeEnumerator.Current;
            else
                return default(TSource);
            ;
        }

        public static TSource JoeyLastOrDefault<TSource>(this IEnumerable<TSource> employees)
        {
            var employeeEnumerator = employees.GetEnumerator();
            TSource last = default(TSource);
            while (employeeEnumerator.MoveNext())
            {
                var employee = employeeEnumerator.Current;
                last = employee;
            }

            return last;
        }

        public static IEnumerable<Employee> JoeyReverse(this IEnumerable<Employee> employees)
        {
            return new Stack<Employee>(employees);
            //            var stack = new Stack<Employee>(employees);
            //            var enumerator = stack.GetEnumerator();
            //            while (enumerator.MoveNext())
            //            {
            //                yield return enumerator.Current;
            //            }
            //            var employeeEnumerator = employees.GetEnumerator();
            //            var reverse = new Stack<Employee>();
            //
            //            while (employeeEnumerator.MoveNext())
            //            {
            //                var employee = employeeEnumerator.Current;
            //                reverse.Push(employee);
            //            }
            //
            //            return reverse;
        }
    }
}