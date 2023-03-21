using DevKit.Entities.Demo.Game.API;

namespace DevKit.Entities.Demo.Characters.API
{
    public interface IPlayerEntity : ICharacterEntity<IPlayerEntity>
    {
        IGameSettings GameSettings { get; set; }
    }
}
