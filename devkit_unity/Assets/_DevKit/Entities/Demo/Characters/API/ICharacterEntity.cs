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
        /// <summary>
        /// Returns <code>true</code> in case this character can be attacked by other entity\character
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TT"></typeparam>
        /// <returns></returns>
        bool? IsTargetableBy<TT>(TT entity) where TT : class, IEntity<TT>;

        /// <summary>
        /// Returns <code>true</code> in case this character can attack the given entity\character
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TT"></typeparam>
        /// <returns></returns>
        bool? CanAttackTarget<TT>(TT entity) where TT : class, IEntity<TT>;
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
