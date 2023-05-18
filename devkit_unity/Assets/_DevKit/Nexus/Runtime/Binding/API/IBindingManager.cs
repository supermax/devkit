namespace DevKit.Nexus.Binding.API
{
    public interface IBindingManager
    {
        IBinding Bind(object source, string sourcePath, object target, string targetPath, BindingMode mode);
    }
}
