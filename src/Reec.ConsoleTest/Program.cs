using Microsoft.Data.SqlClient;
using Reec.SqlServer;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Reec.ConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            var connection = "Data Source=asus\\sql2019;Initial Catalog=Utility;Persist Security Info=True;User ID=sa;Password=Chump1taz; App=Utility.WebApi; Max Pool Size=300;";
            var query = "select * from [dbo].[Curso]";
            var sp = "USP_Curso_Sel";
            var update = "USP_Curso_Update";

            using ReecSqlServer reecSqlServer = new ReecSqlServer(connection);

            var cursos = reecSqlServer.QueryToListEntity<Curso>(query);
            var cursosAsync = await reecSqlServer.QueryToListEntityAsync<Curso>(query);

            var curso = reecSqlServer.QueryToEntity<Curso>(query);
            var cursoAsync = await reecSqlServer.QueryToEntityAsync<Curso>(query);

            var dataSet = reecSqlServer.QueryToDataSet(query);
            var dataSetAsync = await reecSqlServer.QueryToDataSetAsync(query);

            var dataTable = reecSqlServer.QueryToDataTable(query);
            var dataTableAsync = await reecSqlServer.QueryToDataTableAsync(query);

            var dataReader = reecSqlServer.QueryReader(query);
            var dt = Helpers.HelperConvert.DataReaderToDataTable(dataReader);

            var dataReaderAsync = await reecSqlServer.QueryReaderAsync(query);
            var dtAsync = Helpers.HelperConvert.DataReaderToDataTable(dataReaderAsync);



            var spCurso = reecSqlServer.ExecuteToEntity<Curso>(sp, new SqlParameter("@IdCurso", 1));
            var spCursoAsync = await reecSqlServer.ExecuteToEntityAsync<Curso>(sp, new SqlParameter("@IdCurso", 1));

            var spCursos = reecSqlServer.ExecuteToListEntity<Curso>(sp);
            var spCursosAsync = await reecSqlServer.ExecuteToListEntityAsync<Curso>(sp);
             
            var spDataTable = reecSqlServer.ExecuteToDataTable(sp, new SqlParameter("@IdCurso", 1));
            var spDataTableAsync = await reecSqlServer.ExecuteToDataTableAsync(sp, new SqlParameter("@IdCurso", 1));
             
            var spDataSet = reecSqlServer.ExecuteToDataSet(sp);
            var spDataSetAsync = await reecSqlServer.ExecuteToDataSetAsync(sp);

            var spDataReader = reecSqlServer.ExecuteReader(sp);
            var spDt = Helpers.HelperConvert.DataReaderToDataTable(spDataReader); 

            var spDataReaderAsync = await reecSqlServer.ExecuteReaderAsync(sp);
            var spDtAsync = Helpers.HelperConvert.DataReaderToDataTable(spDataReaderAsync);


            using var transaction = reecSqlServer.BeginTransaccion();
           
            var count = new SqlParameter("@count", System.Data.SqlDbType.Int) { 
                    Direction = System.Data.ParameterDirection.Output };
            var vResult = reecSqlServer.ExecuteNonQuery(update,
                                new SqlParameter("@IdCurso", 1),
                                new SqlParameter("@Nombre", "asp.net core MVC"),
                                new SqlParameter("@Activo", true),
                                count);

            var cursosT = reecSqlServer.QueryToListEntity<Curso>(query);
             
            transaction.Commit();


            Console.WriteLine($"tiempo: {stopwatch.Elapsed}");

            Console.WriteLine("Hello World!");

        }

    }
}
