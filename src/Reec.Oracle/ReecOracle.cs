using Oracle.ManagedDataAccess.Client;
using Reec.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reec.MySql
{
    public class ReecOracle : DbBase<OracleConnection, OracleCommand, OracleParameter, OracleTransaction>, IReecOracle
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection">Cadena de conexión de Base de Datos.</param>
        /// <param name="commandTimeOut">Tiempo expresado en segundos, por defecto 30 segundos.</param>
        public ReecOracle(string connection, int commandTimeOut = 30) 
            : base(connection, commandTimeOut)
        {
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction">Inicia un ambiente de transacción.</param>
        /// <param name="commandTimeOut">Tiempo expresado en segundos, por defecto 30 segundos.</param>
        public ReecOracle(OracleTransaction transaction, int commandTimeOut = 30)
            : base(transaction, commandTimeOut)
        {
        
        }

    }

}
