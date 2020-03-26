using System;
using System.Collections.Generic;
using System.Reflection;
using PuppeteerSharp;

namespace AutomationTests.Pages
{
    public static class PageObjectFactory
    {
        private static readonly Dictionary<Type, ConstructorInfo> _constructors;

        static PageObjectFactory()
        {
            _constructors = new Dictionary<Type, ConstructorInfo>();
        }

        public static T CreatePage<T>(Page page) where T:PageObject
        {
            Console.WriteLine($"Creation of {typeof(T).Name} requested... ");

            var args = new List<object> { page };

            if (_constructors.ContainsKey(typeof(T)))
            {
                Console.WriteLine("Cached; returning from cache");

                var existing = _constructors[typeof(T)];
                return existing.Invoke(args.ToArray()) as T;
            }

            var ctor = GetCtor<T>();
            _constructors.Add(typeof(T), ctor);
            return ctor.Invoke(args.ToArray()) as T;
        }

        private static ConstructorInfo GetCtor<T>()
        {
            var type = typeof(T);

            var args = new List<Type>();
            args.Add(typeof(Page));

            // Get the Constructor which matches the given argument Types:
            var constructor = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null,
                CallingConventions.HasThis,
                args.ToArray(),
                new ParameterModifier[0]);

            return constructor;
        }
    }
}
