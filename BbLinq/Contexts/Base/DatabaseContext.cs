using System;
using System.Collections.Generic;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Properties;
using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.Sets.Base;
using BlockBase.BBLinq.Settings.Base;

namespace BlockBase.BBLinq.Contexts.Base
{
    public abstract class DatabaseContext<TSettings> : IDisposable where TSettings : DbSettings
    {
        private readonly ContextCache _cache = ContextCache.Instance;
        private static ContextCache Cache => ContextCache.Instance;

        protected DatabaseContext(TSettings settings)
        {
            Cache.Set(Resources.CACHE_SETTINGS, settings);
            Cache.Set(Resources.CACHE_QUERIES, new List<IQuery>());
            InstantiateSets();
        }

        protected ISet Set<T>() where T : class
        {
            var properties = GetType().GetProperties();

            foreach (var prop in properties)
            {
                var interfaces = prop.PropertyType.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (@interface != typeof(ISet))
                    {
                        continue;
                    }
                    var genericArguments = prop.PropertyType.GetGenericArguments();
                    if (genericArguments.Length > 0 && genericArguments[0] == typeof(T))
                    {
                        return (ISet)prop.GetValue(this);
                    }
                }
            }
            throw new NoSetAvailableException(typeof(T).Name);
        }

        /// <summary>
        /// Sets a default value for each set on the context
        /// </summary>
        private void InstantiateSets()
        {
            var properties = GetType().GetProperties();

            foreach (var prop in properties)
            {
                var interfaces = prop.PropertyType.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (@interface == typeof(ISet))
                    {
                        prop.SetValue(this, Activator.CreateInstance(prop.PropertyType));
                    }
                }
            }
        }

        /// <summary>
        /// Clears the context so that the executor is only available when needed
        /// </summary>
        public void Dispose()
        {
            _cache.Clear();
        }
    }
}
