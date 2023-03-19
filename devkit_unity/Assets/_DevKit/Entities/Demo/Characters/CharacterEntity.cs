using System;
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
    [Serializable]
    public abstract class CharacterEntity<T>
        : Entity<T>
            , ICharacterEntity<T>
        where T: class
    {
        /// <inheritdoc />
        public virtual double? Health
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
        public virtual bool IsTargetableBy<TT>(TT entity) where TT : class, IEntity<TT>
        {
            var isTargetable = Config.GetPropertyInitialValue($"{nameof(IsTargetableBy)}_{entity.TypeId}").Bool.GetValueOrDefault();
            return isTargetable;
        }

        /// <inheritdoc />
        public virtual bool CanAttackTarget<TT>(TT entity) where TT : class, IEntity<TT>
        {
            var isTargetable = Config.GetPropertyInitialValue($"{nameof(CanAttackTarget)}_{entity.TypeId}").Bool.GetValueOrDefault();
            return isTargetable;
        }

        public override void Init(IEntityConfig config)
        {
            BeginUpdate();
            Config = config;
            TypeId = config.GetPropertyInitialValue(nameof(TypeId)).Text;
            Damage = config.GetPropertyInitialValue(nameof(Damage)).Number;
            var damage = Damage;

            Health = config.GetPropertyInitialValue(nameof(Health)).Number;
            CanAttack = config.GetPropertyInitialValue(nameof(CanAttack)).Bool;
            IsTargetable = config.GetPropertyInitialValue(nameof(IsTargetable)).Bool;
            EndUpdate();
        }
    }
}
