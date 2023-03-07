using DevKit.Entities.API;
using DevKit.Entities.Demo.Battle.API;

namespace DevKit.Entities.Demo.Battle
{
    // TODO instantiate battle team and add relevant battle units before the battle
    /// <summary>
    /// Battle team (PC or Player) that participate in battle round
    /// </summary>
    /// <remarks>
    /// Each team contains collection of battle units <see cref="BattleUnit"/>
    /// The battle will be made up of 2 teams each with 4 units.
    /// One team will be controlled by the player and the other team controlled by the computer.
    /// The player will be prompted to choose his/her 4 units and the computer will randomly select 4 units.
    /// </remarks>
    public class BattleTeam : Entity<IBattleTeam>, IBattleTeam
    {
        public IEntityCollection<string, IBattleUnit> Units { get; }
    }
}
