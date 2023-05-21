using System.Diagnostics.CodeAnalysis;

namespace DevKit.DIoC.Container
{
    public interface ITypeMapReset<in T>
    {
        ITypeMapReset<T> From<TM>(string key = null) where TM : class, T;

        ITypeMapReset<T> From<TM>([NotNull] TM instance, string key = null) where TM : class, T;
    }
}
