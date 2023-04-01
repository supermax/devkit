using DevKit.Entities.API;

namespace DevKit.Entities.Demo.Characters.Extensions
{
    public static class CharacterEntitiesExtensions
    {
        public static string GetIsTargetableByKey<T>(this T entity)
        {
            var key = $"{nameof(IsTargetableBy)}_{typeof(T).Name}";
            return key;
        }

        /// <summary>
        /// Returns <code>true</code> in case this character can be attacked by other entity\character
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TT"></typeparam>
        /// <returns></returns>
        public static bool? IsTargetableBy<TT>(TT entity) where TT : class, IEntity<TT>
        {
            var isTargetable = entity.Config.GetPropertyInitialValue(entity.GetIsTargetableByKey()).Bool;
            return isTargetable;
        }

        public static string GetCanAttackTargetKey<T>(this T entity)
        {
            var key = $"{nameof(CanAttackTarget)}_{typeof(T).Name}";
            return key;
        }

        /// <summary>
        /// Returns <code>true</code> in case this character can attack the given entity\character
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TT"></typeparam>
        /// <returns></returns>
        public static bool? CanAttackTarget<TT>(TT entity) where TT : class, IEntity<TT>
        {
            var isTargetable = entity.Config.GetPropertyInitialValue(entity.GetCanAttackTargetKey()).Bool;
            return isTargetable;
        }
    }
}
