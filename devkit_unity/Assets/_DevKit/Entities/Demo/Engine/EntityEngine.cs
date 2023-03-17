using System;
using System.Collections.Generic;
using DevKit.Entities.Demo.Characters.API;

namespace DevKit.Entities.Demo.Engine
{
    /// <summary>
    /// Characters engine, used to instantiate character and to inject relevant config
    /// </summary>
    /// <remarks>
    /// Extends <see cref="Engine"/> for entities
    /// </remarks>
    public class EntityEngine : Entities.Engine, IEntityEngine
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
            var entity = Instantiate<T>();
            var character = entity as ICharacterEntity;
            var characterConfig = GetConfig();
            if (character != null && characterConfig != null)
            {
                character.Config = characterConfig.GetEntityConfig<T>();
            }

            entity.Init();
            return entity;
        }

        protected override T Instantiate<T>()
        {
            var keyType = typeof (T);
            var mappedType = _typeMappings.ContainsKey(keyType) ? _typeMappings[keyType] : keyType;
            var objInstance = Activator.CreateInstance(mappedType);
            var entity = objInstance as T;
            return entity;
        }

        public override void Register<TInterface, TImplementation>()
        {
            _typeMappings[typeof (TInterface)] = typeof (TImplementation);
        }
    }
}
