using DevKit.Entities.API;
using DevKit.Entities.Demo.Battle.API;

namespace DevKit.Entities.Demo.Battle
{
    // TODO implement battle rule by using entity config, based on battle state\round and etc
    /// <summary>
    /// Battle Rule applied for entities\characters before\during\after battle
    /// </summary>
    public class BattleRule : IBattleRule
    {
        // TODO can be replaced with specific IBattleConfig
        private IEngineConfig _config;

        /// <inheritdoc />
        public void Apply<T>(T entity, IBattleState state)
            where T : class, IEntity<T>
        {
            // TODO change property valued of given entity\character according to battle state and config
            //state.CurrentRound.RoundCount
            //entity.ApplyPropertyModifier()
        }

        /// <inheritdoc />
        public void Init(IEngineConfig config)
        {
            _config = config;
        }
    }
}
