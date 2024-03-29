﻿using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Reec.DataBase
{
    /// <summary>
    /// Creación de conexión nativa a cualquier proveedor de base de datos. SqlServer, Oracle, MySql, etc
    /// </summary>
    /// <typeparam name="TConnection">Tipo de dato heredado de DbConnection</typeparam>
    /// <typeparam name="TCommand">Tipo de dato heredado de DbCommand</typeparam>
    /// <typeparam name="TParameter">Tipo de dato heredado de DbParameter</typeparam>
    /// <typeparam name="TTransaction">Tipo de dato heredado de DbTransaction</typeparam>
    public partial interface IDbBase<TConnection, TCommand, TParameter, TTransaction>
                                where TConnection : DbConnection
                                where TCommand : DbCommand
                                where TParameter : DbParameter
                                where TTransaction : DbTransaction
    {

        /// <summary>
        /// Crea un ambiente de transacción.
        /// </summary>
        /// <returns></returns>
        TTransaction BeginTransaccion();

        /// <summary>
        /// Utiliza un ambiente de transaccion externo.
        /// </summary>
        /// <param name="transaction"></param>
        void UseTransaccion(TTransaction transaction);

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna el número de filas afectadas.
        /// </summary>
        /// <param name="storeProcedure">Nombre de storeProcedure</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string storeProcedure, params TParameter[] parameters);

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna el número de filas afectadas.
        /// </summary>
        /// <param name="storeProcedure">Nombre de StoreProcedure</param>
        /// <param name="cancellationToken">Token de cancelación de ejecución</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> ExecuteNonQueryAsync(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters);

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna el número de filas afectadas.
        /// </summary>
        /// <param name="storeProcedure">Nombre de StoreProcedure</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> ExecuteNonQueryAsync(string storeProcedure, params TParameter[] parameters);

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un DataReader.
        /// <para>Ejemplo: Enviar una DataTable</para>
        /// param.SqlDbType = SqlDbType.Structured;
        /// param.TypeName = item.TypeName (Nombre de objeto de BD);
        /// </summary>
        /// <param name="storeProcedure">Nombre de StoreProcedure</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DbDataReader ExecuteReader(string storeProcedure, params TParameter[] parameters);

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un DataReader.
        /// <para>Ejemplo: Enviar una DataTable</para>
        /// param.SqlDbType = SqlDbType.Structured;
        /// param.TypeName = item.TypeName (Nombre de objeto de BD);
        /// </summary>
        /// <param name="storeProcedure">Nombre de StoreProcedure</param>
        /// <param name="cancellationToken">Token de cancelación de ejecución</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<DbDataReader> ExecuteReaderAsync(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters);

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un DataReader.
        /// <para>Ejemplo: Enviar una DataTable</para>
        /// param.SqlDbType = SqlDbType.Structured;
        /// param.TypeName = item.TypeName (Nombre de objeto de BD);
        /// </summary>
        /// <param name="storeProcedure">Nombre de StoreProcedure</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<DbDataReader> ExecuteReaderAsync(string storeProcedure, params TParameter[] parameters);



        /// <summary>
        /// Ejecuta un script de sql y retorna un DataReader.
        /// </summary>
        /// <param name="query">Ejemplo: $"select * from [Tabla] where columna = '{variable1}'</param>
        /// <returns></returns>
        DbDataReader QueryReader(string query);

        /// <summary>
        /// Ejecuta un script de sql y retorna un DataReader. 
        /// </summary>
        /// <param name="query">Ejemplo: $"select * from [Tabla] where columna = '{variable1}'</param>
        /// <param name="cancellationToken">Token de cancelación de ejecución</param>
        /// <returns></returns>
        Task<DbDataReader> QueryReaderAsync(string query, CancellationToken cancellationToken = default);

        /// <summary>
        /// Ejecuta un script de sql y retorna las filas afectadas. Insert, Delete, Update
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        int QueryRowsAffected(string query);

        /// <summary>
        /// Ejecuta un script de sql y retorna las filas afectadas. Insert, Delete, Update
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken">Token de cancelación de ejecución</param>
        /// <returns></returns>
        Task<int> QueryRowsAffectedAsync(string query, CancellationToken cancellationToken = default);


    }
}