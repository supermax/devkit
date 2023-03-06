using DevKit.Entities.API;
using DevKit.Entities.Demo.Battle.API;
using DevKit.Entities.Demo.Characters.API;

namespace DevKit.Entities.Demo.Battle
{
    // TODO setup unit at the beginning of the battle according to game spec
    /// <summary>
    /// Battle Unit - contains collection of characters that participate in battle
    /// </summary>
    /// <remarks>
    /// Extends <see cref="EntityCollection{TKey, TValue}"/>
    /// May contains different types of <see cref="ICharacter"/>
    /// </remarks>
    public class BattleUnit : Entity<IBattleUnit>, IBattleUnit
    {
        public IEntityCollection<string, ICharacter> Characters { get; }
    }
}
