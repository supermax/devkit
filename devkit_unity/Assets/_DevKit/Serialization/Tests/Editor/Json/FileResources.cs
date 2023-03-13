using NUnit.Framework;
using UnityEngine;

namespace DevKit.Serialization.Tests.Editor.Json
{
	internal static class FileResources
	{
		public static TextAsset GetJsonTextAsset(string fileName)
		{
			Assert.That(fileName, Is.Not.Null);
			Assert.That(fileName, Is.Not.Empty);

			var jTextAsset = UnityEngine.Resources.Load<TextAsset>(fileName);
			return jTextAsset;
		}
	}
}
