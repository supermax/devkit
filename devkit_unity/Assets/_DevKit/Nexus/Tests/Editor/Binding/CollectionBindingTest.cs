using DevKit.Nexus.Binding;
using DevKit.Nexus.Binding.API;
using DevKit.Nexus.Tests.Editor.Binding.Models;
using NUnit.Framework;
using UnityEngine;

namespace DevKit.Nexus.Tests.Editor.Binding
{
    public class CollectionBindingTest
    {
        [Test]
        public void BindingManager_ObservableList_OneWayBinding_SimplePath_Test()
        {
            var targetModel = new SimpleModel();
            const string targetPath = nameof(targetModel.List);

            var sourceModel = new ObservableViewModel
                {
                    List = { "a", "b", "c" }
                };
            sourceModel.Init();
            const string sourcePath = nameof(sourceModel.List);

            var binding = BindingManager.Default.Bind(sourceModel, sourcePath, targetModel, targetPath, BindingMode.OneWay);
            Assert.That(binding, Is.Not.Null);

            // check if init works
            Assert.That(targetModel.List.Count, Is.EqualTo(sourceModel.List.Count));
            Debug.Log($"[{BindingMode.OneWay}] Initialized " +
                      $"{nameof(targetModel)}.{nameof(targetModel.List)} ({targetModel.List}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");

            // check if `add` works
            sourceModel.List.Add("d");
            Assert.That(targetModel.List.Count, Is.EqualTo(sourceModel.List.Count));
            Debug.Log($"[{BindingMode.OneWay}] Added new item to " +
                      $"{nameof(targetModel)}.{nameof(targetModel.List)} ({targetModel.List}) " +
                      $"from {nameof(sourceModel)}.{sourcePath}");

            // check if `remove` works
            sourceModel.List.Remove("a");
            Assert.That(targetModel.List.Count, Is.EqualTo(sourceModel.List.Count));
            Debug.Log($"[{BindingMode.OneWay}] Removed item from " +
                      $"{nameof(targetModel)}.{nameof(targetModel.List)} ({targetModel.List}) " +
                      $"and from {nameof(sourceModel)}.{sourcePath}");

            // check if `remove` works
            sourceModel.List.Insert(0, "a");
            Assert.That(targetModel.List.Count, Is.EqualTo(sourceModel.List.Count));
            Debug.Log($"[{BindingMode.OneWay}] Replaced item in " +
                      $"{nameof(targetModel)}.{nameof(targetModel.List)} ({targetModel.List}) " +
                      $"and in {nameof(sourceModel)}.{sourcePath}");

            // check if `reset` works
            sourceModel.List.Clear();
            Assert.That(targetModel.List.Count, Is.EqualTo(sourceModel.List.Count));
            Debug.Log($"[{BindingMode.OneWay}] Cleared " +
                      $"{nameof(targetModel)}.{nameof(targetModel.List)} ({targetModel.List}) " +
                      $"and {nameof(sourceModel)}.{sourcePath}");
        }
    }
}
