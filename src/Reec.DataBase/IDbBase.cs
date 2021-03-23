using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Reec.DataBase
{
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
        /// Ejecuta un query de sql y retorna un DataReader. 
        /// Puede utilizar las extensiones como QueryReader(query).ToEntity<TObject>()
        /// </summary>
        /// <param name="query">Ejemplo: $"select * from [Tabla] where columna = '{variable1}'</param>
        /// <returns></returns>
        DbDataReader QueryReader(string query);

        /// <summary>
        /// Ejecuta un query de sql y retorna un DataReader. 
        /// Puede utilizar las extensiones como QueryReaderAsync(quey).ToEntity<TObject>()
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