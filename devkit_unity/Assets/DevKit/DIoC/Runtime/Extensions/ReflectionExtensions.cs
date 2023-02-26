using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using DevKit.Core.Extensions;
using DevKit.DIoC.Attributes;

namespace DevKit.DIoC.Extensions
{
    public static class ReflectionExtensions
    {
        public static ConstructorInfo GetDefaultConstructor(this Type src)
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

        public static PropertyInfo[] GetInjectableProperties(this Type src)
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

        public static MethodInfo[] GetExecutableMethods(this Type src)
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

        public static string GetFullTypeName(this string cSharpCode)
        {
            const string pattern = @"^(?:(?:\s*//.*(?:\r?\n|\r))*\s*)*(?:using\s+[\w.]+\s*;\s*)*(?:namespace\s+(?<namespace>[\w.]+)\s*{\s*(?:public\s+(?:class|interface)\s+(?<classname>[\w]+))?)?";
            var match = Regex.Match(cSharpCode, pattern);
            var namespaceName = string.Empty;
            var className = string.Empty;
            if (match.Success)
            {
                namespaceName = match.Groups["namespace"].Value;
                className = match.Groups["classname"].Value;
            }
            var typeName = namespaceName.IsNullOrEmpty() ? className : $"{namespaceName}.{className}";
            return typeName;
        }
    }
}
