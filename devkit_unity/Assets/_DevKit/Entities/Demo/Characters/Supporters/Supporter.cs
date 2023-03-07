using DevKit.Entities.Demo.Characters.Supporters.API;

namespace DevKit.Entities.Demo.Characters.Supporters
{
    /// <summary>
    /// Base class for Supporter characters
    /// </summary>
    /// <remarks>
    /// Extedns <see cref="Character"/>
    /// Support characters can’t be targeted if there is a fighter alive on the team.
    /// </remarks>
    public abstract class Supporter : Character, ISupporter
    {

    }
}
