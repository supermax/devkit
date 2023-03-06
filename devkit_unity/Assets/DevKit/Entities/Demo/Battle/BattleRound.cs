using DevKit.Entities.API;
using DevKit.Entities.Demo.Battle.API;

namespace DevKit.Entities.Demo.Battle
{
    // TODO implement round generation and execution logic
    /// <summary>
    /// Represents battle round and contains collection of teams that participiate in this round
    /// </summary>
    /// <remarks>
    /// Combat is broken up into rounds.
    /// Each unit will have an opportunity to attack another unit each round.
    /// The system will choose the units one by one in a random order to do their attack.
    /// The player will be prompted to select a target for each of his/her units’ attacks.
    /// The computer will choose targets based on some AI logic.
    /// </remarks>
    public class BattleRound : Entity<IBattleRound>, IBattleRound
    {
        public IEntityCollection<string, IBattleTeam> Teams { get; }

        /// <inheritdoc />
        public int RoundCount { get; set; }
    }
}
