using DevKit.Entities.Demo.Characters.Casters.API;

namespace DevKit.Entities.Demo.Characters.Casters
{
    /// <summary>
    /// Base class for all caster characters
    /// </summary>
    /// <remarks>
    /// Caster characters have a 50% chance to dodge attacks.
    /// If they are hit by an attack they miss out on their next attack.
    /// </remarks>
    public abstract class Caster : Character, ICaster
    {

    }
}
