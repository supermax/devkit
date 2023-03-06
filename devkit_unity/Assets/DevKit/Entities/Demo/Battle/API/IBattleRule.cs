using DevKit.Entities.API;

namespace DevKit.Entities.Demo.Battle.API
{
    /// <summary>
    /// Interface for Battle Rule class
    /// </summary>
    public interface IBattleRule
    {
        /// <summary>
        /// Applies property and\or behavioral rules according to current battle state
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="state"></param>
        /// <typeparam name="T"></typeparam>
        void Apply<T>(T entity, IBattleState state) where T : class, IEntity<T>;

        /// <summary>
        /// Initializes this rule with config
        /// </summary>
        /// <param name="config"></param>
        void Init(IEngineConfig config);
    }
}
