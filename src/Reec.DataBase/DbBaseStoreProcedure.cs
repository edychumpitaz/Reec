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


        
        public virtual async Task<DataTable> ExecuteToDataTableAsync(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters)
        {
            var dt = (await this.ExecuteReaderAsync(storeProcedure, cancellationToken, parameters)).ToDataTable();
            return dt;
        }
        
        public virtual async Task<DataTable> ExecuteToDataTableAsync(string storeProcedure, params TParameter[] parameters)
        {
            var dt = (await this.ExecuteReaderAsync(storeProcedure, parameters)).ToDataTable();
            return dt;
        }
        
        public virtual DataTable ExecuteToDataTable(string storeProcedure, params TParameter[] parameters)
        {
            var dt = this.ExecuteReader(storeProcedure, parameters).ToDataTable();
            return dt;
        }



        
        public virtual async Task<DataSet> ExecuteToDataSetAsync(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters)
        {
            var dts = (await this.ExecuteReaderAsync(storeProcedure, cancellationToken, parameters)).ToDataSet();
            return dts;
        }
        
        public virtual async Task<DataSet> ExecuteToDataSetAsync(string storeProcedure, params TParameter[] parameters)
        {
            var dts = (await this.ExecuteReaderAsync(storeProcedure, parameters)).ToDataSet();
            return dts;
        }
        
        public virtual DataSet ExecuteToDataSet(string storeProcedure, params TParameter[] parameters)
        {
            var dts = this.ExecuteReader(storeProcedure, parameters).ToDataSet();
            return dts;
        }



        
        public virtual async Task<TEntity> ExecuteToEntityAsync<TEntity>(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters) where TEntity : class
        {
            var entity = (await this.ExecuteReaderAsync(storeProcedure, cancellationToken, parameters)).ToEntity<TEntity>();
            return entity;
        }        
        
        public virtual async Task<TEntity> ExecuteToEntityAsync<TEntity>(string storeProcedure, params TParameter[] parameters) where TEntity : class
        {
            var entity = (await this.ExecuteReaderAsync(storeProcedure, parameters)).ToEntity<TEntity>();
            return entity;
        }       
        
        public virtual TEntity ExecuteToEntity<TEntity>(string storeProcedure, params TParameter[] parameters) where TEntity : class
        {
            var entity = this.ExecuteReader(storeProcedure, parameters).ToEntity<TEntity>();
            return entity;
        }



        
        public virtual async Task<List<TEntity>> ExecuteToListEntityAsync<TEntity>(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters) where TEntity : class
        {
            var vResult = (await this.ExecuteReaderAsync(storeProcedure, cancellationToken, parameters)).ToListEntity<TEntity>();
            return vResult;
        }
        
        public virtual async Task<List<TEntity>> ExecuteToListEntityAsync<TEntity>(string storeProcedure, params TParameter[] parameters) where TEntity : class
        {
            var vResult = (await this.ExecuteReaderAsync(storeProcedure, parameters)).ToListEntity<TEntity>();
            return vResult;
        }
        
        public virtual List<TEntity> ExecuteToListEntity<TEntity>(string storeProcedure, params TParameter[] parameters) where TEntity : class
        {
            var vResult = this.ExecuteReader(storeProcedure, parameters).ToListEntity<TEntity>();
            return vResult;
        }




    }

}
