using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Reec.DataBase
{
    public partial class DbBase<TConnection, TCommand, TParameter, TTransaction> :
                                IDbBase<TConnection, TCommand, TParameter, TTransaction>

    {


        public DataTable QueryToDataTable(string query)
        {
            return this.QueryReader(query).ToDataTable();
        }
        public async Task<DataTable> QueryToDataTableAsync(string query, CancellationToken cancellationToken = default)
        {
            return (await this.QueryReaderAsync(query, cancellationToken)).ToDataTable();
        }



        public DataSet QueryToDataSet(string query)
        {
            return this.QueryReader(query).ToDataSet();
        }
        public async Task<DataSet> QueryToDataSetAsync(string query, CancellationToken cancellationToken = default)
        {
            return (await this.QueryReaderAsync(query, cancellationToken)).ToDataSet();
        }



        public TEntity QueryToEntity<TEntity>(string query) where TEntity : class
        {
            return this.QueryReader(query).ToEntity<TEntity>();
        }
        public async Task<TEntity> QueryToEntityAsync<TEntity>(string query, CancellationToken cancellationToken = default) where TEntity : class
        {
            return (await this.QueryReaderAsync(query, cancellationToken)).ToEntity<TEntity>();
        }



        public List<TEntity> QueryToListEntity<TEntity>(string query) where TEntity : class
        {
            return this.QueryReader(query).ToListEntity<TEntity>();
        }
        public async Task<List<TEntity>> QueryToListEntityAsync<TEntity>(string query, CancellationToken cancellationToken = default) where TEntity : class
        {
            return (await this.QueryReaderAsync(query, cancellationToken)).ToListEntity<TEntity>();
        }


    }


}
