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
            container.FromJson(json);
            Assert.That(container, Is.Not.Null);
            Assert.That(container.Count, Is.EqualTo(entityConfig.PropertyValues.Count));

            foreach (var pair in entityConfig.PropertyValues)
            {
                Assert.That(pair.Key, Is.Not.Null);
                Assert.That(pair.Value, Is.Not.Null);

                var value = container[pair.Key];
                Assert.That(pair.Value.Bool, Is.EqualTo(value.Bool));
                Assert.That(pair.Value.Number, Is.EqualTo(value.Number));
                Assert.That(pair.Value.Text, Is.EqualTo(value.Text));
            }
        }
    }
}
