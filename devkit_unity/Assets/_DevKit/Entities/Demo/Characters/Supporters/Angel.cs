using DevKit.Entities.Demo.Characters.Supporters.API;

namespace DevKit.Entities.Demo.Characters.Supporters
{
    /// <summary>
    /// Angel supporter character
    /// </summary>
    /// <remarks>
    /// Extends <see cref="Supporter"/>
    /// Angels can’t attack. Every round they can bless another unit.
    /// A blessing will heal the unit for 3 life per round for 3 rounds.
    /// Angels can’t heal themselves.
    /// </remarks>
    public class Angel : Supporter, IAngel
    {

    }
}
