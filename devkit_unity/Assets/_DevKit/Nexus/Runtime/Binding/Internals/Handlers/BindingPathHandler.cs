using System;
using System.Collections.Generic;
using System.Reflection;
using DevKit.Core.Extensions;

namespace DevKit.Nexus.Binding.Internals.Handlers
{
    internal static class BindingPathHandler
    {
        private static readonly Dictionary<Type, PropertyInfo[]> ReflectionCache = new();

        internal static BindingPath GetBindingPath(object obj, string path)
        {
            obj.ThrowIfNull(nameof(obj));
            path.ThrowIfNullOrEmpty(nameof(path));

            var objType = obj.GetType();
            if (!ReflectionCache.TryGetValue(objType, out var props))
            {
                props = objType.GetProperties();
                ReflectionCache[objType] = props;
            }

            object source = null;
            PropertyInfo propertyInfo = null;

            if (!path.Contains('.'))
            {
                foreach (var prop in props)
                {
                    if (prop.Name != path)
                    {
                        continue;
                    }

                    source = obj;
                    propertyInfo = prop;
                    break;
                }
            }

            // TODO complete implementation
            // var pathParts = path.Split('.');
            // foreach (var prop in props)
            // {
            //
            // }

            source.ThrowIfNull(nameof(source));
            propertyInfo.ThrowIfNull(nameof(propertyInfo));

            var bindingPath = new BindingPath(source, propertyInfo);
            return bindingPath;
        }
    }
}
