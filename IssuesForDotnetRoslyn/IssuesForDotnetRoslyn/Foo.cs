using System;
using System.Diagnostics;

namespace IssuesForDotnetRoslyn
{
    public sealed class Foo<T>
    {
        private readonly Func<int, T>? _func1;
        private readonly Func<T>? _func2;

        public Foo(Func<int, T>? func)
        {
            _func1 = func ?? throw new ArgumentNullException(nameof(func));
        }

        public Foo(Func<T>? func)
        {
            _func2 = func ?? throw new ArgumentNullException(nameof(func));
        }

        public T Bar()
        {
            if (_func1 is not null)
            {
                return _func1(42);
            }

            Debug.Assert(_func2 is not null); // Should eliminate warning CS8602 Dereference of a possibly null reference
            return _func2(); // But actually warning is shown for .NET Standard 2.0
        }
    }
}
