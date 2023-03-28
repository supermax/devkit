using DevKit.Entities.Demo.Characters.API;
using DevKit.Entities.Demo.Game.API;

namespace DevKit.Entities.Demo.Characters.Players.API
{
    public interface IPlayerEntity : ICharacterEntity<IPlayerEntity>
    {
        IPlayerLoginProfile LoginProfile { get; set; }

        IGameSettings GameSettings { get; set; }
    }
}
