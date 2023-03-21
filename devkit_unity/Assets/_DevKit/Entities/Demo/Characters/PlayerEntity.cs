using DevKit.Entities.Demo.Characters.API;
using DevKit.Entities.Demo.Game.API;

namespace DevKit.Entities.Demo.Characters
{
    public class PlayerEntity
        : CharacterEntity<IPlayerEntity>
            , IPlayerEntity
    {
        public IGameSettings GameSettings { get; set; }
    }
}
