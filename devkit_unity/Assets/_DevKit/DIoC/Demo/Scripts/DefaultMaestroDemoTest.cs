using DevKit.Core.Objects;
using DevKit.DIoC.Demo.Entities;
using DevKit.Logging.Extensions;

namespace DevKit.DIoC.Demo
{
    public class DefaultMaestroDemoTest : BaseMonoBehaviour
    {
        private void Start()
        {
            this.LogInfo($"+++ {nameof(Maestro)}.{nameof(Maestro.Default)}.{nameof(Maestro.Default.GetHashCode)}(): " +
                         $"{Maestro.Default.GetHashCode()}");

            var map = Maestro.Default.Map<IEnemy>().Singleton<Witch>();
            this.LogInfo($"+++ {nameof(map)}.{nameof(map.GetHashCode)}(): {map.GetHashCode()}");

            var instance = Maestro.Default.Get<IEnemy>().Instance();
            this.LogInfo($"+++ {nameof(instance)}.{nameof(instance.GetHashCode)}(): {instance.GetHashCode()}");

            var unmap = Maestro.Default.UnMap<IEnemy>().From<Witch>();
            this.LogInfo($"+++ {nameof(unmap)}.{nameof(unmap.GetHashCode)}(): {unmap.GetHashCode()}");
        }
    }
}
