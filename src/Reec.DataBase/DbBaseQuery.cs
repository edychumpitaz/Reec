using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reec.DataBase 
{
    public partial class DbBase<TConnection, TCommand, TParameter, TTransaction> : 
                                IDbBase<TConnection, TCommand, TParameter, TTransaction>

    {
 
        
        public DataTable QueryToDataTable(string query)
        {
            var dt = this.QueryReader(query).ToDataTable();
            return dt;
        }
        public async Task<DataTable> QueryToDataTableAsync(string query, CancellationToken cancellationToken = default)
        {
            var dt = (await this.QueryReaderAsync(query, cancellationToken)).ToDataTable();
            return dt;
        }



        public DataSet QueryToDataSet(string query)
        {
            DataSet dts = this.QueryReader(query).ToDataSet();
            return dts;
        }
        public async Task<DataSet> QueryToDataSetAsync(string query, CancellationToken cancellationToken = default)
        {
            DataSet dts = (await this.QueryReaderAsync(query, cancellationToken)).ToDataSet();
            return dts;
        }



        public TEntity QueryToEntity<TEntity>(string query) where TEntity : class
        {
            var entity = this.QueryReader(query).ToEntity<TEntity>();
            return entity;
        }
        public async Task<TEntity> QueryToEntityAsync<TEntity>(string query, CancellationToken cancellationToken = default) where TEntity : class
        {
            var entity = (await this.QueryReaderAsync(query, cancellationToken)).ToEntity<TEntity>();
            return entity;
        }



        public List<TEntity> QueryToListEntity<TEntity>(string query) where TEntity : class
        {
            var listEntity = this.QueryReader(query).ToListEntity<TEntity>();
            return listEntity;
        }
        public async Task<List<TEntity>> QueryToListEntityAsync<TEntity>(string query, CancellationToken cancellationToken = default) where TEntity : class
        {

            var listEntity = (await this.QueryReaderAsync(query, cancellationToken)).ToListEntity<TEntity>();
            return listEntity;
        }
     

    }


}
