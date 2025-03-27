namespace DevKit.DIoC.Extensions
{
    public static class MaestroExtensions
    {
        public static bool TryGetInstance<T>(this IMaestro maestro, out T service) where T : class
        {
            service = maestro.Get<T>()?.Instance();
            return service != null;
        }
    }
}