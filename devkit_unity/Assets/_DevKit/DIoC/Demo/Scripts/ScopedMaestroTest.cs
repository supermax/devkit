using DevKit.Core.Objects;
using DevKit.DIoC.Demo.Entities;
using DevKit.DIoC.Demo.Views;
using DevKit.Logging.Extensions;
using UnityEngine;

namespace DevKit.DIoC.Demo
{
    public class ScopedMaestroTest : BaseMonoBehaviour
    {
        public IMaestro MaestroInstance { get; set; }

        [SerializeField]
        private MyView _myView;

        protected override void OnAwake()
        {
            base.OnAwake();

            MaestroInstance = new Maestro();
            this.LogInfo($"*** {nameof(MaestroInstance)}.{nameof(MaestroInstance.GetHashCode)}(): {MaestroInstance.GetHashCode()}");

            var map = MaestroInstance.Map<IEnemy>().Singleton<Witch>();
            this.LogInfo($"*** {nameof(map)}.{nameof(map.GetHashCode)}(): {map.GetHashCode()}");

            var instance = MaestroInstance.Get<IEnemy>().Instance();
            this.LogInfo($"*** {nameof(instance)}.{nameof(instance.GetHashCode)}(): {instance.GetHashCode()}");

            var unmap = MaestroInstance.UnMap<IEnemy>().From<Witch>();
            this.LogInfo($"*** {nameof(unmap)}.{nameof(unmap.GetHashCode)}(): {unmap.GetHashCode()}");

            var viewMap = MaestroInstance.Map<MyView>().Singleton(_myView);
            this.LogInfo($"*** {nameof(viewMap)}.{nameof(viewMap.GetHashCode)}(): {viewMap.GetHashCode()}");

            var viewInstance = MaestroInstance.Get<MyView>().Instance();
            this.LogInfo($"*** {nameof(viewInstance)}.{nameof(viewInstance.GetHashCode)}(): {viewInstance.GetHashCode()}");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            MaestroInstance.UnMapAll<IEnemy>();
            this.LogInfo($"*** {nameof(MaestroInstance)}.{nameof(MaestroInstance.UnMapAll)}<{nameof(IEnemy)}>");

            MaestroInstance.UnMapAll<MyView>();
            this.LogInfo($"*** {nameof(MaestroInstance)}.{nameof(MaestroInstance.UnMapAll)}<{nameof(MyView)}>");

            var maestroImpl = (Maestro)MaestroInstance;
            maestroImpl.Dispose();

            MaestroInstance = null;
        }
    }
}
