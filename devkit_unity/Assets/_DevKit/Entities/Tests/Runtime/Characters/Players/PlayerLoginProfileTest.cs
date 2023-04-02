using DevKit.Entities.Demo.Characters.Players;
using DevKit.Serialization.Json.Extensions;
using NUnit.Framework;
using UnityEngine;

namespace DevKit.Entities.Tests.Characters.Players
{
    [TestFixture]
    public class PlayerLoginProfileTest
    {
        [Test]
        public void PlayerLoginProfile_TestSerialization()
        {
            var profile = new PlayerLoginProfile();
            Assert.That(profile, Is.Not.Null);
            Assert.DoesNotThrow(() => profile.Init());

            var json = profile.ToJson();
            Debug.LogFormat("Original '{0}': {1}", profile.GetType().Name, json);
            Assert.That(json, Is.Not.Null);

            var profile1 = json.ToObject<PlayerLoginProfile>();
            Assert.That(profile1, Is.Not.Null);

            var json1 = profile1.ToJson();
            Debug.LogFormat("Clone '{0}': {1}", profile1.GetType().Name, json1);
            Assert.That(profile1.SessionId, Is.EqualTo(profile.SessionId));
            Assert.That(profile1.LoginType, Is.EqualTo(profile.LoginType));

            Assert.That(profile1.DeviceInfo, Is.Not.Null);
            Assert.That(profile1.DeviceInfo.Platform, Is.EqualTo(profile.DeviceInfo.Platform));
            Assert.That(profile1.DeviceInfo.DeviceModel, Is.EqualTo(profile.DeviceInfo.DeviceModel));
        }
    }
}
