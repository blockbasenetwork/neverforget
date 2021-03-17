using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Base;
using BlockBase.BBLinq.Model.Nodes;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Queries.BlockBaseQueries
{
    public class BlockBaseRecordDeleteQuery:BlockBaseQuery, IDeleteDatabaseQuery
    {
        public string DatabaseName { get; }
        public ExpressionNode Condition { get; }

        public BlockBaseRecordDeleteQuery(string databaseName, bool isEncrypted) : base(isEncrypted)
        {
            DatabaseName = databaseName;
        }

        public BlockBaseRecordDeleteQuery(string databaseName, ExpressionNode condition, bool isEncrypted) : base(isEncrypted)
        {
            DatabaseName = databaseName;
            Condition = condition;
        }

        public override string GenerateQueryString()
        {
            var queryBuilder = new BlockBaseQueryBuilder();
            queryBuilder.DeleteRecord(DatabaseName, Condition, IsEncrypted);
            return queryBuilder.ToString();
        }
    }
}
