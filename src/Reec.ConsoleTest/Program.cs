﻿using Reec.SqlServer;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reec.ConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {


            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            cancellationToken.CancelAfter(3000);

            var connection = "Data Source=asus\\sql2019;Initial Catalog=Utility;Persist Security Info=True;User ID=sa;Password=Chump1taz; App=Utility.WebApi; Max Pool Size=300;";
            var queryFull = "select * from HttpLog";

            using ReecSqlServer reecSqlServer = new ReecSqlServer(connection);
            using var t = reecSqlServer.BeginTransaccion();
             
            //var rowsAffected =  reecSqlServer.QueryRowsAffected("update [dbo].[HttpLog] set RequestHeader = 'Reec prueba' where Id = 1");

            var rowsAffected = await reecSqlServer.QueryRowsAffectedAsync("update [dbo].[HttpLog] set RequestHeader = 'Reec prueba 3' where Id = 1", cancellationToken.Token);

            var dt = reecSqlServer.QueryToDataTable(queryFull);

            t.Commit();


            Console.WriteLine("Hello World!");

        }
    }
}
