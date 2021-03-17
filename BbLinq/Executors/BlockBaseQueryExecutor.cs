using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Contexts;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Helpers;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Properties;
using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.Settings;

namespace BlockBase.BBLinq.Executors
{
    public class BlockBaseQueryExecutor : IQueryExecutor

    {
        public bool UseDatabase { get; set; }
        public bool UseTransaction { get; set; }

        #region Callers
        public async Task ExecuteQueryAsync(IQuery query)
        {
            var settings = GetSettings();
            var queryString = BuildQueryString(query.GenerateQueryString(), settings);
            var requestBody = GenerateRequestBody(queryString, settings);
            var result = await CallRequest(settings, requestBody);
            var parsedResult = (new BlockBaseResultParser()).Parse<string>(result, null);
            if (!parsedResult.Succeeded)
            {
                throw new QueryExecutionException(parsedResult.Message);
            }
        }

        public async Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(ISelectQuery query)
        {
            var settings = GetSettings();
            var queryString = BuildQueryString(query.GenerateQueryString(), settings);
            var requestBody = GenerateRequestBody(queryString, settings);
            var callResult = await CallRequest(settings, requestBody);
            var parsedResult = (new BlockBaseResultParser()).Parse<TResult>(callResult, query);
            if (!parsedResult.Succeeded)
            {
                throw new QueryExecutionException(parsedResult.Message);
            }

            var result = new List<TResult>();
            foreach (var response in parsedResult.Responses)
            {
                if (response == null) continue;
                if (response.Content == null)
                {
                    if (!response.Executed)
                    {
                        throw new QueryExecutionException(parsedResult.Message);
                    }
                }
                else
                {
                    result.AddRange(response?.Content);
                }
            }
            return result;
        }
       
        #endregion

        #region Completers
        private string BuildQueryString(string queryString, BlockBaseSettings settings)
        {
            var queryBuilder = new BlockBaseQueryBuilder();
            AddUseDatabase(queryBuilder, settings);
            AddTransaction(queryString, queryBuilder);
            return queryBuilder.ToString();
        }

        private void AddUseDatabase(BlockBaseQueryBuilder queryBuilder, BlockBaseSettings settings)
        {
            if (UseDatabase)
            {
                queryBuilder.UseDatabase(settings.DatabaseName);
            }
        }

        private void AddTransaction(string queryString, BlockBaseQueryBuilder queryBuilder)
        {
            if (UseTransaction)
            {
                queryBuilder.BeginTransaction();
            }
            queryBuilder.Append(queryString);
            if (UseTransaction)
            {
                queryBuilder.CommitTransaction();
                queryBuilder.RollbackTransaction();
            }
        }
        #endregion

        #region Request auxiliary

        private Dictionary<string,string> GenerateRequestBody(string query, BlockBaseSettings settings)
        {
            var signature = SignatureHelper.SignHash(settings.PrivateKey, Encoding.UTF8.GetBytes(query));
            var queryRequest = new Dictionary<string, string>
            {
                {"Query", query},
                {"Account", settings.UserAccount},
                {"Signature", signature}
            };
            return queryRequest;
        }

        #endregion

        private static BlockBaseSettings GetSettings()
        {
            return ContextCache.Instance.Get<BlockBaseSettings>(Resources.CACHE_SETTINGS);
        }
        private async Task<string> CallRequest(BlockBaseSettings settings, Dictionary<string, string> body)
        {
            var request = HttpHelper.ComposePostWebRequest($"{settings.NodeAddress}/api/Requester/ExecuteQuery");
            return await HttpHelper.CallWebRequestNoSSLVerification(request, body);
        }
    }
}
