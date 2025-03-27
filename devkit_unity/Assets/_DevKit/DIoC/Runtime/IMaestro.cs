using DevKit.DIoC.Container;

namespace DevKit.DIoC
{
    public interface IMaestro
    {
        ITypeMapResolver<T> Get<T>() where T : class;

        ITypeMap<T> Map<T>() where T : class;

        ITypeMapReset<T> UnMap<T>() where T : class;

        void UnMapAll<T>() where T : class;

		void Reset();
    }
}
