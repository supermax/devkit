using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevKit.Core.Extensions;

namespace DevKit.Nexus.Binding.Internals.Handlers
{
    internal static class BindingPathHandler
    {
        private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> ReflectionCache = new();

        internal static BindingPath GetBindingPath(object obj, string path)
        {
            obj.ThrowIfNull(nameof(obj));
            path.ThrowIfNullOrEmpty(nameof(path));

            object source = null;
            var objType = obj.GetType();
            PropertyInfo propertyInfo = null;

            const char pathSeparator = '.';
            // in case the path is simple
            if (!path.Contains(pathSeparator)
                || path.StartsWith(pathSeparator)
                || path.EndsWith(pathSeparator))
            {
                source = obj;
                propertyInfo = GetPropertyInfo(objType, path);
            }
            else // in case the path is complex and contains '.' char (e.g. `DataContext.Item.Name`)
            {
                var visitedSources = new List<object>();
                var pathParts = path.Split(pathSeparator);
                for (var i = 0; i < pathParts.Length; i++)
                {
                    var pathPart = pathParts[i];
                    propertyInfo = GetPropertyInfo(objType, pathPart);

                    if (i == pathParts.Length - 1)
                    {
                        break;
                    }
                    //objType = propertyInfo.DeclaringType;
                    source = propertyInfo.GetValue(obj);
                    obj = source;
                    objType = obj.GetType();

                    if (visitedSources.Contains(obj))
                    {
                        // TODO throw relevant exc related to possible recursion
                        break;
                    }
                    visitedSources.Add(obj);
                }
            }

            source.ThrowIfNull(nameof(source));
            propertyInfo.ThrowIfNull(nameof(propertyInfo));

            var bindingPath = new BindingPath(source, propertyInfo);
            return bindingPath;
        }

        private static PropertyInfo GetPropertyInfo(Type objType, string path)
        {
            if (!ReflectionCache.TryGetValue(objType, out var propsDic))
            {
                var props = objType.GetProperties();
                propsDic = props.ToDictionary(prop => prop.Name);
                ReflectionCache[objType] = propsDic;
            }
            propsDic.TryGetValue(path, out var propertyInfo);
            return propertyInfo;
        }
    }
}
