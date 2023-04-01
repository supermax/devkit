using System;
using DevKit.Entities.API;
using DevKit.Entities.Demo.Characters.Players.API;
using DevKit.Entities.Extensions;
using DevKit.Serialization.Json;
using DevKit.Serialization.Json.API;

namespace DevKit.Entities.Demo.Characters.Players
{
    [JsonDataContract]
    public class PlayerLoginProfile : Entity<IPlayerLoginProfile>, IPlayerLoginProfile
    {
        /// <inheritdoc />
        [JsonDataMemberIgnore]
        public override IEntityConfig Config { get; protected set; }

        /// <inheritdoc />
        [JsonDataMemberIgnore]
        public override string Error { get; protected set; }

        /// <inheritdoc />
        [JsonDataMemberIgnore]
        public override EntityPropertiesContainer PropertyValues { get; } = new();

        /// <summary>
        /// Game Session ID
        /// <remarks>
        /// <para>
        /// If Game does not need Server, then client's code will init <see cref="SessionId"/>
        /// </para>
        /// <para>
        /// If Game needs a Server, then <see cref="SessionId"/> value will be pulled from server in Login/Auth process
        /// </para>
        /// </remarks>
        /// </summary>
        [JsonDataMember(Name = "sid")]
        public string SessionId { get; set; }

        /// <summary>
        /// Username
        /// <remarks>
        /// <para>
        /// If Game does not need Server, then client's code will init <see cref="Username"/> and/or Player will edit it in runtime
        /// </para>
        /// <para>
        /// If Game needs a Server, then <see cref="Username"/> value will be pulled from server in Login/Auth process
        /// </para>
        /// </remarks>
        /// </summary>
        [JsonDataMember(Name = "username")]
        public string Username { get; set; }

        [JsonDataMember(Name = "tz", FallbackValue = 1, DefaultValue = 1)]
        public string TimeZone { get; set; }

        [JsonDataMember(Name = "isFirstLogin")]
        public bool IsFirstLogin { get; set; }

        [JsonDataMember(Name = "loginTime")]
        public DateTime LoginTime { get; set; }

        [JsonDataMember(Name = "deviceInfo")]
        public PlayerDeviceInfo DeviceInfo { get; set; }

        [JsonDataMember(Name = "loginType")]
        public LoginType LoginType { get; set; }

        public override void Init()
        {
            base.Init();

            Id = this.GetId();
            TypeId = this.GetTypeId();
            LoginType = LoginType.Guest;
            LoginTime = DateTime.UtcNow;
            TimeZone = System.TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now)
                ? System.TimeZone.CurrentTimeZone.DaylightName
                : System.TimeZone.CurrentTimeZone.StandardName;

            if (DeviceInfo != null)
            {
                return;
            }
            DeviceInfo = new PlayerDeviceInfo();
            DeviceInfo.Init();
        }

        public override void Init(IEntityConfig config)
        {
            Config = config;
        }

        static PlayerLoginProfile()
        {
            JsonMapper.Default.RegisterExporter<LoginType, string>(val => val.ToString());
            JsonMapper.Default.RegisterImporter<int, LoginType>(jVal => (LoginType)jVal);
        }
    }
}
