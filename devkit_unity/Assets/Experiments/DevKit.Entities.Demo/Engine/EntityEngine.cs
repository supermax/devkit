using System;
using System.Collections.Generic;
using DevKit.Core.Observables.API;
using DevKit.Entities.API;
using DevKit.PubSub;
using IEntityEngine = DevKit.Entities.Demo.Engine.API.IEntityEngine;

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
        // TODO replace with Maestro container
        private readonly IDictionary<Type, Type> _typeMappings = new Dictionary<Type, Type>();

        /// <summary>
        /// Creates instance of new character
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>new instance of <para>T</para> with injected config</returns>
        public override T GetInstance<T>()
        {
            var entity = Instantiate<T>();
            if (entity == null)
            {
                return null;
            }

            entity.Init();
            if (Config != null)
            {
                var entityConfig = Config.GetEntityConfig<T>();
                entity.Init(entityConfig);
            }

            entity.PropertyChanged += OnEntityPropertyChanged;
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

        public override void Register<TInterface, TImplementation>(bool singleton)
        {
            _typeMappings[typeof (TInterface)] = typeof (TImplementation);
        }

        private static void OnEntityPropertyChanged<T>(IObservableObject<T> sender, PropertyChangedEventArgs<T> args) where T : class, IEntity<T>
        {
            Messenger.Default.Publish(args);
        }
    }
}
