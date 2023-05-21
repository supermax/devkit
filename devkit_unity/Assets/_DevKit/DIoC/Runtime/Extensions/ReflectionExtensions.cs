using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using DevKit.Core.Extensions;
using DevKit.DIoC.Attributes;

namespace DevKit.DIoC.Extensions
{
    internal static class ReflectionExtensions
    {
        internal static ConstructorInfo GetDefaultConstructor([NotNull] this Type src)
        {
            ConstructorInfo defCtor = null;
            var constructors = src.GetConstructors();
            foreach (var ctor in constructors)
            {
                var att = ctor.GetCustomAttribute<DefaultAttribute>();
                if (att != null)
                {
                    return ctor;
                }

                var param = ctor.GetParameters();
                if (param.IsNullOrEmpty())
                {
                    defCtor = ctor;
                }
            }
            return defCtor;
        }

        internal static PropertyInfo[] GetInjectableProperties([NotNull] this Type src)
        {
            var injectProps = new List<PropertyInfo>();
            var props = src.GetProperties();
            foreach (var prop in props)
            {
                var att = prop.GetCustomAttribute<InjectAttribute>();
                if(att == null) continue;

                injectProps.Add(prop);
            }
            return injectProps.ToArray();
        }

        internal static MethodInfo[] GetExecutableMethods([NotNull] this Type src)
        {
            var exeMethods = new List<MethodInfo>();
            var methods = src.GetMethods();
            foreach (var method in methods)
            {
                var att = method.GetCustomAttribute<ExecuteAttribute>();
                if(att == null) continue;

                exeMethods.Add(method);
            }
            return exeMethods.ToArray();
        }
    }
}
