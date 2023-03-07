using DevKit.Entities.API;

namespace DevKit.Entities.Demo.Battle.API
{
    /// <summary>
    /// Interface Battle Team
    /// </summary>
    /// <remarks>
    /// Collection of battle units <see cref="IBattleUnit"/>
    /// </remarks>
    public interface IBattleTeam : IEntity<IBattleTeam>
    {
        IEntityCollection<string, IBattleUnit> Units { get; }
    }
}
