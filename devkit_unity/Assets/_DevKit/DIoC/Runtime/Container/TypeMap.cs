using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using DevKit.Core.Extensions;
using DevKit.DIoC.Attributes;
using DevKit.DIoC.Extensions;
using DevKit.Logging.Extensions;

namespace DevKit.DIoC.Container
{
    // TODO add mapType and instance fields for single-mapping and use dic for multi-mapping
    // TODO dispose upon removal from cache
    // TODO refer to TypeMapAttr and to InitTrigger during mapping
    // TODO handle default keys (add/remove/update)
    internal class TypeMap<TInterface>
        : ITypeMap
        , ITypeMap<TInterface>
        , ITypeMapResolver<TInterface>
        , ITypeMapReset<TInterface>
        where TInterface : class
    {
        private IDictionary<string, TypeMapConfig> _mapTypes;

        private IDictionary<string, TInterface> _instances;

        private List<Type> _dependencies;

        private TypeMapCache _cache;

        internal TypeMap(TypeMapCache cache)
        {
            _cache = cache;
            _mapTypes = new ConcurrentDictionary<string, TypeMapConfig>();
            _instances = new ConcurrentDictionary<string, TInterface>();
        }

        private ITypeMap<TInterface> MapType<TImplementation>(string key = null, bool isSingleton = false) where TImplementation : class, TInterface
        {
            var type = typeof(TImplementation);
            if (!type.IsClass)
            {
                throw new OperationCanceledException($"The type {type} is not a class!");
            }
            if (type == GetType())
            {
                throw new InvalidOperationException($"The type {type} cannot be {GetType()}!");
            }

            var mapAtt = type.GetCustomAttribute<TypeMapAttribute>();
            if (mapAtt is {IsSingleton: true})
            {
                isSingleton = true;
                //mapAtt.MapTypes // TODO map subtypes
                //mapAtt.InitTrigger // TODO refer to trigger
            }

            var depAtt = type.GetCustomAttribute<DependencyAttribute>();
            if (depAtt != null && !depAtt.Dependencies.IsNullOrEmpty())
            {
                foreach (var dependency in depAtt.Dependencies)
                {
                    // TODO map type
                    // TODO check for circular dependency
                    //Resolve(dependency, args, dependencies);
                    //_cache.
                }
            }

            var config = new TypeMapConfig {Type = type, IsSingleton = isSingleton};
            if (key != null)
            {
                _mapTypes[key] = config;
            }
            else
            {
                _mapTypes[typeof (TInterface).FullName] = config;
                _mapTypes[typeof (TImplementation).FullName] = config;
            }
            return this;
        }

        public ITypeMap<TInterface> To<TImplementation>(string key = null) where TImplementation : class, TInterface
        {
            return MapType<TImplementation>(key);
        }

        public ITypeMapReset<TInterface> From<TImplementation>(string key = null) where TImplementation : class, TInterface
        {
            var type = typeof(TImplementation);
            if (!type.IsClass)
            {
                throw new OperationCanceledException($"The type {type} is not a class!");
            }
            if (type == GetType())
            {
                throw new InvalidOperationException($"The type {type} cannot be {GetType()}!");
            }

            if (key != null)
            {
                _mapTypes.Remove(key);
            }
            else
            {
                _mapTypes.Remove(typeof (TInterface).FullName);
                _mapTypes.Remove(typeof (TImplementation).FullName);
            }
            return this;
        }

        public ITypeMapReset<TInterface> From<TImplementation>([NotNull] TImplementation instance, string key = null) where TImplementation : class, TInterface
        {
            return From<TImplementation>(key);
        }

        public ITypeMap<TInterface> Singleton<TImplementation>(string key = null) where TImplementation : class, TInterface
        {
            return MapType<TImplementation>(key, true);
        }

        public ITypeMap<TInterface> Singleton<TImplementation>([NotNull] TImplementation instance, string key = null) where TImplementation : class, TInterface
        {
            var type = typeof(TImplementation);
            if (!type.IsClass)
            {
                throw new OperationCanceledException($"The type {type} is not a class!");
            }
            if (type == GetType())
            {
                throw new InvalidOperationException($"The type {type} cannot be {GetType()}!");
            }

            if (key != null)
            {
                _instances[key] = instance;
            }
            else
            {
                _instances[typeof (TInterface).FullName] = instance;
                _instances[typeof (TImplementation).FullName] = instance;
            }
            return Singleton<TImplementation>(key);
        }

        public TInterface Instance(string key = null, params object[] args)
        {
            TInterface instance;
            key ??= typeof(TInterface).FullName;
            if (_instances.ContainsKey(key))
            {
                instance = _instances[key];
                return instance;
            }

            if (!_mapTypes.ContainsKey(key))
            {
                this.LogWarning($"Cannot find mapped type for {key}");
                return default;
            }

            var type = _mapTypes[key].Type;
            instance = (TInterface)Resolve(type, args);
            if (!_mapTypes[key].IsSingleton)
            {
                return instance;
            }

            _instances[key] = instance;
            return instance;
        }

        public TInterface Inject([NotNull] TInterface instance, params object[] args)
        {
            InjectProperties(instance.GetType(), args, instance);
            return instance;
        }

        public void Dispose()
        {
            _cache = null;

            _mapTypes?.Clear();
            _mapTypes = null;

            _instances?.Clear();
            _instances = null;
        }

        public override string ToString()
        {
            return $"{nameof(TypeMap<TInterface>)}<{typeof(TInterface).FullName}>";
        }

        private object Resolve([NotNull] Type type, object[] args)
        {
            type.ThrowIfNull(nameof(type));
            object instance;
            if (!type.IsClass)
            {
                var typeMap = _cache.Get(type);
                if (typeMap == null)
                {
                    throw new OperationCanceledException($"The {type} is not a class!");
                }

                var key = type.FullName;
                var configs = typeMap.GetTypeMapConfigs();
                if (!configs.IsNullOrEmpty() && configs.ContainsKey(key))
                {
                    var typeConfig = configs[key];
                    type = typeConfig.Type;
                    key = type.FullName;

                    if (typeConfig.IsSingleton)
                    {
                        var instances = typeMap.GetInstances();
                        if (!instances.IsNullOrEmpty() && instances.TryGetValue(key, out instance))
                        {
                            return instance;
                        }
                    }
                }
            }
            type.ThrowIfNull(nameof(type));

            InitDependencies(type, args);
            instance = InitInstance(type, args);
            InjectProperties(type, args, instance);
            ExecuteMethods(type, args, instance);
            return instance;
        }

        private void ExecuteMethods(Type type, object[] args, object instance)
        {
            var methods = type.GetExecutableMethods();
            if (methods.IsNullOrEmpty())
            {
                return;
            }

            foreach (var method in methods)
            {
                var methodParams = method.GetParameters();
                if (!methodParams.IsNullOrEmpty())
                {
                    var methodParamsAry = new object[methodParams.Length];
                    for (var i = 0; i < methodParams.Length; i++)
                    {
                        var methodParam = methodParams[i];
                        var paramType = methodParam.ParameterType;
                        var paramInstance = Resolve(paramType, args);
                        methodParamsAry[i] = paramInstance;
                    }
                    method.Invoke(instance, methodParamsAry);
                }
                else
                {
                    method.Invoke(instance, null);
                }
            }
        }

        private void InjectProperties(Type type, object[] args, object instance)
        {
            var props = type.GetInjectableProperties();
            if (props.IsNullOrEmpty())
            {
                return;
            }

            foreach (var prop in props)
            {
                if (!prop.CanWrite)
                {
                    this.LogWarning($"Cannot inject value into READ-ONLY property.");
                    continue;
                }

                var propValue = Resolve(prop.PropertyType, args);
                prop.SetValue(instance, propValue);
            }
        }

        private object InitInstance(Type type, object[] args)
        {
            object instance;
            var ctor = type.GetDefaultConstructor();
            if (ctor == null)
            {
                throw new OperationCanceledException($"Cannot get constructor for {type}");
            }

            var ctorParams = ctor.GetParameters();
            if (ctorParams.IsNullOrEmpty())
            {
                instance = ctor.Invoke(null);
            }
            else
            {
                // TODO use args param
                var ctorParamValues = new object[ctorParams.Length];
                for (var i = 0; i < ctorParams.Length; i++)
                {
                    var ctorParam = ctorParams[i];
                    var ctorParamType = ctorParam.ParameterType;
                    var ctorParamValue = Resolve(ctorParamType, args);
                    ctorParamValues[i] = ctorParamValue;
                }
                instance = ctor.Invoke(ctorParamValues);
            }
            return instance;
        }

        private void InitDependencies([NotNull] MemberInfo src, object[] args)
        {
            var depAtt = src.GetCustomAttribute<DependencyAttribute>();
            if (depAtt == null || depAtt.Dependencies.IsNullOrEmpty())
            {
                return;
            }

            _dependencies ??= new List<Type>();
            foreach (var dependency in depAtt.Dependencies)
            {
                if (_dependencies.Contains(dependency))
                {
                    this.LogWarning($"Cyclic dependency {dependency}, skipping resolution");
                    continue;
                }

                _dependencies.Add(dependency);
                Resolve(dependency, args);
            }
        }

        #region ITypeMap

        IDictionary<string, TypeMapConfig> ITypeMap.GetTypeMapConfigs()
        {
            return _mapTypes;
        }

        IDictionary<string, object> ITypeMap.GetInstances()
        {
            var dic = _instances
                .ToDictionary<KeyValuePair<string, TInterface>, string, object>(pair => pair.Key, pair => pair.Value);
            return dic;
        }

        #endregion
    }
}
