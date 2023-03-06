namespace DevKit.Entities.Demo.Battle.API
{
    /// <summary>
    /// Interface for Battle State
    /// </summary>
    public interface IBattleState
    {
        /// <summary>
        /// Current battle round
        /// </summary>
        IBattleRound CurrentRound { get; set; }
    }
}
