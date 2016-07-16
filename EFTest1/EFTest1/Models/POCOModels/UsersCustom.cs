using KooBoo.Framework.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using KooBoo.Framework;

namespace EFTest1.Models
{
    public partial class Users : BusinessBase
    {
        public bool ChechUserAuth()
        {
            bool isVerified = false;

            try
            {
                sqlManager.ExcecuteNonQuery("ChechUserAuth", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("Email", SqlDbType.VarChar, Email),
                    SQL.SQLParameter("Password", SqlDbType.VarChar, Password),
                    SQL.SQLParameter("IsVerified", SqlDbType.Bit, 1, ParameterDirection.Output)
                });

                if (sqlManager.CurrentCommand != null)
                {
                    if (sqlManager.CurrentCommand.Parameters["IsVerified"].Value != DBNull.Value)
                    {
                        isVerified = (bool)sqlManager.CurrentCommand.Parameters["IsVerified"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Users.ChechUserAuth");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }

            return isVerified;
        }
    }
}