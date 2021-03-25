# Reec
Proyecto de utilitarios para realizar conversiones de tipos de datos génericos, obtener nombres de ContentType apartir de FileName, conexión a base de datos SqlServer, Oracle, MySql, etc.

## Comenzando 🚀

_Estas instrucciones te permitirán obtener una copia del proyecto en funcionamiento en tu máquina local._


### Pre-requisitos 📋
```PowerShell
Install-Package Reec.Helpers
Install-Package Reec.MySql
Install-Package Reec.Oracle
Install-Package Reec.SqlServer
```

### Uso del paquete Helpers 🔧
```csharp
using Reec.Helpers;
.
.
.
// Convierte un DataReader en un objeto genérico. Si el número de filas es diferente a 1, retornará null.
var entity = HelperConvert.DataReaderToEntity<TEntity>(dataReader);

//Convierte un DataReader a una lista genérica, si no hay ninguna propiedad que haga match, retornará null.
var listEntity = HelperConvert.DataReaderToList<TEntity>(dataReader);

//Convierte un dataReader genérico a DataTable
var dataTable = HelperConvert.DataReaderToDataTable(dataReader);

var dataSet = HelperConvert.DataReaderToDataSet(dataReader);

//Convierte una lista genérica en un DataTable
var dataTable = HelperConvert.ListEntityToDataTable(listEntity);
var dataTable1 = HelperConvert.ListEntityToDataTable(listEntity, "propiedad1", "propiedad2", "propiedad3");


//El valor de contentType = "application/pdf"
var contentTypePdf = HelperContentType.GetContentType("test.pdf");

//El valor de contentTypeXlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
var contentTypeXlsx = HelperContentType.GetContentType("test1.xlsx");
```


### Uso del paquete SqlServer 🔧

```csharp
using Reec.SqlServer;
.
.
.
using ReecSqlServer reecSqlServer = new ReecSqlServer("cadena de conexión");
var dataTable = reecSqlServer.QueryToDataTable("select * from Table1");

//Ejecuta un script de sql y retorna el número de las filas afectadas. Insert, Delete, Update
var rowsAffected = reecSqlServer.QueryRowsAffected("update [Tabla1] set columna1 = 'prueba' where Id = 1");


//Ejecuta un StoreProcedure y retorna el número de filas afectadas, envio de parametros nativos
var rowsAffected1 = reecSqlServer.ExecuteNonQuery("Name_StoreProcedure",
                       new SqlParameter("@param1", 1),
                       new SqlParameter("@param2", "prueba SP 3"));


//Ejecuta un StoreProcedure y retorna el número de filas afectadas.
var rowsAffected2 = reecSqlServer.ExecuteNonQuery("Name_StoreProcedure");

//Ejecuta un StoreProcedure y retorna un DataReader.
var dataReader = reecSqlServer.ExecuteReader("Name_StoreProcedure");

//Ejecuta un StoreProcedure y retorna un ListEntity. Si el StoreProcedure no retorna filas, la función retornara null.
var listEntity = reecSqlServer.ExecuteToListEntity<TEntity>("Name_StoreProcedure");



///Uso de transacciones, se recomienda usar using al declarar la variable.
using var t = reecSqlServer.BeginTransaccion();
/*
  código insert ..
  código delete ..
  código update ..
  código select ..
*/
if(condicion)//alguna regla de negocio para ejecutar el Rollback, si ocurre una excepción, el rollback se ejecuta en automático.
{
  t.Rollback();
}

t.Commit();
```


