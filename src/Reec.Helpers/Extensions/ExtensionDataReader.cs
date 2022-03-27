using Reec.Helpers;
using System.Collections.Generic;


namespace System.Data.Common
{
    public static class ExtensionDataReader
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

    }

}
