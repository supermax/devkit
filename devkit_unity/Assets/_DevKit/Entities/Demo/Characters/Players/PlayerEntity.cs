using DevKit.Entities.Demo.Characters.Players.API;
using DevKit.Entities.Demo.Game.API;
using DevKit.Serialization.Json.API;

namespace DevKit.Entities.Demo.Characters.Players
{
    public class PlayerEntity
        : CharacterEntity<IPlayerEntity>
            , IPlayerEntity
    {
        [JsonDataMember("loginProfile")]
        public IPlayerLoginProfile LoginProfile { get; set; }

        [JsonDataMember("gameSettings")]
        public IGameSettings GameSettings { get; set; }
    }
}
