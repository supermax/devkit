using DevKit.Core.Extensions;
using DevKit.Core.Objects;
using DevKit.Nexus.Binding.API;
using DevKit.Nexus.Binding.Internals;
using DevKit.Nexus.Binding.Internals.Handlers;

namespace DevKit.Nexus.Binding
{
    public class BindingManager : Singleton<IBindingManager, BindingManager>, IBindingManager
    {
        public IBinding Bind(object source, string sourcePath, object target, string targetPath, BindingMode mode)
        {
            source.ThrowIfNull(nameof(source));
            sourcePath.ThrowIfNullOrEmpty(nameof(sourcePath));

            target.ThrowIfNull(nameof(target));
            targetPath.ThrowIfNullOrEmpty(nameof(targetPath));

            var sourceBindingPath = BindingPathHandler.GetBindingPath(source, sourcePath);
            var targetBindingPath = BindingPathHandler.GetBindingPath(target, targetPath);

            IBinding binding = sourceBindingPath is CollectionBindingPath && targetBindingPath is CollectionBindingPath
                    ? new CollectionBinding(source, sourcePath, target, targetPath, mode)
                    : new PropertyBinding(source, sourcePath, target, targetPath, mode);

            return binding
                   .SetSourceBindingPath(sourceBindingPath)
                   .SetTargetBindingPath(targetBindingPath)
                   .InitValues()
                   .Bind();
        }
    }
}
