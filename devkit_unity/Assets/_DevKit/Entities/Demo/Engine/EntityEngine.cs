using System;
using System.Collections.Generic;
using DevKit.Entities.Demo.Characters.API;

// TODO do we need specific `CharactersEngine` or we can make it more generic `EntityEngine`
namespace DevKit.Entities.Demo.Characters
{
    /// <summary>
    /// Characters engine, used to instantiate character and to inject relevant config
    /// </summary>
    /// <remarks>
    /// Extends <see cref="Engine"/> for entities
    /// </remarks>
    public class EntityEngine : Engine, IEntityEngine
    {
        private readonly IDictionary<Type, Type> _typeMappings = new Dictionary<Type, Type>();

        protected virtual IEntityEngineConfig GetConfig()
        {
            return Config as IEntityEngineConfig;
        }

        /// <summary>
        /// Creates instance of new character
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>new instance of <para>T</para> with injected config</returns>
        public override T Create<T>()
        {
            var keyType = typeof (T);
            var mappedType = _typeMappings.ContainsKey(keyType) ? _typeMappings[keyType] : keyType;

            var entity = (T)Activator.CreateInstance(mappedType);
            var character = entity as ICharacter;
            var characterConfig = GetConfig();
            if (character != null && characterConfig != null)
            {
                character.Config = characterConfig.GetEntityConfig<T>();
            }

            entity.Init();
            return entity;
        }

        public override TInterface Register<TInterface, TImplementation>()
        {
            // TODO use _typeMappings
            throw new NotImplementedException();
        }
    }
}
