using DevKit.Entities.Demo.Characters.Players;
using DevKit.Serialization.Json.Extensions;
using NUnit.Framework;

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
            Assert.That(json, Is.Not.Null);

            var profile1 = json.ToObject<PlayerLoginProfile>();
            Assert.That(profile1, Is.Not.Null);
            Assert.That(profile1.Id, Is.EqualTo(profile.Id));
            Assert.That(profile1.TypeId, Is.EqualTo(profile.TypeId));
            Assert.That(profile1.SessionId, Is.EqualTo(profile.SessionId));
            Assert.That(profile1.LoginType, Is.EqualTo(profile.LoginType));
            
            Assert.That(profile1.DeviceInfo, Is.Not.Null);
            Assert.That(profile1.DeviceInfo.Id, Is.EqualTo(profile.DeviceInfo.Id));
        }
    }
}
