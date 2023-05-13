namespace DevKit.Nexus.Binding.API
{
    public interface IBindingManager
    {
        Binding Bind(object source, string sourcePath, object target, string targetPath, BindingMode mode);
    }
}
