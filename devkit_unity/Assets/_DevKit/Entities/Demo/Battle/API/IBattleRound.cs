using DevKit.Entities.API;

namespace DevKit.Entities.Demo.Battle.API
{
    /// <summary>
    /// Interface for Battle Round
    /// </summary>
    /// <remarks>
    /// Contains collection of Battle Teams <see cref="IBattleTeam"/>
    /// </remarks>
    public interface IBattleRound
    {
        IEntityCollection<string, IBattleTeam> Teams { get; }

        /// <summary>
        /// The number of current battle round
        /// </summary>
        int RoundCount { get; set; }
    }
}
