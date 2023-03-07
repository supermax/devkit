using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace DevKit.Entities.Tests.Engine
{
    public class EngineTest
    {
        [Test]
        public void Engine_Instance_Test()
        {
            //var engine = new DevKit.Entities.
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator EngineTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
