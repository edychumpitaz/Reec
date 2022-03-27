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
        /// Ejecuta un script de sql y retorna un DataSet.
        /// Si el Query no retorna filas, la función retorna null.
        /// </summary>
        /// <param name="query">Ejemplo: $"select * from [Tabla] where columna = '{variable1}'</param>
        /// <returns></returns>
        DataSet QueryToDataSet(string query);

        /// <summary>
        /// Ejecuta un script de sql y retorna un DataSet.
        /// Si el Query no retorna filas, la función retorna null.
        /// </summary>
        /// <param name="query">Ejemplo: $"select * from [Tabla] where columna = '{variable1}'</param>
        /// <param name="cancellationToken">Token de cancelación de ejecución</param>
        /// <returns></returns>
        Task<DataSet> QueryToDataSetAsync(string query, CancellationToken cancellationToken = default);

        /// <summary>
        /// Ejecuta un script de sql y retorna un DataTable.
        /// </summary>
        /// <param name="query">Ejemplo: $"select * from [Tabla] where columna = '{variable1}'</param>
        /// <returns></returns>
        DataTable QueryToDataTable(string query);

        /// <summary>
        /// Ejecuta un script de sql y retorna un DataTable.
        /// </summary>
        /// <param name="query">Ejemplo: $"select * from [Tabla] where columna = '{variable1}'</param>
        /// <param name="cancellationToken">Token de cancelación de ejecución</param>
        /// <returns></returns>
        Task<DataTable> QueryToDataTableAsync(string query, CancellationToken cancellationToken = default);

        /// <summary>
        /// Ejecuta un script de sql y retorna un Entity.
        /// Si la consulta sql retorna más de una fila, la función retornara null.
        /// </summary>
        /// <typeparam name="TEntity">Entidad de Retorno</typeparam>
        /// <param name="query">Ejemplo: $"select * from [Tabla] where columna = '{variable1}'</param>
        /// <returns></returns>
        TEntity QueryToEntity<TEntity>(string query) where TEntity : class;

        /// <summary>
        /// Ejecuta un script de sql y retorna un Entity.
        /// Si la consulta sql retorna más de una fila, la función retornara null.
        /// </summary>
        /// <typeparam name="TEntity">Entidad de Retorno</typeparam>
        /// <param name="query">Ejemplo: $"select * from [Tabla] where columna = '{variable1}'</param>
        /// <param name="cancellationToken">Token de cancelación de ejecución</param>
        /// <returns></returns>
        Task<TEntity> QueryToEntityAsync<TEntity>(string query, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// Ejecuta un script de sql y retorna un ListEntity.
        /// Si la consulta sql no retorna ninguna fila, la función retornara null.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query">Ejemplo: $"select * from [Tabla] where columna = '{variable1}'"</param>
        /// <returns></returns>
        List<TEntity> QueryToListEntity<TEntity>(string query) where TEntity : class;

        /// <summary>
        /// Ejecuta un script de sql y retorna un ListEntity.
        /// Si la consulta sql no retorna ninguna fila, la función retornara null.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query">Ejemplo: $"select * from [Tabla] where columna = '{variable1}'"</param>
        /// <param name="cancellationToken">Token de cancelación de ejecución</param>
        /// <returns></returns>
        /// <returns></returns>
        Task<List<TEntity>> QueryToListEntityAsync<TEntity>(string query, CancellationToken cancellationToken = default) where TEntity : class;



    }

}
