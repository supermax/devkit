using DevKit.Core.Extensions;
using DevKit.DIoC.Config;
using NUnit.Framework;
using DevKit.Editor.IOC.Tests.Entities;
using UnityEngine;

namespace DevKit.Editor.IOC.Tests.Config
{
    [TestFixture]
    public class TypeConfigTest
    {
        [Test]
        public void Maestro_TypeConfig_Save_Test()
        {
            var config = new TypeConfig
                {
                    InitTrigger = TypeInitTrigger.OnMapping,
                    SourceType = typeof(Animal).FullName
                };
            var json = config.ToJson();
            Debug.LogFormat("{0}", json);
            Assert.NotNull(json);

            config = json.FromJson<TypeConfig>();
            Debug.LogFormat("{0}", config);
            Assert.NotNull(config);
        }
    }
}
