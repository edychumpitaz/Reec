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



        public virtual async Task<DataTable> ExecuteToDataTableAsync(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters)
        {
            return (await this.ExecuteReaderAsync(storeProcedure, cancellationToken, parameters)).ToDataTable();
        }

        public virtual async Task<DataTable> ExecuteToDataTableAsync(string storeProcedure, params TParameter[] parameters)
        {
            return (await this.ExecuteReaderAsync(storeProcedure, parameters)).ToDataTable();
        }

        public virtual DataTable ExecuteToDataTable(string storeProcedure, params TParameter[] parameters)
        {
            return this.ExecuteReader(storeProcedure, parameters).ToDataTable();
        }




        public virtual async Task<DataSet> ExecuteToDataSetAsync(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters)
        {
            return (await this.ExecuteReaderAsync(storeProcedure, cancellationToken, parameters)).ToDataSet();
        }

        public virtual async Task<DataSet> ExecuteToDataSetAsync(string storeProcedure, params TParameter[] parameters)
        {
            return (await this.ExecuteReaderAsync(storeProcedure, parameters)).ToDataSet();
        }

        public virtual DataSet ExecuteToDataSet(string storeProcedure, params TParameter[] parameters)
        {
            return this.ExecuteReader(storeProcedure, parameters).ToDataSet();
        }




        public virtual async Task<TEntity> ExecuteToEntityAsync<TEntity>(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters) where TEntity : class
        {
            return (await this.ExecuteReaderAsync(storeProcedure, cancellationToken, parameters)).ToEntity<TEntity>();
        }

        public virtual async Task<TEntity> ExecuteToEntityAsync<TEntity>(string storeProcedure, params TParameter[] parameters) where TEntity : class
        {
            return (await this.ExecuteReaderAsync(storeProcedure, parameters)).ToEntity<TEntity>();
        }

        public virtual TEntity ExecuteToEntity<TEntity>(string storeProcedure, params TParameter[] parameters) where TEntity : class
        {
            return this.ExecuteReader(storeProcedure, parameters).ToEntity<TEntity>();
        }




        public virtual async Task<List<TEntity>> ExecuteToListEntityAsync<TEntity>(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters) where TEntity : class
        {
            return (await this.ExecuteReaderAsync(storeProcedure, cancellationToken, parameters)).ToListEntity<TEntity>();
        }

        public virtual async Task<List<TEntity>> ExecuteToListEntityAsync<TEntity>(string storeProcedure, params TParameter[] parameters) where TEntity : class
        {
            return (await this.ExecuteReaderAsync(storeProcedure, parameters)).ToListEntity<TEntity>();
        }

        public virtual List<TEntity> ExecuteToListEntity<TEntity>(string storeProcedure, params TParameter[] parameters) where TEntity : class
        {
            return this.ExecuteReader(storeProcedure, parameters).ToListEntity<TEntity>();
        }




    }

}
