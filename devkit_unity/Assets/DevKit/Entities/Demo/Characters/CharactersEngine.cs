using System;
using DevKit.Entities.Demo.Characters.API;

namespace DevKit.Entities.Demo.Characters
{
    /// <summary>
    /// Characters engine, used to instanciate character and to inject relevant config
    /// </summary>
    /// <remarks>
    /// Extends <see cref="Engine"/> for entities
    /// </remarks>
    public class CharactersEngine : Engine, ICharactersEngine
    {
        protected virtual ICharactersConfig GetConfig()
        {
            return Config as ICharactersConfig;
        }

        /// <summary>
        /// Creates instance of new character
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>new instance of <para>T</para> with injected config</returns>
        public override T Create<T>()
        {
            var entity = Activator.CreateInstance<T>();

            var character = entity as ICharacter;
            var characterConfig = GetConfig();
            if (character != null && characterConfig != null)
            {
                character.Config = characterConfig;
            }

            entity.Init();
            return entity;
        }
    }
}
