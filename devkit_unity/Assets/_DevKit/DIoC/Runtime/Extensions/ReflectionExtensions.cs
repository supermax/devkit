using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using DevKit.Core.Extensions;
using DevKit.DIoC.Attributes;

namespace DevKit.DIoC.Extensions
{
    internal static class ReflectionExtensions
    {
        internal static ConstructorInfo GetDefaultConstructor([NotNull] this Type src)
        {
            var constructors = src.GetConstructors();
            constructors.ThrowIfNullOrEmpty(nameof(constructors));

            var defCtor = (from ctor in constructors
                                        let att = ctor.GetCustomAttribute<DefaultAttribute>()
                                        where att != null select ctor)
                                        .FirstOrDefault();
            if (defCtor != null)
            {
                return defCtor;
            }

            foreach (var ctor in constructors)
            {
                var param = ctor.GetParameters();
                if (!param.IsNullOrEmpty())
                {
                    continue;
                }

                defCtor = ctor;
                break;
            }
            if (defCtor != null)
            {
                return defCtor;
            }

            defCtor = constructors[0];
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
