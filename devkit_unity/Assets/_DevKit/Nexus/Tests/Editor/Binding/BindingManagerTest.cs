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
        public void BindingManager_Binding_SimplePath_Test()
        {
            var targetModel = new SimpleViewModel();
            const string targetPath = nameof(targetModel.Name);

            var sourceModel = new ObservableViewModel{ Name = Application.productName };
            const string sourcePath = nameof(sourceModel.Name);

            var binding = BindingManager.Default.Bind(sourceModel, sourcePath, targetModel, targetPath, BindingMode.OneWay);
            Assert.That(binding, Is.Not.Null);
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.Name));

            sourceModel.Name = Application.companyName;
            Assert.That(targetModel.Name, Is.SameAs(sourceModel.Name));
        }

        // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // // `yield return null;` to skip a frame.
        // [UnityTest]
        // public IEnumerator BindingManagerTestWithEnumeratorPasses()
        // {
        //     // Use the Assert class to test conditions.
        //     // Use yield to skip a frame.
        //     yield return null;
        // }
    }
}
