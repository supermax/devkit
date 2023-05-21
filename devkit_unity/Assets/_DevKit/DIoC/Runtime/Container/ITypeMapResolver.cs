using System.Diagnostics.CodeAnalysis;

namespace DevKit.DIoC.Container
{
    public interface ITypeMapResolver<T> where T : class
    {
        T Instance(string key = null, params object[] args);

        T Inject([NotNull]T instance, params object[] args);
    }
}
