# Reec
Paquetes para realizar conversiones de tipos de datos génericos, obtener tipo de ContentType apartir de un nombre de archivo. 
Alto rendimiento en conexión a base de datos de manera nativa y de fácil uso en SqlServer, Oracle, MySql.
Paquete de inspección de errores automáticos en WebApi para SqlServer, permite generar un Id de seguimiento.
Se agregaran más conexiones de base de datos próximamente.

## Comenzando 🚀

_Estas instrucciones te permitirán obtener una copia del proyecto en funcionamiento en tu máquina local._


### Pre-requisitos 📋
```PowerShell
Install-Package Reec.Helpers
Install-Package Reec.MySql
Install-Package Reec.Oracle
Install-Package Reec.SqlServer
Install-Package Reec.Inspection.SqlServer
```

### Uso del paquete Reec.Helpers 🔧
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


### Uso del paquete Reec.SqlServer 🔧

```csharp
using Reec.SqlServer;
.
.
.
using ReecSqlServer reecSqlServer = new ReecSqlServer("cadena de conexión");
var dataTable = reecSqlServer.QueryToDataTable("select * from Table1");

//Ejecuta un script de sql y retorna el número de las filas afectadas. Insert, Delete, Update
var rowsAffected = reecSqlServer.QueryRowsAffected("update [Tabla1] set columna1 = 'prueba' where Id = 1");


//Ejecuta un StoreProcedure y retorna el número de filas afectadas, envío de parametros nativos
var rowsAffected1 = reecSqlServer.ExecuteNonQuery("Name_StoreProcedure",
                       new SqlParameter("@param1", 1),
                       new SqlParameter("@param2", "prueba SP 3"));


//Ejecuta un StoreProcedure y retorna el número de filas afectadas.
var rowsAffected2 = reecSqlServer.ExecuteNonQuery("Name_StoreProcedure");

//Ejecuta un StoreProcedure y retorna un DataReader.
var dataReader = reecSqlServer.ExecuteReader("Name_StoreProcedure");

//Ejecuta un StoreProcedure y retorna un DataSet. Si el StoreProcedure no retorna filas, la función retorna null.
var dataSet = reecSqlServer.ExecuteToDataSet("Name_StoreProcedure")

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


### Uso del paquete Reec.Inspection.SqlServer 🔧
_Configuración del Startup._
```csharp
using Reec.Inspection.SqlServer;
public void ConfigureServices(IServiceCollection services)
{
    services.AddReecException<DbContextSqlServer>(options =>
                 options.UseSqlServer("cadena de conexión"));

}

 public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ///Debe ser el 1er middleware agregado para inspeccionar los log.
    app.UseReecException<DbContextSqlServer>();
}
```

_Formas de uso de error controlado._
```csharp
using static Reec.Inspection.ReecEnums; /// obtener enums de categorias de mensajes
.
.
[HttpGet]
public IActionResult TestWarning(string parameter)
{
    // Error controlado de validación de datos
    if (string.IsNullOrWhiteSpace(parameter))
        throw new ReecException(Category.Warning, "Campo 'parameter' obligatorio.");

    return Ok(parameter);
}

[HttpGet]
public IActionResult TestInternalServerError(string parameter)
{
    var numerador = 1;
    var denominador = 0;    
    // Error no controlado del sistema. produce un error 500 del servidor.
    // El error que retorna a la api: {"Id":3,"Path":"/weatherforecast/TestInternalServerError/1","TraceIdentifier":"8000001b-0002-ff00-b63f-84710c7967bb","Category":500,"CategoryDescription":"InternalServerError","Message":["Error no controlado del sistema."]}
    var division = numerador / denominador;
    return Ok(parameter);
}

public IActionResult TestBusinessLogic(string parameter)
{
    if (string.IsNullOrWhiteSpace(parameter))
        throw new ReecException(Category.BusinessLogic, "No cumple con la regla de negocio.");
    return Ok(parameter);
}

public IActionResult TestBusinessLogicLegacy(string parameter)
{
    try 
    {
        var numerador = 1;
        var denominador = 0;
        var division = numerador / denominador;
        return Ok(parameter);
    }
    catch (Exception)
    {
        throw new ReecException(Category.BusinessLogicLegacy, "Error no controlado del sistema legacy 'app1'.");
    }
}
```
