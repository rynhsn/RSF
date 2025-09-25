using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA00300
{
    public partial class ConnectToSQLCls
    {
        public string GetSQLVersion()
        {
            R_Exception loException = new R_Exception();
            DataTable loDatatable = null;
            DbConnection loDbConnection = null;
            string lcRtn = "";
            R_Db loDb = new R_Db();

            try
            {
                loDbConnection = loDb.GetConnection();
                loDatatable = loDb.SqlExecQuery("select @@version", loDbConnection, true);

                lcRtn = loDatatable.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loDbConnection != null)
                {
                    if (!(loDbConnection.State == ConnectionState.Closed))
                    {
                        loDbConnection.Close();
                        loDbConnection.Dispose();
                    }
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();

            return lcRtn;
        }
    }
}

