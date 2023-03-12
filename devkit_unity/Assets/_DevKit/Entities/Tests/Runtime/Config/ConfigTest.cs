using DevKit.Core.Extensions;
using DevKit.Serialization.Json.Extensions;
using NUnit.Framework;
using UnityEngine;

namespace DevKit.Entities.Tests.Config
{
    [TestFixture]
    public class ConfigTest
    {
        [Test]
        public void EntityConfig_Serialization_Test()
        {
            var entityConfig = new EntityConfig();
            Assert.That(entityConfig, Is.Not.Null);
            Assert.That(entityConfig.PropertyValues, Is.Not.Null);

            for (var i = 0; i < 100; i++)
            {
                var holder = new PropertyValueHolder
                    {
                        Number = Random.value,
                        Bool = Random.Range(0, 1) != 0,
                        Text = $"prop_{i}"
                    };
                entityConfig.PropertyValues[holder.Text] = holder;
            }
            Assert.That(entityConfig.PropertyValues.Count, Is.EqualTo(100));

            var json = entityConfig.ToJson();
            Debug.Log(json);

            Assert.That(json, Is.Not.Null);
            Assert.That(json, Is.Not.Empty);
        }
    }
}
