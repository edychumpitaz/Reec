using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Reec.Helpers
{
    public static class ExtensionHelpers
    {
        /// <summary>
        /// Convierte un DataReader a un DataTable.
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this DbDataReader dataReader)
        {
            return HelperConvert.DataReaderToDataTable(dataReader);
        }

        /// <summary>
        /// Convierte un DataReader en un DataSet.
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static DataSet ToDataSet(this DbDataReader dataReader)
        {
            return HelperConvert.DataReaderToDataSet(dataReader);
        }

        /// <summary>
        /// Convierte un DataReader en un objeto genérico. Si el número de filas es diferente a 1, retornará null
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static TEntity ToEntity<TEntity>(this DbDataReader dataReader) where TEntity : class
        {
            return HelperConvert.DataReaderToEntity<TEntity>(dataReader);
        }

        /// <summary>
        /// Convierte un DataReader a una lista genérica, si no hay ninguna propiedad que haga match, retornara null.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static List<TEntity> ToListEntity<TEntity>(this DbDataReader dataReader) where TEntity : class
        {
            return HelperConvert.DataReaderToList<TEntity>(dataReader);
        }



        /// <summary>
        /// Convierte un DataTable en un objeto genérico. Si el DataTable no contiene elementos, la función retornará null.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static TEntity ToEntity<TEntity>(this DataTable dataTable) where TEntity : class
        {
            return HelperConvert.DataTableToEntity<TEntity>(dataTable);
        }

        /// <summary>
        /// Convierte un DataTable en una lista genérica. Si el DataTable no contiene elementos, la función retornará null.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<TEntity> ToListEntity<TEntity>(this DataTable dataTable) where TEntity : class
        {
            return HelperConvert.DataTableToList<TEntity>(dataTable);
        }




        /// <summary>
        /// Convierte una lista genérica en un DataTable. Si la lista no contiene elementos, la función retornará null.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="listEntity"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<TEntity>(this IList<TEntity> listEntity, params string[] members) where TEntity : class
        {
            return HelperConvert.ListEntityToDataTable(listEntity, members);
        }

        /// <summary>
        /// Convierte una lista genérica en un DataReader, los nombres de las columnas estan ordenadas alfabeticamente. 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="listEntity"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        public static DbDataReader ToDataReader<TEntity>(this IList<TEntity> listEntity, params string[] members) where TEntity : class
        {
            return HelperConvert.ListEntityToDataReader(listEntity, members);
        }



    }

}
