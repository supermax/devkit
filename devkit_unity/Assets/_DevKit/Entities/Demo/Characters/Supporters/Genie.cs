using DevKit.Entities.Demo.Characters.Supporters.API;

namespace DevKit.Entities.Demo.Characters.Supporters
{
    /// <summary>
    /// Genie supporter character
    /// </summary>
    /// <remarks>
    /// Extends <see cref="Supporter"/>
    /// Every third round the genie can grant a wish:
    ///   1) heal a unit for 5 life
    ///   2) give a unit +5 damage for 1 round
    ///   3) deal 3 damage to an enemy
    /// </remarks>
    public class Genie : Supporter, IGenie
    {

    }
}
