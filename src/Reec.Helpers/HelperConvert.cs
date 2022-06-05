using FastMember;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Reec.Helpers
{
    public static class HelperConvert
    {

        /// <summary>
        /// Convierte un DataReader a una lista genérica, si no hay ninguna propiedad que haga match, retornara null.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static List<TEntity> DataReaderToList<TEntity>(DbDataReader dataReader) where TEntity : class
        {
            string propertyName = null;
            List<TEntity> list = null;
            try
            {
                if (dataReader != null && !dataReader.IsClosed && dataReader.HasRows)
                {
                    list = new List<TEntity>();
                    var properties = typeof(TEntity).GetProperties();


                    var columnNames = dataReader.GetColumnSchema().Select(x => x.ColumnName);

                    var listProperties = (from prop in properties
                                          join row in columnNames
                                              on prop.Name.ToUpper() equals row.ToUpper()
                                          select prop)
                                        .ToList();

                    if (listProperties.Count == 0)
                        return null;

                    while (dataReader.Read())
                    {
                        var obj = Activator.CreateInstance<TEntity>();
                        foreach (var prop in listProperties)
                        {
                            propertyName = prop.Name;
                            if (!object.Equals(dataReader[prop.Name], DBNull.Value))
                                prop.SetValue(obj, dataReader[prop.Name]);
                        }
                        list.Add(obj);
                    }

                    listProperties.Clear();
                    listProperties = null;
                    properties = null;
                    propertyName = null;
                    columnNames = null;

                    dataReader.NextResult();
                }

                if (dataReader != null && !dataReader.IsClosed && !dataReader.HasRows)
                    dataReader.Close();

                return list;
            }
            catch (Exception ex)
            {
                var message = $"PropertyName '{propertyName}' : {ex.Message}";
                throw new Exception(message, ex);
            }
        }

        /// <summary>
        /// Convierte un DataReader en un objeto genérico. Si el número de filas es diferente a 1, retornará null
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static TEntity DataReaderToEntity<TEntity>(DbDataReader dataReader) where TEntity : class
        {
            string propertyName = null;
            TEntity entity = null;
            try
            {
                if (dataReader != null && !dataReader.IsClosed && dataReader.HasRows)
                {
                    var properties = typeof(TEntity).GetProperties();
                    var columnNames = dataReader.GetColumnSchema().Select(x => x.ColumnName);
                    var listProperties = (from prop in properties
                                          join row in columnNames
                                              on prop.Name.ToUpper() equals row.ToUpper()
                                          select prop)
                                        .ToList();

                    if (listProperties.Count == 0)
                        return null;

                    var count = 0;
                    while (dataReader.Read())
                    {
                        count++;
                        if (count >= 2)
                            break;

                        entity = Activator.CreateInstance<TEntity>();
                        foreach (var prop in listProperties)
                        {
                            propertyName = prop.Name;
                            if (!object.Equals(dataReader[prop.Name], DBNull.Value))
                                prop.SetValue(entity, dataReader[prop.Name]);
                        }

                    }

                    listProperties.Clear();
                    listProperties = null;
                    properties = null;
                    propertyName = null;
                    columnNames = null;

                    if (count != 1)
                        entity = default;

                    dataReader.NextResult();
                }


                if (dataReader != null && !dataReader.IsClosed && !dataReader.HasRows)
                    dataReader.Close();

                return entity;

            }
            catch (Exception ex)
            {
                var message = $"PropertyName '{propertyName}' : {ex.Message}";
                throw new Exception(message, ex);
            }

        }

        /// <summary>
        /// Convierte un DataReader genérico a DataTable. Si el DataReader no contiene elementos, la función retornará null.
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static DataTable DataReaderToDataTable(DbDataReader dataReader)
        {
            DataTable dt = null;
            if (dataReader != null && !dataReader.IsClosed && dataReader.HasRows)
            {
                dt = new DataTable();
                dt.Load(dataReader);
            }

            if (dataReader != null && !dataReader.IsClosed && !dataReader.HasRows)
                dataReader.Close();

            return dt;
        }

        /// <summary>
        /// Convierte un DataReader genérico a DataTable. Si el DataReader no contiene elementos, la función retornará null.
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static DataTable DataReaderToDataTable(IDataReader dataReader)
        {
            DataTable dt = null;
            if (dataReader != null && !dataReader.IsClosed)
            {
                dt = new DataTable();
                dt.Load(dataReader);
                if (dt.Rows.Count == 0)
                    dt = null;
            }
            return dt;
        }

        /// <summary>
        /// Convierte un DataReader a un DataSet. Si el DataReader no contiene elementos, la función retornará null.
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static DataSet DataReaderToDataSet(DbDataReader dataReader)
        {
            DataSet dts = null;
            var count = 0;
            while (dataReader != null && !dataReader.IsClosed && dataReader.HasRows)
            {
                count++;
                if (dts == null) dts = new DataSet();
                dts.Tables.Add($"Table{count}").Load(dataReader);
            }

            if (dataReader != null && !dataReader.IsClosed && !dataReader.HasRows)
                dataReader.Close();

            return dts;
        }


        /// <summary>
        /// Convierte un DataTable en un objeto genérico. Si el DataTable no contiene elementos, la función retornará null.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static TEntity DataTableToEntity<TEntity>(DataTable dataTable) where TEntity : class
        {

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            string PropertyName = null;
            try
            {
                TEntity obj = Activator.CreateInstance<TEntity>();
                var properties = obj.GetType().GetProperties();

                var count = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    if (count >= 1)
                        break;

                    foreach (PropertyInfo prop in properties)
                    {
                        if (dataTable.Columns.Contains(prop.Name))
                        {
                            PropertyName = prop.Name;
                            if (!object.Equals(row[prop.Name], DBNull.Value))
                                prop.SetValue(obj, row[prop.Name]);
                        }
                    }
                    count++;
                }
                properties = null;
                PropertyName = null;
                if (count != 1)
                    obj = default;

                return obj;
            }
            catch (Exception ex)
            {
                var message = $"PropertyName '{PropertyName}' : {ex.Message}";
                throw new Exception(message, ex);
            }

        }

        /// <summary>
        /// Convierte un DataTable en una lista genérica. Si el DataTable no contiene elementos, la función retornará null.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<TEntity> DataTableToList<TEntity>(DataTable dataTable) where TEntity : class
        {

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            string PropertyName = null;
            try
            {
                List<TEntity> list = new List<TEntity>();
                TEntity obj = Activator.CreateInstance<TEntity>();
                var properties = obj.GetType().GetProperties();

                foreach (DataRow row in dataTable.Rows)
                {
                    obj = Activator.CreateInstance<TEntity>();
                    foreach (PropertyInfo prop in properties)
                    {
                        if (dataTable.Columns.Contains(prop.Name))
                        {
                            PropertyName = prop.Name;
                            if (!object.Equals(row[prop.Name], DBNull.Value))
                                prop.SetValue(obj, row[prop.Name], null);
                        }
                    }

                    list.Add(obj);
                }

                properties = null;
                PropertyName = null;
                if (list.Count == 0)
                    list = null;

                return list;
            }
            catch (Exception ex)
            {
                var message = $"PropertyName '{PropertyName}' : {ex.Message}";
                throw new Exception(message, ex);
            }

        }

        /// <summary>
        /// Convierte un DataTable en una cadena de string con saltos de línea. 
        /// Si el DataTable no contiene elementos, la función retornará null.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="titulo"></param>
        /// <param name="delimitador">Caracter delimitador que utiliza para exportar el texto.</param>
        /// <returns></returns>
        public static string DataTableToString(DataTable dataTable, string titulo, string delimitador)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(titulo))
                sb.AppendLine(titulo);

            List<string> cabecera = new List<string>();
            foreach (DataColumn column in dataTable.Columns)
                cabecera.Add(column.ColumnName);

            sb.AppendJoin(delimitador, cabecera).AppendLine();
            foreach (DataRow row in dataTable.Rows)
                sb.AppendJoin(delimitador, row.ItemArray).AppendLine();

            return sb.ToString();
        }

        /// <summary>
        /// Convierte un string representado en XML a un Entity. Si el parametro es vacio la función retornará null.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de objeto a convertir</typeparam>
        /// <param name="stringXml"></param>
        /// <returns></returns>
        public static TEntity StringXmlToEntity<TEntity>(string stringXml) where TEntity : class
        {
            if (!string.IsNullOrWhiteSpace(stringXml))
                return null;

            XmlSerializer ser = new XmlSerializer(typeof(TEntity));
            using StringReader sr = new StringReader(stringXml);
            return (TEntity)ser.Deserialize(sr);
        }

        /// <summary>
        /// Convierte un string representado en XML a un DataSet. Si el parametro es vacio la función retornará null.
        /// </summary>
        /// <param name="stringXml"></param>
        /// <returns></returns>
        public static DataSet StringXmlToDataSet(string stringXml)
        {
            using StringReader xmlSR = new StringReader(stringXml);
            DataSet ds = new DataSet();
            ds.ReadXml(xmlSR);
            return ds;
        }

        /// <summary>
        /// Convierte un objeto a un string expresado en JSON.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string ObjectToJson<TEntity>(TEntity entity) where TEntity : class
        {
            var settings = new JsonSerializerSettings
            {
                //ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ContractResolver = new DefaultContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            var vResult = JsonConvert.SerializeObject(entity, settings);
            return vResult;

        }

        /// <summary>
        /// Convierte un string expresado en JSON a Objeto.
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static TObject JsonToObject<TObject>(string jsonString) where TObject : class
        {
            var settings = new JsonSerializerSettings
            {
                //ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ContractResolver = new DefaultContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            var vResult = JsonConvert.DeserializeObject<TObject>(jsonString, settings);
            return vResult;

        }

        /// <summary>
        /// Convierte una lista genérica en un DataTable, si la lista no contiene elementos, la función retornará null.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="listEntity"></param>
        /// <param name="members">Nombres de las propiedades que se van a exportar.</param>
        /// <returns></returns>
        public static DataTable ListEntityToDataTable<TEntity>(IList<TEntity> listEntity, params string[] members) where TEntity : class
        {

            if (listEntity == null || listEntity.Count == 0)
                return null;

            DataTable table = new DataTable();
            if (members != null && members.Length > 0)
            {
                using var reader = ObjectReader.Create(listEntity, members);
                table.Load(reader);
                return table;
            }
            else
            {

                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(TEntity));
                foreach (PropertyDescriptor prop in properties)
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

                foreach (TEntity item in listEntity)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    table.Rows.Add(row);
                }
                properties.Clear();
            }

            return table;
        }

        /// <summary>
        /// Convierte una lista genérica en un DataReader, los nombres de las columnas estan ordenadas alfabeticamente. 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="listEntity"></param>
        /// <param name="members">Nombres de las propiedades que se van a exportar.</param>
        /// <returns></returns>
        public static DbDataReader ListEntityToDataReader<TEntity>(IList<TEntity> listEntity, params string[] members) where TEntity : class
        {
            var reader = ObjectReader.Create(listEntity, members);
            return reader;
        }

        /// <summary>
        /// Convierte una cadena de base64String a un byte[], si la cadena no es tiene el formato correcto, retornará null.
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns>Retorna un arreglo de byte[]</returns>
        public static byte[] FromBase64String(string base64String)
        {
            try
            {
                var vResult = Convert.FromBase64String(base64String);
                return vResult;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }

}
