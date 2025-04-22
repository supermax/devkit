using DevKit.Nexus.Binding;
using DevKit.Nexus.Binding.API;
using DevKit.Nexus.Tests.Editor.Binding.Models;
using NUnit.Framework;
using UnityEngine;

namespace DevKit.Nexus.Tests.Editor.Binding
{
    public class PropertyBindingTest
    {
        [Test]
        public void BindingManager_OneWayBinding_SimplePath_Test()
        {
            var targetModel = new SimpleModel();
            const string targetPath = nameof(targetModel.Name);
            
            var go = new GameObject();
            var sourceModel = go.AddComponent<ObservableViewModelTestFixture>();
            sourceModel.Name = Application.productName;
            sourceModel.Init();
            const string sourcePath = nameof(sourceModel.Name);

            var binding = BindingManager.Default.Bind(sourceModel, sourcePath, targetModel, targetPath, BindingMode.OneWay);
            Assert.That(binding, Is.Not.Null);
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.Name));
            Debug.Log($"[{BindingMode.OneWay}] Initialized " +
                      $"{nameof(targetModel)}.{nameof(targetModel.Name)} ({targetModel.Name}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");

            sourceModel.Name = Application.companyName;
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.Name));
            Debug.Log($"[{BindingMode.OneWay}] Updated " +
                      $"{nameof(targetModel)}.{nameof(targetModel.Name)} ({targetModel.Name}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");
        }

        [Test]
        public void BindingManager_TwoWayBinding_SimplePath_Test()
        {
            var go = new GameObject();
            var targetModel = go.AddComponent<ObservableViewModelTestFixture>();
            targetModel.Init();
            const string targetPath = nameof(targetModel.Name);

            var sourceModel = go.AddComponent<ObservableViewModelTestFixture>();
            sourceModel.Name = Application.productName;
            sourceModel.Init();
            const string sourcePath = nameof(sourceModel.Name);

            var binding = BindingManager.Default.Bind(sourceModel, sourcePath, targetModel, targetPath, BindingMode.TwoWay);
            Assert.That(binding, Is.Not.Null);
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.Name));
            Debug.Log($"[{BindingMode.TwoWay}] Initialized " +
                      $"{nameof(targetModel)}.{nameof(targetModel.Name)} ({targetModel.Name}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");

            sourceModel.Name = Application.companyName;
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.Name));
            Debug.Log($"[{BindingMode.TwoWay}] Updated " +
                      $"{nameof(targetModel)}.{nameof(targetModel.Name)} ({targetModel.Name}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");

            targetModel.Name = Application.identifier;
            Assert.That(sourceModel.Name, Is.SameAs(targetModel.Name));
            Debug.Log($"[{BindingMode.TwoWay}] Updated " +
                      $"{nameof(sourceModel)}.{nameof(sourceModel.Name)} ({sourceModel.Name}) " +
                      $"from {nameof(targetModel)}.{targetPath}");
        }

        [Test]
        public void BindingManager_OneWayBinding_ComplexPath_Test()
        {
            var targetModel = new SimpleModel();
            const string targetPath = nameof(targetModel.Name);

            var go = new GameObject();
            var sourceModel = go.AddComponent<ObservableViewModelTestFixture>();
            sourceModel.ChildModel.Name = Application.companyName;
            sourceModel.Init();
            const string sourcePath = nameof(sourceModel.ChildModel) + "." + nameof(sourceModel.ChildModel.Name);

            var binding = BindingManager.Default.Bind(sourceModel, sourcePath, targetModel, targetPath, BindingMode.OneWay);
            Assert.That(binding, Is.Not.Null);
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.ChildModel.Name));
            Debug.Log($"[{BindingMode.OneWay}] Initialized " +
                      $"{nameof(targetModel)}.{nameof(targetModel.Name)} ({targetModel.Name}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");

            sourceModel.ChildModel.Name = Application.identifier;
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.ChildModel.Name));
            Debug.Log($"[{BindingMode.OneWay}] Updated " +
                      $"{nameof(targetModel)}.{nameof(targetModel.Name)} ({targetModel.Name}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");
        }

        [Test]
        public void BindingManager_TwoWayBinding_ComplexPath_Test()
        {
            var go = new GameObject();
            var targetModel = go.AddComponent<ObservableViewModelTestFixture>();
            targetModel.Init();
            const string targetPath = nameof(targetModel.Name);

            var sourceModel = go.AddComponent<ObservableViewModelTestFixture>();
            sourceModel.ChildModel.Name = Application.companyName;
            sourceModel.Init();
            const string sourcePath = nameof(sourceModel.ChildModel) + "." + nameof(sourceModel.ChildModel.Name);

            var binding = BindingManager.Default.Bind(sourceModel, sourcePath, targetModel, targetPath, BindingMode.TwoWay);
            Assert.That(binding, Is.Not.Null);
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.ChildModel.Name));
            Debug.Log($"[{BindingMode.TwoWay}] Initialized " +
                      $"{nameof(targetModel)}.{nameof(targetModel.Name)} ({targetModel.Name}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");

            sourceModel.ChildModel.Name = Application.identifier;
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.ChildModel.Name));
            Debug.Log($"[{BindingMode.TwoWay}] Updated " +
                      $"{nameof(targetModel)}.{nameof(targetModel.Name)} ({targetModel.Name}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");

            targetModel.Name = Application.productName;
            Assert.That(sourceModel.ChildModel.Name, Is.SameAs(targetModel.Name));
            Debug.Log($"[{BindingMode.TwoWay}] Updated " +
                      $"{nameof(sourceModel)}.{nameof(sourceModel.Name)} ({sourceModel.Name}) " +
                      $"from {nameof(targetModel)}.{targetPath}");
        }
    }
}
