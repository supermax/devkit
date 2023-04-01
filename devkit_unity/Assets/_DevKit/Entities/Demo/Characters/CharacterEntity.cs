using System;
using DevKit.Entities.API;
using DevKit.Entities.Demo.Characters.API;
using DevKit.Entities.Extensions;
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
        [JsonDataMemberIgnore]
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
        [JsonDataMemberIgnore]
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
        [JsonDataMemberIgnore]
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
        [JsonDataMemberIgnore]
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
        [JsonDataMemberIgnore]
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
        [JsonDataMemberIgnore]
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

        public override void Init()
        {
            base.Init();
            Id = this.GetId();
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
