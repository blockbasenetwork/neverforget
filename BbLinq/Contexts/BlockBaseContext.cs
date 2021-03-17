using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlockBase.BBLinq.Contexts.Base;
using BlockBase.BBLinq.Executors;
using BlockBase.BBLinq.Properties;
using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.Queries.BlockBaseQueries;
using BlockBase.BBLinq.Sets;
using BlockBase.BBLinq.Settings;

namespace BlockBase.BBLinq.Contexts
{
    /// <summary>
    /// Inherit this context 
    /// </summary>
    public abstract class BlockBaseContext : DatabaseContext<BlockBaseSettings>
    {

        /// <summary>
        /// Constructor by settings wrapper
        /// </summary>
        protected BlockBaseContext(BlockBaseSettings settings) : base(settings)
        {
        }

        /// <summary>
        /// Returns a set based on the type specified
        /// </summary>
        public new BlockBaseSet<T> Set<T>() where T : class
        {
            return base.Set<T>() as BlockBaseSet<T>;
        }

        /// <summary>
        /// Creates the database
        /// </summary>
        public async Task CreateDatabase()
        {
            var settings = ContextCache.Instance.Get<BlockBaseSettings>(Resources.CACHE_SETTINGS);

            var sets = GetType().GetProperties();
            var types = new List<Type>();
            foreach (var set in sets)
            {
                if ((set.PropertyType.GetInterface("ISet") == null)) continue;
                var type = set.PropertyType.GetGenericArguments()[0];
                types.Add(type);
            }

            var query = new CreateDatabaseQuery(settings.DatabaseName, types.ToArray());
            var executor = new BlockBaseQueryExecutor {UseDatabase = false};
            await executor.ExecuteQueryAsync(query);
        }

        public async Task DropDatabaseIfExists()
        {
            try
            {
                await DropDatabase();
            }
            catch(Exception)
            {
                // ignored
            }
        }

        public async Task CreateDatabaseIfNotExists()
        {
            try
            {
                await CreateDatabase();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public async Task DropDatabase()
        {
            var settings = ContextCache.Instance.Get<BlockBaseSettings>(Resources.CACHE_SETTINGS);
            var query = new BlockBaseDropDatabaseQuery(settings.DatabaseName);
            var executor = new BlockBaseQueryExecutor {UseDatabase = false};
            await executor.ExecuteQueryAsync(query);
        }

        public async Task ExecuteBatch()
        {
            var batch = ContextCache.Instance.Get<List<IQuery>>(Resources.CACHE_QUERIES);
            var executor = new BlockBaseQueryExecutor();
            foreach (var batchItem in batch)
            {
                await executor.ExecuteQueryAsync(batchItem);
            }
        }
    }
}
