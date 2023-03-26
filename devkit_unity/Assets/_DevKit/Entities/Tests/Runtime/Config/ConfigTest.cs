using System.Collections.Generic;
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

            entityConfig.PropertyValues = new Dictionary<string, PropertyValueHolder>();
            for (var i = 0; i < 100; i++)
            {
                var holder = new PropertyValueHolder(Random.value)
                             .SetValue(Random.Range(0, 1) != 0)
                             .SetValue(i % 2 == 0 ? $"prop_{i}" : null);
                entityConfig.PropertyValues[$"prop_{i}"] = holder;
            }
            Assert.That(entityConfig.PropertyValues.Count, Is.EqualTo(100));

            var json = entityConfig.PropertyValues.ToJson();
            Debug.Log(json);
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Is.Not.Empty);

            var deserializedConfig = json.ToObject<Dictionary<string, PropertyValueHolder>>();
            Assert.That(deserializedConfig, Is.Not.Null);
            Assert.That(deserializedConfig.Count, Is.EqualTo(entityConfig.PropertyValues.Count));
        }
    }
}
