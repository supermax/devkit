using DevKit.Entities.Demo.Extensions;
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

            entityConfig.PropertyValues = new EntityPropertiesContainer();
            for (var i = 0; i < 100; i++)
            {
                var holder = new PropertyValueHolder();
                var opt = Random.Range(0, 3);
                switch (opt)
                {
                    case 0:
                        holder.Bool = true;
                        break;

                    case 1:
                        holder.Number = Random.Range(0, 1000001);
                        break;

                    case 2:
                        holder.Text = $"text_{i}";
                        break;
                }
                entityConfig.PropertyValues[$"prop_{i}"] = holder;
            }
            Assert.That(entityConfig.PropertyValues.Count, Is.EqualTo(100));

            var json = entityConfig.PropertyValues.ToJson();
            Debug.Log(json);
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Is.Not.Empty);

            var container = new EntityPropertiesContainer();
            var deserializedConfig = container.FromJson(json);
            Assert.That(deserializedConfig, Is.Not.Null);
            Assert.That(deserializedConfig.Count, Is.EqualTo(entityConfig.PropertyValues.Count));
        }
    }
}
