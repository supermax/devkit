using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DevKit.DIoC.Container
{
    public interface ITypeMap<in T>
    {
        ITypeMap<T> To<TM>(string key = null) where TM : class, T;

        ITypeMap<T> Singleton<TM>(string key = null) where TM : class, T;

        ITypeMap<T> Singleton<TM>([NotNull] TM instance, string key = null) where TM : class, T;
    }

    internal interface ITypeMap : IDisposable
    {
        IDictionary<string, TypeMapConfig> GetTypeMapConfigs();

        IDictionary<string, object> GetInstances();
    }
}
