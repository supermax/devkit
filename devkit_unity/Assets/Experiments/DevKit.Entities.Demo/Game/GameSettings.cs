using DevKit.Entities.API;
using DevKit.Entities.Demo.Game.API;

namespace DevKit.Entities.Demo.Game
{
    public class GameSettings : Entity<IGameSettings>, IGameSettings
    {
        public bool IsMusicEnabled
        {
            get { return GetPropertyValue().Bool.GetValueOrDefault(); }
            set { SetPropertyValue(value); }
        }

        public bool IsSfxEnabled
        {
            get { return GetPropertyValue().Bool.GetValueOrDefault(); }
            set { SetPropertyValue(value); }
        }

        public bool PushNotificationsEnabled
        {
            get { return GetPropertyValue().Bool.GetValueOrDefault(); }
            set { SetPropertyValue(value); }
        }

        public override void Init(IEntityConfig config)
        {
            BeginUpdate();
            Config = config;
            IsMusicEnabled = config.GetPropertyInitialValue(nameof(IsMusicEnabled)).Bool.GetValueOrDefault();
            IsSfxEnabled = config.GetPropertyInitialValue(nameof(IsSfxEnabled)).Bool.GetValueOrDefault();
            PushNotificationsEnabled = config.GetPropertyInitialValue(nameof(PushNotificationsEnabled)).Bool.GetValueOrDefault();
            EndUpdate();
        }
    }
}
