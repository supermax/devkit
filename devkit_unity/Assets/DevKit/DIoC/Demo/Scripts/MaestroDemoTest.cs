using DevKit.DIoC.Demo.Entities;
using UnityEngine;

namespace DevKit.DIoC.Demo
{
    // TODO add more tests
    public class MaestroDemoTest : MonoBehaviour
    {
        private void Start()
        {
            var map = Maestro.Default.Map<IEnemy>().To<Witch>();
            Debug.Log(map);

            var instance = Maestro.Default.Get<IEnemy>().Instance();
            Debug.Log(instance);

            var unmap = Maestro.Default.UnMap<IEnemy>().From<Witch>();
            Debug.Log(unmap);
        }
    }
}