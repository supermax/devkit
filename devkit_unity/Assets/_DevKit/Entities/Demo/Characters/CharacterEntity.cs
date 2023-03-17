using System;
using System.Runtime.Serialization;
using DevKit.Entities.API;
using DevKit.Entities.Demo.Characters.API;

namespace DevKit.Entities.Demo.Characters
{
    /// <summary>
    /// Base class for all game characters
    /// </summary>
    /// <remarks>
    /// Extends <see cref="Entity{T}"/> by adding required properties and functions for battle and etc
    /// </remarks>
    [DataContract]
    [Serializable]
    public abstract class CharacterEntity : Entity<ICharacterEntity>, ICharacterEntity
    {
        public IEntityConfig Config { get; set; }

        /// <inheritdoc />
        public virtual double? Health
        {
            get
            {
                var value = GetPropertyValue(nameof(Health));
                return value.Number;
            }
            set
            {
                SetPropertyValue(value);
            }
        }

        /// <inheritdoc />
        public virtual double? Damage
        {
            get
            {
                var value = GetPropertyValue();
                return value.Number;
            }
            set
            {
                SetPropertyValue(value);
            }
        }

        /// <inheritdoc />
        public virtual bool? IsTargetable
        {
            get
            {
                var value = GetPropertyValue();
                return value.Bool.GetValueOrDefault();
            }
            set
            {
                SetPropertyValue(value);
            }
        }

        /// <inheritdoc />
        public virtual bool? CanAttack
        {
            get
            {
                var value = GetPropertyValue();
                return value.Bool.GetValueOrDefault();
            }
            set
            {
                SetPropertyValue(value);
            }
        }

        /// <inheritdoc />
        public virtual bool IsTargetableBy<T>(T entity) where T : class, IEntity<T>
        {
            // TODO take relevant values from character config
            throw new NotImplementedException();
        }

        public virtual bool CanAttackTarget<T>(T entity) where T : class, IEntity<T>
        {
            // TODO take relevant values from character config
            throw new NotImplementedException();
        }

        public override void Init()
        {
            base.Init();

            Damage = Config.GetPropertyInitialValue(nameof(Damage)).Number;
            Health = Config.GetPropertyInitialValue(nameof(Health)).Number;
            CanAttack = Config.GetPropertyInitialValue(nameof(CanAttack)).Bool;
            IsTargetable = Config.GetPropertyInitialValue(nameof(IsTargetable)).Bool;
        }
    }
}
