using DevKit.Entities.Demo.Battle.API;

namespace DevKit.Entities.Demo.Battle
{
    // TODO initiate battle state at the beginning of battle and setup with relevant Battle Round
    /// <summary>
    /// Battle state object with data about current round and etc
    /// </summary>
    public class BattleState : IBattleState
    {
        /// <inheritdoc />
        public IBattleRound CurrentRound { get; set; }
    }
}
