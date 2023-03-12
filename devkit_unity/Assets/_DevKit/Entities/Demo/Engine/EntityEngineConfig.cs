using DevKit.Entities.Demo.Characters.API;

// TODO do we need special config for the characters or we can use base engine config? (or we can make it more generic `EntityEngine`?)
namespace DevKit.Entities.Demo.Characters
{
    /// <summary>
    /// Characters config class, used to determine basic character values and behavior
    /// </summary>
    /// <remarks>
    /// Extends <see cref="EngineConfig"/>
    /// </remarks>
    public class EntityEngineConfig : EngineConfig, IEntityEngineConfig
    {
        public override void Init()
        {
            // TODO
        }
    }
}
