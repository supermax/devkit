using System.Reflection;
using DevKit.Core.Extensions;

namespace DevKit.Nexus.Binding.Internals.Handlers
{
    internal static class BindingPathHandler
    {
        internal static BindingPath GetBindingPath(object obj, string path)
        {
            obj.ThrowIfNull(nameof(obj));
            path.ThrowIfNullOrEmpty(nameof(path));

            var objType = obj.GetType();
            var props = objType.GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty);

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
