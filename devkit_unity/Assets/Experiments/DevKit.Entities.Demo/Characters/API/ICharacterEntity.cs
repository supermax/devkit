using DevKit.Entities.API;

namespace DevKit.Entities.Demo.Characters.API
{
    /// <summary>
    /// Interface for base character class
    /// </summary>
    /// <remarks>
    /// Extends basic <see cref="IEntity{T}"/>
    /// </remarks>
    public interface ICharacterEntity<T>
        : ICharacterEntity
            , IEntity<T>
        where T : class
    {
    }

    public interface ICharacterEntity
    {
        /// <summary>
        /// Character's Health Value
        /// </summary>
        double? Health { get; set; }

        /// <summary>
        /// Character's Damage Value
        /// </summary>
        double? Damage { get; set; }

        /// <summary>
        /// Indicates if this character can be attacked
        /// </summary>
        bool? IsTargetable { get; set; }

        /// <summary>
        /// Indicates if this character can attack
        /// </summary>
        bool? CanAttack { get; set; }
    }
}
