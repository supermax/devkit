using DevKit.Entities.Demo.Characters.Fighters.API;

namespace DevKit.Entities.Demo.Characters.Fighters
{
    /// <summary>
    /// Ninja fighter character
    /// </summary>
    /// <remarks>
    /// Extends <see cref="Fighter"/>
    /// Ninjas are non-targetable for every second combat round.
    /// </remarks>
    public class Ninja : Fighter, INinja
    {

    }
}
