using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
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
        /// Ejecuta un StoreProcedure y retorna un DataSet.
        /// Si el StoreProcedure no retorna filas, la función retorna null.
        /// </summary>
        /// <param name="storeProcedure">Nombre de StoreProcedure</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet ExecuteToDataSet(string storeProcedure, params TParameter[] parameters);

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un DataSet.
        /// Si el StoreProcedure no retorna filas, la función retorna null.
        /// </summary>
        /// <param name="storeProcedure">Nombre de StoreProcedure</param>
        /// <param name="cancellationToken">Token de cancelación de ejecución</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<DataSet> ExecuteToDataSetAsync(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters);

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un DataSet.
        /// Si el StoreProcedure no retorna filas, la función retorna null.
        /// </summary>
        /// <param name="storeProcedure">Nombre de StoreProcedure</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<DataSet> ExecuteToDataSetAsync(string storeProcedure, params TParameter[] parameters);



        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un DataTable.
        /// Si el StoreProcedure no retorna filas, la función retorna null.
        /// </summary>
        /// <param name="storeProcedure">Nombre de StoreProcedure</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataTable ExecuteToDataTable(string storeProcedure, params TParameter[] parameters);

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un DataTable.
        /// Si el StoreProcedure no retorna filas, la función retorna null.
        /// </summary>
        /// <param name="storeProcedure">Nombre de StoreProcedure</param>
        /// <param name="cancellationToken">Token de cancelación de ejecución</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<DataTable> ExecuteToDataTableAsync(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters);

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un DataTable.
        /// Si el StoreProcedure no retorna filas, la función retorna null.
        /// </summary>
        /// <param name="storeProcedure">Nombre de StoreProcedure</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<DataTable> ExecuteToDataTableAsync(string storeProcedure, params TParameter[] parameters);



        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un Entity.
        /// Si el StoreProcedure retorna mas de una fila, la función retornara null.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de objeto de retorno.</typeparam>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        TEntity ExecuteToEntity<TEntity>(string storeProcedure, params TParameter[] parameters) where TEntity : class;

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un Entity.
        /// Si el StoreProcedure retorna mas de una fila, la función retornara null.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de objeto de retorno.</typeparam>
        /// <param name="storeProcedure"></param>
        /// <param name="cancellationToken">Token de cancelación de ejecución</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<TEntity> ExecuteToEntityAsync<TEntity>(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters) where TEntity : class;

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un Entity.
        /// Si el StoreProcedure retorna mas de una fila, la función retornara null.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de objeto de retorno.</typeparam>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<TEntity> ExecuteToEntityAsync<TEntity>(string storeProcedure, params TParameter[] parameters) where TEntity : class;



        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un ListEntity.
        /// Si el StoreProcedure no retorna filas, la función retornara null.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad para rtornarlo como lista</typeparam>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<TEntity> ExecuteToListEntity<TEntity>(string storeProcedure, params TParameter[] parameters) where TEntity : class;

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un ListEntity.
        /// Si el StoreProcedure no retorna filas, la función retornara null.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad para retornarlo como lista</typeparam>
        /// <param name="storeProcedure"></param>
        /// <param name="cancellationToken">Token de cancelación de ejecución</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<List<TEntity>> ExecuteToListEntityAsync<TEntity>(string storeProcedure, CancellationToken cancellationToken = default, params TParameter[] parameters) where TEntity : class;

        /// <summary>
        /// Ejecuta un StoreProcedure y retorna un ListEntity.
        /// Si el StoreProcedure no retorna filas, la función retornara null.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad para rtornarlo como lista</typeparam>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<List<TEntity>> ExecuteToListEntityAsync<TEntity>(string storeProcedure, params TParameter[] parameters) where TEntity : class;


    }


}
