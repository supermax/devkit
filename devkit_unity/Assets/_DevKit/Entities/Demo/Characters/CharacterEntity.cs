using System;
using DevKit.Entities.API;
using DevKit.Entities.Demo.Characters.API;
using DevKit.Serialization.Json.API;

namespace DevKit.Entities.Demo.Characters
{
    /// <summary>
    /// Base class for all game characters
    /// </summary>
    /// <remarks>
    /// Extends <see cref="Entity{T}"/> by adding required properties and functions for battle and etc
    /// </remarks>
    [Serializable]
    [JsonDataContract]
    public abstract class CharacterEntity<T>
        : Entity<T>
            , ICharacterEntity<T>
        where T: class
    {
        /// <inheritdoc />
        [JsonDataMember("id")]
        public override string Id
        {
            get { return IdValue; }
            set
            {
                IdValue = value;
                SetPropertyValue(value);
            }
        }

        /// <inheritdoc />
        [JsonDataMember("typeId")]
        public override string TypeId
        {
            get { return TypeIdValue; }
            set
            {
                TypeIdValue = value;
                SetPropertyValue(value);
            }
        }

        /// <inheritdoc />
        [JsonDataMemberIgnore]
        public override IEntityConfig Config { get; protected set; }

        /// <inheritdoc />
        [JsonDataMemberIgnore]
        public override string Error { get; protected set; }

        /// <inheritdoc />
        [JsonDataMember("properties")]
        public override EntityPropertiesContainer PropertyValues { get; } = new ();

        /// <inheritdoc />
        [JsonDataMember("health")]
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
        [JsonDataMember("damage")]
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
        [JsonDataMember("targetable")]
        public virtual bool? IsTargetable
        {
            get
            {
                var value = GetPropertyValue();
                return value.Bool;
            }
            set
            {
                SetPropertyValue(value);
            }
        }

        /// <inheritdoc />
        [JsonDataMember("canAttack")]
        public virtual bool? CanAttack
        {
            get
            {
                var value = GetPropertyValue();
                return value.Bool;
            }
            set
            {
                SetPropertyValue(value);
            }
        }

        public static string GetIsTargetableByKey<TT>()
        {
            var key = $"{nameof(IsTargetableBy)}_{typeof(TT).Name}";
            return key;
        }

        /// <inheritdoc />
        public virtual bool? IsTargetableBy<TT>(TT entity) where TT : class, IEntity<TT>
        {
            var isTargetable = Config.GetPropertyInitialValue(GetIsTargetableByKey<TT>()).Bool;
            return isTargetable;
        }

        public static string GetCanAttackTargetKey<TT>()
        {
            var key = $"{nameof(CanAttackTarget)}_{typeof(TT).Name}";
            return key;
        }

        /// <inheritdoc />
        public virtual bool? CanAttackTarget<TT>(TT entity) where TT : class, IEntity<TT>
        {
            var isTargetable = Config.GetPropertyInitialValue(GetCanAttackTargetKey<TT>()).Bool;
            return isTargetable;
        }

        public override void Init(IEntityConfig config)
        {
            BeginUpdate();
            Config = config;
            TypeId = config.GetPropertyInitialValue(nameof(TypeId)).Text;
            Damage = config.GetPropertyInitialValue(nameof(Damage)).Number;
            Health = config.GetPropertyInitialValue(nameof(Health)).Number;
            CanAttack = config.GetPropertyInitialValue(nameof(CanAttack)).Bool;
            IsTargetable = config.GetPropertyInitialValue(nameof(IsTargetable)).Bool;
            EndUpdate();
        }

        public CharacterEntity()
        {

        }

        public CharacterEntity(string id)
        {
            Id = id;
        }
    }
}
