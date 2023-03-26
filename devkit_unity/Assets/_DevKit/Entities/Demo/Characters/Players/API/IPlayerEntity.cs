using DevKit.Entities.Demo.Characters.API;
using DevKit.Entities.Demo.Game.API;

namespace DevKit.Entities.Demo.Characters.Players.API
{
    public interface IPlayerEntity : ICharacterEntity<IPlayerEntity>
    {
        IGameSettings GameSettings { get; set; }
    }
}
