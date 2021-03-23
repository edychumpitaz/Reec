using Reec.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Xml;


namespace System.Data.Common 
{
    public static class ExtensionDataReader
    {

        public static DataTable ToDataTable(this DbDataReader dataReader)
        {
            DataTable dt = HelperConvert.DataReaderToDataTable(dataReader);
            return dt;
        }

        public static DataSet ToDataSet(this DbDataReader dataReader)
        {
            DataSet dts = HelperConvert.DataReaderToDataSet(dataReader);
            return dts;
        }

        /// <summary>
        /// Convierte un DataReader en un objeto genérico. Si el número de filas es diferente a 1, retornará null
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static TEntity ToEntity<TEntity>(this DbDataReader dataReader) where TEntity : class
        {
            var entity = HelperConvert.DataReaderToEntity<TEntity>(dataReader);
            return entity;
        }

        /// <summary>
        /// Convierte un DataReader a una lista genérica, si no hay ninguna propiedad que haga match, retornara null.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static List<TEntity> ToListEntity<TEntity>(this DbDataReader dataReader) where TEntity : class
        {
            var list = HelperConvert.DataReaderToList<TEntity>(dataReader);
            return list;
        }

    }

}
