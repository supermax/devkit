﻿using DevKit.DIoC.Tests.Editor.Entities;
using NUnit.Framework;
using UnityEngine;

namespace DevKit.DIoC.Tests.Editor
{
    /* TODO Add tests:
        1) Singleton
        2) Unmap
        3) Reset
        4) Dispose
        5) All other APIs
     */
    [TestFixture]
    public class MaestroTest
    {
        [SetUp]
        [Test]
        [Order(0)]
        public void Maestro_Map_Test()
        {
            var food = Maestro.Default
                                            .Map<IFood>()
                                            .To<Food>();
            Debug.Log($"test {nameof(Maestro_Map_Test)}<{nameof(IFood)}> result {food}");
            Assert.NotNull(food, $"failed to Map<{nameof(IFood)}>");

            var animal = Maestro.Default
                                            .Map<IAnimal>()
                                            .To<Dog>("dog")
                                            .To<Carp>("fish")
                                            .Singleton(new Cat());
            Debug.Log($"test {nameof(Maestro_Map_Test)}<{nameof(IAnimal)}> result {animal}");
            Assert.NotNull(animal, $"failed to Map<{nameof(IAnimal)}>");

            var mammal = Maestro.Default
                                                .Map<Mammal>()
                                                .To<Dog>()
                                                .To<Cat>();
            Debug.Log($"test {nameof(Maestro_Map_Test)}<{nameof(Mammal)}> result {mammal}");
            Assert.NotNull(animal, $"failed to Map<{nameof(Mammal)}>");

            var fish = Maestro.Default
                                        .Map<Fish>()
                                        .Singleton<Carp>()
                                        .Singleton<Shark>("jaws")
                                        .Singleton<Carp>("fishy");
            Debug.Log($"test {nameof(Maestro_Map_Test)}<{nameof(Fish)}> result {fish}");
            Assert.NotNull(fish, $"failed to Map<{nameof(Fish)}>");
        }

        [Test]
        [Order(1)]
        public void Maestro_Get_Test()
        {
            Maestro_Get_Test<IAnimal>();

            Maestro_Get_Test<Mammal>();

            Maestro_Get_Test<Fish>();
        }

        [Test]
        [Order(2)]
        public void Maestro_Get_Instance_Test()
        {
            Maestro_Get_Instance_Test<IAnimal>();

            Maestro_Get_Instance_Test<Mammal>();

            Maestro_Get_Instance_Test<Fish>();
        }

        [Test]
        [Order(3)]
        public void Maestro_Get_Specific_Instance_Test()
        {
            Maestro_Get_Instance_Test<IAnimal>("dog");
            Maestro_Get_Instance_Test<IAnimal>("fish");

            Maestro_Get_Instance_Test<Mammal>(typeof(Dog).FullName);
            Maestro_Get_Instance_Test<Mammal>(typeof(Cat).FullName);

            Maestro_Get_Instance_Test<Fish>("jaws");
            Maestro_Get_Instance_Test<Fish>("fishy");
        }

        [Test]
        [Order(4)]
        public void Maestro_Injectable_Properties_Test()
        {
            var dog = Maestro.Default.Get<IAnimal>().Instance("dog") as Dog;
            Debug.Log($"test {nameof(Maestro_Injectable_Properties_Test)}<{nameof(IAnimal)}>(\"dog\").Food = {dog?.Food}");
            Assert.NotNull(dog?.Food, $"failed to Get<{nameof(IAnimal)}>.Instance(\"dog\")");
        }

        private static void Maestro_Get_Instance_Test<T>(string key = null) where T : class
        {
            var instance = Maestro.Default.Get<T>().Instance(key);
            Debug.Log($"test {nameof(Maestro_Get_Instance_Test)}<{typeof(T).Name}>({key}) result {instance}");
            Assert.NotNull(instance, $"failed to Get<{nameof(T)}>.Instance({key})");
        }

        private static void Maestro_Get_Test<T>() where T : class
        {
            var result = Maestro.Default.Get<T>();
            Debug.Log($"test {nameof(Maestro_Get_Test)}<{typeof(T).Name}> result {result}");
            Assert.NotNull(result, $"failed to Get<{typeof(T).Name}>");
        }

        [Test]
        [Order(5)]
        public void Maestro_Singleton_Test()
        {
            var fish1 = Maestro.Default.Get<Fish>().Instance();
            var hash1 = fish1.GetHashCode();
            Debug.Log($"test {nameof(Maestro_Singleton_Test)}.{nameof(Maestro.Default.Get)}<{nameof(Fish)}>().Instance() = ID: {hash1}");
            Assert.NotNull(fish1, $"failed to Get<{nameof(Fish)}>.Instance()");

            var fish2 = Maestro.Default.Get<Fish>().Instance();
            var hash2 = fish2.GetHashCode();
            Debug.Log($"test {nameof(Maestro_Singleton_Test)}.{nameof(Maestro.Default.Get)}<{nameof(Fish)}>().Instance() = ID: {hash2}");
            Assert.NotNull(fish2, $"failed to Get<{nameof(Fish)}>.Instance()");

            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        [Order(6)]
        public void Maestro_Singleton_ByKey_Test()
        {
            const string singletonKey = "fishy";

            var fish1 = Maestro.Default.Get<Fish>().Instance(singletonKey);
            var hash1 = fish1.GetHashCode();
            Debug.Log($"test {nameof(Maestro_Singleton_ByKey_Test)}.{nameof(Maestro.Default.Get)}<{nameof(Fish)}>().Instance() = ID: {hash1}");
            Assert.NotNull(fish1, $"failed to Get<{nameof(Fish)}>.Instance()");

            var fish2 = Maestro.Default.Get<Fish>().Instance(singletonKey);
            var hash2 = fish2.GetHashCode();
            Debug.Log($"test {nameof(Maestro_Singleton_ByKey_Test)}.{nameof(Maestro.Default.Get)}<{nameof(Fish)}>().Instance() = ID: {hash2}");
            Assert.NotNull(fish2, $"failed to Get<{nameof(Fish)}>.Instance()");

            Assert.AreEqual(hash1, hash2);
        }
    }
}
