using System;
using System.Collections.Generic;
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
    public abstract class BlockBaseJoin<TReturn> : BlockBaseBaseSet<BlockBaseJoin<TReturn>>
        where TReturn : BlockBaseJoin<TReturn>
    {
        protected int? RecordsToSkip;
        protected int? RecordsToTake;
        protected bool EncryptQuery;
        protected LambdaExpression Predicate;
        protected JoinNode[] Joins { get; set; }



        public ISelectQuery GetSelectQuery()
        {
            var selectedProperties = new List<BlockBaseColumn>();
            foreach (var type in GetType().GenericTypeArguments)
            {
                selectedProperties.Add(new BlockBaseColumn() {Table = type.GetTableName()});
            }

            var condition = BlockBaseExpressionParser.Parse(Predicate);
            return new BlockBaseRecordSelectQuery(null, null, selectedProperties.ToArray(), Joins, condition,
                RecordsToTake, RecordsToTake,
                EncryptQuery);
        }

        public void BatchSelect()
        {
            var query = GetSelectQuery();
            StoreQueryInBatch(query);
        }

        public async Task<IEnumerable<dynamic>> SelectAsync()
        {
            var query = GetSelectQuery();
            var executor = new BlockBaseQueryExecutor() {UseDatabase = true};
            var res = await executor.ExecuteQueryAsync<dynamic>(query);
            return res;
        }

        protected virtual ISelectQuery GetSelectQuery<TRecordResult>(LambdaExpression mapper)
        {
            var condition = BlockBaseExpressionParser.Parse(Predicate);
            var selectedProperties = BlockBaseExpressionParser.ParseSelectionColumns(mapper);
            var query = new BlockBaseRecordSelectQuery(typeof(TRecordResult), mapper, selectedProperties, Joins,
                condition, RecordsToTake, RecordsToSkip, EncryptQuery);
            return query;
        }

        protected void BatchSelect<TRecordResult>(LambdaExpression mapper)
        {
            var query = GetSelectQuery<TRecordResult>(mapper);
            StoreQueryInBatch(query);
        }

        protected async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(LambdaExpression mapper)
        {
            var query = GetSelectQuery<TRecordResult>(mapper);
            var executor = new BlockBaseQueryExecutor() {UseDatabase = true};
            var res = await executor.ExecuteQueryAsync<TRecordResult>(query);
            return res;
        }

        protected JoinNode[] GenerateJoins<TNext>()
        {
            var types = JoinBuilder.DrawTypesFromJoinNodes(Joins);
            var join = JoinBuilder.BuildJoin(types, typeof(TNext));
            var joins = new List<JoinNode>();
            joins.AddRange(Joins);
            joins.Add(join);
            return joins.ToArray();
        }
    }

    public class BlockBaseJoin<TA, TB> : BlockBaseJoin<BlockBaseJoin<TA, TB>>, IJoin<TA, TB>
    {
        public BlockBaseJoin()
        {
            Joins = new[] {JoinBuilder.BuildJoin(typeof(TA), typeof(TB))};
        }

        public IJoin<TA, TB, TNext> Join<TNext>()
        {

            return new BlockBaseJoin<TA, TB, TNext>(GenerateJoins<TNext>());
        }

        public IQueryableJoinSet<TA, TB> Where(Expression<Func<TA, TB, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(
            Expression<Func<TA, TB, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }
    }

    public class BlockBaseJoin<TA, TB, TC> : BlockBaseJoin<BlockBaseJoin<TA, TB, TC>>, IJoin<TA, TB, TC>
    {
        public BlockBaseJoin(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoin()
        {
            Joins = new[] {JoinBuilder.BuildJoin(typeof(TA), typeof(TB))};
        }

        public IJoin<TA, TB, TC, TNext> Join<TNext>()
        {
            return new BlockBaseJoin<TA, TB, TC, TNext>(GenerateJoins<TNext>());
        }

        IQueryableJoinSet<TA, TB, TC> IQueryableJoinSet<TA, TB, TC>.Where(Expression<Func<TA, TB, TC, bool>> predicate)
        {
            return Where(predicate);
        }

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(
            Expression<Func<TA, TB, TC, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }

        public IQueryableJoinSet<TA, TB, TC> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC> Where(Expression<Func<TA, TB, TC, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }
    }

    public class BlockBaseJoin<TA, TB, TC, TD> : BlockBaseJoin<BlockBaseJoin<TA, TB, TC, TD>>, IJoin<TA, TB, TC, TD>
    {
        public BlockBaseJoin(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoin()
        {
            Joins = new[] {JoinBuilder.BuildJoin(typeof(TA), typeof(TB))};
        }

        public IJoin<TA, TB, TC, TD, TNext> Join<TNext>()
        {
            return new BlockBaseJoin<TA, TB, TC, TD, TNext>(GenerateJoins<TNext>());
        }


        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(
            Expression<Func<TA, TB, TC, TD, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }


        public IQueryableJoinSet<TA, TB, TC, TD> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD> Where(Expression<Func<TA, TB, TC, TD, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }
    }

    public class BlockBaseJoin<TA, TB, TC, TD, TE> : BlockBaseJoin<BlockBaseJoin<TA, TB, TC, TD, TE>>,
        IJoin<TA, TB, TC, TD, TE>
    {
        public BlockBaseJoin(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoin()
        {
            Joins = new[] {JoinBuilder.BuildJoin(typeof(TA), typeof(TB))};
        }

        public IJoin<TA, TB, TC, TD, TE, TNext> Join<TNext>()
        {
            return new BlockBaseJoin<TA, TB, TC, TD, TE, TNext>(GenerateJoins<TNext>());
        }

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(
            Expression<Func<TA, TB, TC, TD, TE, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }
        public IQueryableJoinSet<TA, TB, TC, TD, TE> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE> Where(Expression<Func<TA, TB, TC, TD, TE, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }
    }

    public class BlockBaseJoin<TA, TB, TC, TD, TE, TF> : BlockBaseJoin<BlockBaseJoin<TA, TB, TC, TD, TE, TF>>, IJoin<TA, TB, TC, TD, TE, TF>
    {
        public BlockBaseJoin(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoin()
        {
            Joins = new[] { JoinBuilder.BuildJoin(typeof(TA), typeof(TB)) };
        }

        public IJoin<TA, TB, TC, TD, TE, TF, TNext> Join<TNext>()
        {
            return new BlockBaseJoin<TA, TB, TC, TD, TE, TF, TNext>(GenerateJoins<TNext>());
        }

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF> Where(Expression<Func<TA, TB, TC, TD, TE, TF, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }
    }

    public class BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG> : BlockBaseJoin<BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG>>, IJoin<TA, TB, TC, TD, TE, TF, TG>
    {
        public BlockBaseJoin(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoin()
        {
            Joins = new[] { JoinBuilder.BuildJoin(typeof(TA), typeof(TB)) };
        }

        public IJoin<TA, TB, TC, TD, TE, TF, TG, TNext> Join<TNext>()
        {
            return new BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TNext>(GenerateJoins<TNext>());
        }

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }
    }

    public class BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH> : BlockBaseJoin<BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH>>, IJoin<TA, TB, TC, TD, TE, TF, TG, TH>
    {
        public BlockBaseJoin(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoin()
        {
            Joins = new[] { JoinBuilder.BuildJoin(typeof(TA), typeof(TB)) };
        }

        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TNext> Join<TNext>()
        {
            return new BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TNext>(GenerateJoins<TNext>());
        }

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }
    }
    public class BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI> : BlockBaseJoin<BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI>>, IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI>
    {
        public BlockBaseJoin(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoin()
        {
            Joins = new[] { JoinBuilder.BuildJoin(typeof(TA), typeof(TB)) };
        }

        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TNext> Join<TNext>()
        {
            return new BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TNext>(GenerateJoins<TNext>());
        }

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }
    }


    public class BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> : BlockBaseJoin<BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ>>, IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ>
    {
        public BlockBaseJoin(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoin()
        {
            Joins = new[] { JoinBuilder.BuildJoin(typeof(TA), typeof(TB)) };
        }

        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TNext> Join<TNext>()
        {
            return new BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TNext>(GenerateJoins<TNext>());
        }

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }


        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }
    }

    public class BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> : BlockBaseJoin<BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK>>, IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK>
    {
        public BlockBaseJoin(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoin()
        {
            Joins = new[] { JoinBuilder.BuildJoin(typeof(TA), typeof(TB)) };
        }

        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TNext> Join<TNext>()
        {
            return new BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TNext>(GenerateJoins<TNext>());
        }
        

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }
    }

    public class BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> : BlockBaseJoin<BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL>>, IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL>
    {
        public BlockBaseJoin(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoin()
        {
            Joins = new[] { JoinBuilder.BuildJoin(typeof(TA), typeof(TB)) };
        }

        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TNext> Join<TNext>()
        {
            return new BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TNext>(GenerateJoins<TNext>());
        }
        
        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }
    }

    public class BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> : BlockBaseJoin<BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM>>, IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM>
    {
        public BlockBaseJoin(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoin()
        {
            Joins = new[] { JoinBuilder.BuildJoin(typeof(TA), typeof(TB)) };
        }

        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TNext> Join<TNext>()
        {
            return new BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TNext>(GenerateJoins<TNext>());
        }
        
        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }
    }

    public class BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> : BlockBaseJoin<BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN>>, IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN>
    {
        public BlockBaseJoin(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoin()
        {
            Joins = new[] { JoinBuilder.BuildJoin(typeof(TA), typeof(TB)) };
        }

        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TNext> Join<TNext>()
        {
            return new BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TNext>(GenerateJoins<TNext>());
        }
        
        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }
    }

    public class BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> : BlockBaseJoin<BlockBaseJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO>>, IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO>
    {
        public BlockBaseJoin(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoin()
        {
            Joins = new[] { JoinBuilder.BuildJoin(typeof(TA), typeof(TB)) };
        }


        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, TRecordResult>> mapper)
        {
            return base.GetSelectQuery<TRecordResult>(mapper);
        }

        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, TRecordResult>> mapper)
        {
            base.BatchSelect<TRecordResult>(mapper);
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, TRecordResult>> mapper)
        {
            return await base.SelectAsync<TRecordResult>(mapper);
        }
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }
    }
}