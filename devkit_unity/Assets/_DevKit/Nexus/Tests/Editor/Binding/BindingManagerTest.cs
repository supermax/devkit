using DevKit.Nexus.Binding;
using DevKit.Nexus.Binding.API;
using DevKit.Nexus.Tests.Editor.Binding.Models;
using NUnit.Framework;
using UnityEngine;

namespace DevKit.Nexus.Tests.Editor.Binding
{
    public class BindingManagerTest
    {
        [Test]
        public void BindingManager_OneWayBinding_Test()
        {
            var targetModel = new SimpleViewModel();
            const string targetPath = nameof(targetModel.Name);

            var sourceModel = new ObservableViewModel{ Name = Application.productName };
            sourceModel.Init();
            const string sourcePath = nameof(sourceModel.Name);

            var binding = BindingManager.Default.Bind(sourceModel, sourcePath, targetModel, targetPath, BindingMode.OneWay);
            Assert.That(binding, Is.Not.Null);
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.Name));
            Debug.Log($"[OneWayBinding] Initialized " +
                      $"{nameof(targetModel)}.{nameof(targetModel.Name)} ({targetModel.Name}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");

            sourceModel.Name = Application.companyName;
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.Name));
            Debug.Log($"[OneWayBinding] Updated " +
                      $"{nameof(targetModel)}.{nameof(targetModel.Name)} ({targetModel.Name}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");
        }

        [Test]
        public void BindingManager_TwoWayBinding_Test()
        {
            var targetModel = new ObservableViewModel();
            targetModel.Init();
            const string targetPath = nameof(targetModel.Name);

            var sourceModel = new ObservableViewModel{ Name = Application.productName };
            sourceModel.Init();
            const string sourcePath = nameof(sourceModel.Name);

            var binding = BindingManager.Default.Bind(sourceModel, sourcePath, targetModel, targetPath, BindingMode.TwoWay);
            Assert.That(binding, Is.Not.Null);
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.Name));
            Debug.Log($"[TwoWayBinding] Initialized " +
                      $"{nameof(targetModel)}.{nameof(targetModel.Name)} ({targetModel.Name}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");

            sourceModel.Name = Application.companyName;
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.Name));
            Debug.Log($"[TwoWayBinding] Updated " +
                      $"{nameof(targetModel)}.{nameof(targetModel.Name)} ({targetModel.Name}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");

            targetModel.Name = Application.identifier;
            Assert.That(sourceModel.Name, Is.SameAs(targetModel.Name));
            Debug.Log($"[TwoWayBinding] Updated " +
                      $"{nameof(sourceModel)}.{nameof(sourceModel.Name)} ({sourceModel.Name}) " +
                      $"from {nameof(targetModel)}.{targetPath}");
        }
    }
}
