using DevKit.Entities.API;

namespace DevKit.Entities.Demo.Game.API
{
    public interface IGameSettings : IEntity<IGameSettings>
    {
        bool IsMusicEnabled { get; set; }

        bool IsSfxEnabled { get; set; }

        bool PushNotificationsEnabled { get; set; }
    }
}
