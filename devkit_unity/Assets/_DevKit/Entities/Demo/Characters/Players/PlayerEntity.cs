using DevKit.Entities.Demo.Characters.Players.API;
using DevKit.Entities.Demo.Game.API;

namespace DevKit.Entities.Demo.Characters.Players
{
    public class PlayerEntity
        : CharacterEntity<IPlayerEntity>
            , IPlayerEntity
    {
        public IGameSettings GameSettings { get; set; }
    }
}
