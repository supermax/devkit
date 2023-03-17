using DevKit.Entities.API;
using DevKit.Entities.Demo.Characters.API;

namespace DevKit.Entities.Demo.Battle.API
{
    /// <summary>
    /// Interface for Battle Unit
    /// </summary>
    /// <remarks>
    /// Battle Unit belongs to <see cref="IBattleTeam"/> and participates in <see cref="IBattleRound"/>
    /// </remarks>
    public interface IBattleUnit : IEntity<IBattleUnit>
    {
        IEntityCollection<string, ICharacterEntity> Characters { get; }
    }
}
