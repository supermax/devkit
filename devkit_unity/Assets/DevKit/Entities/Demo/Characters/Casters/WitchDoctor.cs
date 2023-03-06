using DevKit.Entities.Demo.Characters.Casters.API;

namespace DevKit.Entities.Demo.Characters.Casters
{
    /// <summary>
    /// Witch Doctor character
    /// </summary>
    /// <remarks>
    /// Extends <see cref="Caster"/>
    /// Witch Doctors self-heal for 2 health at the end of each round.
    /// </remarks>
    public class WitchDoctor : Caster, IWitchDoctor
    {

    }
}
