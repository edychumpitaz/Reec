using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reec.DataBase
{
    public partial class DbBase<TConnection, TCommand, TParameter, TTransaction> : IDisposable,
                            IDbBase<TConnection, TCommand, TParameter, TTransaction>
                            where TConnection : DbConnection
                            where TCommand : DbCommand
                            where TParameter : DbParameter
                            where TTransaction : DbTransaction

    {

        private readonly string connectionString;

        /// <summary>
        /// Tiempo expresado en segundos.
        /// </summary>
        private readonly int commandTimeOut = 30;
        private bool disposedValue;

        private TTransaction Transaction { get; set; }

        private TConnection _Connection;
        private TConnection Connection
        {
            get
            {
                if (this._Connection == null)
                {
                    var conecction = Activator.CreateInstance<TConnection>();
                    conecction.ConnectionString = connectionString;
                    return conecction;
                    //this._Connection =   new SqlConnection(connectionString);
                }

                return this._Connection;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection">Cadena de conexión de Base de Datos.</param>
        /// <param name="commandTimeOut">Tiempo expresado en segundos, por defecto 30 segundos.</param>
        public DbBase(string connection, int commandTimeOut = 30)
        {
            this.connectionString = connection;
            this.commandTimeOut = commandTimeOut;
            this._Connection = Activator.CreateInstance<TConnection>();
            this._Connection.ConnectionString = connection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction">Inicia un ambiente de transacción.</param>
        /// <param name="commandTimeOut">Tiempo expresado en segundos, por defecto 30 segundos.</param>
        public DbBase(TTransaction transaction, int commandTimeOut = 30)
        {
            this.connectionString = transaction.Connection.ConnectionString;
            this._Connection = (TConnection)transaction.Connection;
            this.commandTimeOut = commandTimeOut;
            this.Transaction = transaction;
        }

        
        public TTransaction BeginTransaccion()
        {
            if (this.Connection.State == ConnectionState.Closed)
                this.Connection.Open();
            this.Transaction = (TTransaction)Connection.BeginTransaction();
            return this.Transaction;
        }
        
        public void UseTransaccion(TTransaction transaction)
        {
            this.Transaction = transaction;
            this._Connection = (TConnection)transaction.Connection;
            if (this.Connection.State == ConnectionState.Closed)
                this.Connection.Open();
        }


        /// <summary>
        /// Configuración de parametros de SQL
        /// <para>Ejemplo: Enviar una DataTable</para>
        /// param.SqlDbType = SqlDbType.Structured;
        /// param.TypeName = item.TypeName (Nombre de objeto de BD);
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private TCommand CommandProcedure(string storeProcedure, params TParameter[] parameters)
        {
            var Command = Activator.CreateInstance<TCommand>();
            Command.CommandType = CommandType.StoredProcedure;
            Command.Connection = this.Connection;
            Command.CommandTimeout = commandTimeOut;
            Command.CommandText = storeProcedure;

            if (this.Transaction != null)
                Command.Transaction = this.Transaction;

            if (this.Connection.State == ConnectionState.Closed)
                this.Connection.Open();

            if (parameters != null && parameters.Length > 0)
                Command.Parameters.AddRange(parameters);

            return Command;
        }

        private TCommand CommandQuery(string query)
        {
            var Command = Activator.CreateInstance<TCommand>();
            Command.CommandType = CommandType.Text;
            Command.Connection = this.Connection;
            Command.CommandTimeout = commandTimeOut;
            Command.CommandText = query;

            if (this.Connection.State == ConnectionState.Closed)
                this.Connection.Open();


            if (this.Transaction != null)
                Command.Transaction = this.Transaction;

            return Command;
        }



        #region Ejecución de SqlQuery

        
        public async Task<DbDataReader> QueryReaderAsync(string query, CancellationToken cancellationToken = default)
        {
            using var Command = CommandQuery(query);
            if (this.Transaction != null)
            {
                Command.Transaction = this.Transaction;
                var reader = await Command.ExecuteReaderAsync(cancellationToken);
                return reader;
                //IAsyncResult result = Command.BeginExecuteReader();
                //while (!result.IsCompleted) await Task.Delay(5);
                //return Command.EndExecuteReader(result);
            }
            else
                return await Command.ExecuteReaderAsync(CommandBehavior.CloseConnection, cancellationToken);

        }

        public DbDataReader QueryReader(string query)
        {
            using var Command = CommandQuery(query);
            if (this.Transaction != null)
            {
                Command.Transaction = this.Transaction;
                var reader = Command.ExecuteReader();
                return reader;
                //IAsyncResult result = Command.BeginExecuteReader();
                //while (!result.IsCompleted) Task.Delay(5).Wait();
                //return Command.EndExecuteReader(result);
            }
            else
                return Command.ExecuteReader(CommandBehavior.CloseConnection);
        }



        public async Task<int> QueryRowsAffectedAsync(string query, CancellationToken cancellationToken = default)
        {
            using var Command = CommandQuery(query);
            var reader = await Command.ExecuteNonQueryAsync(cancellationToken);
            if (this.Transaction == null) await this.Connection.CloseAsync();
            if (cancellationToken.IsCancellationRequested)
                if (this.Transaction != null) await this.Transaction.RollbackAsync();
            return reader;
        }
  
        public int QueryRowsAffected(string query)
        {
            using var Command = CommandQuery(query);
            var reader = Command.ExecuteNonQuery();
            if (this.Transaction == null) this.Connection.Close();
            return reader;
        }


        #endregion



        #region Ejecución de StoredProcedure       
        
        public async Task<DbDataReader> ExecuteReaderAsync(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters)
        {
            using var Command = this.CommandProcedure(storeProcedure, parameters);
            if (this.Transaction != null)
            {
                Command.Transaction = this.Transaction;
                var reader = await Command.ExecuteReaderAsync(cancellationToken);
                return reader;
                //IAsyncResult result = Command.BeginExecuteReader();
                //while (!result.IsCompleted) await Task.Delay(5);
                //return Command.EndExecuteReader(result);
            }
            else
                return await Command.ExecuteReaderAsync(CommandBehavior.CloseConnection, cancellationToken);
        }
        
        public async Task<DbDataReader> ExecuteReaderAsync(string storeProcedure, params TParameter[] parameters)
        {
            using var Command = this.CommandProcedure(storeProcedure, parameters);
            if (this.Transaction != null)
            {
                Command.Transaction = this.Transaction;
                var reader = await Command.ExecuteReaderAsync();
                return reader;
                //IAsyncResult result = Command.BeginExecuteReader();
                //while (!result.IsCompleted) await Task.Delay(5);
                //return Command.EndExecuteReader(result);
            }
            else
                return await Command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }
        
        public DbDataReader ExecuteReader(string storeProcedure, params TParameter[] parameters)
        {

            using var Command = this.CommandProcedure(storeProcedure, parameters);
            if (this.Transaction != null)
            {
                Command.Transaction = this.Transaction;
                var reader = Command.ExecuteReaderAsync().Result;
                return reader;
                //IAsyncResult result = Command.BeginExecuteReader();
                //while (!result.IsCompleted) Task.Delay(5).Wait();
                //return Command.EndExecuteReader(result);
            }
            else
                return Command.ExecuteReader(CommandBehavior.CloseConnection);
        }


        
        public async Task<int> ExecuteNonQueryAsync(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters)
        {
            using var sqlCommand = this.CommandProcedure(storeProcedure, parameters);
            var vResult = await sqlCommand.ExecuteNonQueryAsync(cancellationToken);
            if (this.Transaction == null) await this.Connection.CloseAsync();
            if (cancellationToken.IsCancellationRequested)            
                if (this.Transaction != null) await this.Transaction.RollbackAsync();
            
            return vResult;
        }
        
        public async Task<int> ExecuteNonQueryAsync(string storeProcedure, params TParameter[] parameters)
        {
            using var sqlCommand = this.CommandProcedure(storeProcedure, parameters);
            var vResult = await sqlCommand.ExecuteNonQueryAsync();
            if (this.Transaction == null) await this.Connection.CloseAsync();
            return vResult;
        }
        
        public int ExecuteNonQuery(string storeProcedure, params TParameter[] parameters)
        {
            using var sqlCommand = this.CommandProcedure(storeProcedure, parameters);
            var vResult = sqlCommand.ExecuteNonQuery();
            if (this.Transaction == null) this.Connection.Close();
            return vResult;
        }

        #endregion



        //protected virtual void Dispose(bool disposing)
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar el estado administrado (objetos administrados)
                    if (this.Connection != null)
                    {
                        this.Connection.Close();
                        this.Connection.Dispose();
                    }

                    if (Transaction != null)
                        Transaction.Dispose();

                }


                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
                Transaction = null;
                _Connection = null;
            }
        }

        // // TODO: reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~UtilitySqlServer()
        // {
        //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }

}
