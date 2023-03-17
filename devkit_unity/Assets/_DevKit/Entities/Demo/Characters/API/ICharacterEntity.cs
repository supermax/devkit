using DevKit.Entities.API;

namespace DevKit.Entities.Demo.Characters.API
{
    /// <summary>
    /// Interface for base character class
    /// </summary>
    /// <remarks>
    /// Extends basic <see cref="IEntity{T}"/>
    /// </remarks>
    public interface ICharacterEntity : IEntity<ICharacterEntity>
    {
        /// <summary>
        /// Character config section
        /// </summary>
        IEntityConfig Config { get; set; }

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

        /// <summary>
        /// Returns <code>true</code> in case this character can be attacked by other entity\character
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool IsTargetableBy<T>(T entity) where T : class, IEntity<T>;

        /// <summary>
        /// Returns <code>true</code> in case this character can attack the given entity\character
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool CanAttackTarget<T>(T entity) where T : class, IEntity<T>;
    }
}
