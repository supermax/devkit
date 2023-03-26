using System;

namespace DevKit.Entities.Extensions
{
    public static class EntityExtensions
    {
        public static string GetId<TT>(this TT entity)
        {
            var key = $"[{typeof(TT).Name}]-[{Guid.NewGuid().ToString()}]";
            return key;
        }

        public static string GetTypeId<TT>(this TT entity)
        {
            var key = typeof(TT).Name;
            return key;
        }
    }
}
