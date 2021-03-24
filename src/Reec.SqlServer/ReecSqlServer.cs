using Microsoft.Data.SqlClient;
using Reec.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reec.SqlServer
{
    public class ReecSqlServer : DbBase<SqlConnection, SqlCommand, SqlParameter, SqlTransaction>, IReecSqlServer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection">Cadena de conexión de Base de Datos.</param>
        /// <param name="commandTimeOut">Tiempo expresado en segundos, por defecto 30 segundos.</param>
        public ReecSqlServer(string connection, int commandTimeOut = 30) 
            : base(connection, commandTimeOut)
        {
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction">Inicia un ambiente de transacción.</param>
        /// <param name="commandTimeOut">Tiempo expresado en segundos, por defecto 30 segundos.</param>
        public ReecSqlServer(SqlTransaction transaction, int commandTimeOut = 30)
            : base(transaction, commandTimeOut)
        {
        
        }

    }

}
