using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Executors;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Database;
using BlockBase.BBLinq.Model.Nodes;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.Queries.BlockBaseQueries;
using BlockBase.BBLinq.Sets.Base;

namespace BlockBase.BBLinq.Sets
{
    public class BlockBaseSet<T> : BlockBaseBaseSet<BlockBaseSet<T>>, IBlockBaseSet<T>
    {
        private Expression<Func<T, bool>> _predicate;
        private int? _recordsToSkip;
        private int? _recordsToTake;
        private bool _encryptQuery;

        #region Insert

        public IQuery GetInsertQuery(T record)
        {
            return new BlockBaseRecordInsertQuery(typeof(T), record, _encryptQuery);
        }

        public void BatchInsert(T record)
        {
            var query = GetInsertQuery(record);
            StoreQueryInBatch(query);
        }

        public async Task InsertAsync(T record)
        {
            var query = GetInsertQuery(record);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            await executor.ExecuteQueryAsync(query);
        }

        public IQuery GetInsertQuery(IEnumerable<T> records)
        {
            return new BlockBaseRecordInsertQuery(typeof(T), records, _encryptQuery);
        }

        public void BatchInsert(IEnumerable<T> records)
        {
            var query = GetInsertQuery(records);
            StoreQueryInBatch(query);
        }

        public async Task InsertAsync(IEnumerable<T> records)
        {
            var query = GetInsertQuery(records);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            await executor.ExecuteQueryAsync(query);
        }
        #endregion

        #region Addons
        public IQueryableSet<T> Where(Expression<Func<T, bool>> predicate)
        {
            _predicate = predicate;
            return this;
        }

        public IQueryableSet<T> Skip(int skipNumber)
        {
            _recordsToSkip = skipNumber;
            return this;
        }

        public IQueryableSet<T> Take(int takeNumber)
        {
            _recordsToTake = takeNumber;
            return this;
        }


        #endregion

        #region Delete
        public IQuery GetDeleteQuery()
        {
            var condition = BlockBaseExpressionParser.Parse(_predicate);
            return new BlockBaseRecordDeleteQuery(typeof(T).GetTableName(), condition, _encryptQuery);
        }

        public void BatchDelete()
        {
            var query = GetDeleteQuery();
            StoreQueryInBatch(query);
        }

        public async Task DeleteAsync()
        {
            var query = GetDeleteQuery();
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            await executor.ExecuteQueryAsync(query);
        }

        public IQuery GetDeleteQuery(T record)
        {
            return new BlockBaseRecordDeleteQuery(typeof(T).GetTableName(), NodeBuilder.GenerateComparisonNodeOnKey(record), _encryptQuery);
        }

        public void BatchDelete(T record)
        {
            var query = GetDeleteQuery(record);
            StoreQueryInBatch(query);
        }

        public async Task DeleteAsync(T record)
        {
            var query = GetDeleteQuery(record);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            await executor.ExecuteQueryAsync(query);
        }
        #endregion

        #region Update
        public IQuery GetUpdateQuery(T record)
        {
            var condition = _predicate != null ?
                BlockBaseExpressionParser.Parse(_predicate) :
                NodeBuilder.GenerateComparisonNodeOnKey(record);
            return new BlockBaseRecordUpdateQuery(typeof(T), record, condition, _encryptQuery);
        }

        public void BatchUpdate(T record)
        {
            var query = GetUpdateQuery(record);
            StoreQueryInBatch(query);
        }

        public async Task UpdateAsync(T record)
        {
            var query = GetUpdateQuery(record);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            await executor.ExecuteQueryAsync(query);
        }

        public IQuery GetUpdateQuery(object record)
        {
            var condition = _predicate != null ? BlockBaseExpressionParser.Parse(_predicate) : NodeBuilder.GenerateComparisonNodeOnObjectKey(typeof(T), record);
            return new BlockBaseRecordUpdateQuery(typeof(T), record, condition, _encryptQuery);
        }

        public void BatchUpdate(object record)
        {
            var query = GetUpdateQuery(record);
            StoreQueryInBatch(query);
        }

        public async Task UpdateAsync(object record)
        {
            var query = GetUpdateQuery(record);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            await executor.ExecuteQueryAsync(query);
        }


        #endregion

        #region Select
        public ISelectQuery GetSelectQuery()
        {
            var selectedProperties = new[] { new BlockBaseColumn() { Table = typeof(T).GetTableName() } };
            var condition = BlockBaseExpressionParser.Parse(_predicate);
            var joins = new[] { new JoinNode(typeof(T).GetPrimaryKey(), null) };
            return new BlockBaseRecordSelectQuery(typeof(T), null, selectedProperties, joins, condition, _recordsToTake, _recordsToSkip,
                _encryptQuery);
        }

        public void BatchSelect()
        {
            var query = GetSelectQuery();
            StoreQueryInBatch(query);
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            var query = GetSelectQuery();
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            var res = await executor.ExecuteQueryAsync<T>(query);
            return res;
        }

        #endregion

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<T, TRecordResult>> mapper)
        {
            var condition = BlockBaseExpressionParser.Parse(_predicate);
            var joins = new[] { new JoinNode(typeof(T).GetPrimaryKey(), null) };
            var selectedProperties = BlockBaseExpressionParser.ParseSelectionColumns(mapper);
            var query = new BlockBaseRecordSelectQuery(typeof(TRecordResult), mapper, selectedProperties, joins,
                condition, _recordsToTake, _recordsToSkip, _encryptQuery);
            return query;
        }

        public void BatchSelect<TRecordResult>(Expression<Func<T, TRecordResult>> mapper)
        {
            var query = GetSelectQuery(mapper);
            StoreQueryInBatch(query);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<T, TRecordResult>> mapper)
        {
            var query = GetSelectQuery(mapper);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            var res = await executor.ExecuteQueryAsync<TRecordResult>(query);
            return res;
        }

        #region Join

        public IJoin<T, TB> Join<TB>()
        {
            return new BlockBaseJoin<T, TB>();
        }

        #endregion

        #region Encrypt
        IFetchableSet<T> IBlockBaseBaseSet<IFetchableSet<T>>.Encrypt()
        {
            _encryptQuery = true;
            return this;
        }
        IInsertableSet<T> IBlockBaseBaseSet<IInsertableSet<T>>.Encrypt()
        {
            _encryptQuery = true;
            return this;
        }
        IQueryableSet<T> IBlockBaseBaseSet<IQueryableSet<T>>.Encrypt()
        {
            _encryptQuery = true;
            return this;
        }
        IBlockBaseSet<T> IBlockBaseBaseSet<IBlockBaseSet<T>>.Encrypt()
        {
            _encryptQuery = true;
            return this;
        }
        #endregion

        public ISelectQuery GetGetQuery(object id)
        {
            var condition = NodeBuilder.GenerateComparisonNodeOnKey(typeof(T), id);
            var selectedProperties = new[] { new BlockBaseColumn() { Table = typeof(T).GetTableName() } };
            var joins = new[] { new JoinNode(typeof(T).GetPrimaryKey(), null) };
            var query = new BlockBaseRecordSelectQuery(typeof(T), null, selectedProperties, joins,
                condition, 1, 0, _encryptQuery);
            return query;
        }

        public void BatchGet(object id)
        {
            var query = GetGetQuery(id);
            StoreQueryInBatch(query);
        }

        public async Task<T> GetAsync(object id)
        {
            var query = GetGetQuery(id);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            var res = (await executor.ExecuteQueryAsync<T>(query)).ToArray();
            return res.Length == 0 ? default : res[0];
        }
    }
}
