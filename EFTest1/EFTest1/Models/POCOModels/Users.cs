using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public partial class Users : BusinessBase
    {
        #region Properties

        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedUsers
        /// </summary>
        /// <returns></returns>
        public void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetUsersDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("UserID", SqlDbType.Int, UserID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    UserID = dt.GetDataCellValue(0, "UserID").ToInt32();
                    Email = dt.GetDataCellValue(0, "Email");
                    Password = dt.GetDataCellValue(0, "Password");
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Users.SelectUsersDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all Users for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<Users> GetListSearch(string searchValue)
        {
            List<Users> list = new List<Users>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetUsersListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new Users
                            {
                                UserID = d.Field<int>("UserID"),
                                Email = d.Field<string>("Email"),
                                Password = d.Field<string>("Password"),
                                
                            }).ToList<Users>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Users.SelectUsersListSearch");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }

            return list;
        }

        #endregion

        #region Insert Update Methods

        /// <summary>
        /// Inserts or updates the specified Users
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdateUsers", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("UserID", SqlDbType.Int, UserID),
                    SQL.SQLParameter("Email", SqlDbType.VarChar, Email),
                    SQL.SQLParameter("Password", SqlDbType.VarChar, Password),
                    SQL.SQLParameter("UserIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["UserIDOut"].Value != DBNull.Value)
                        {
                            UserID = (int)sqlManager.CurrentCommand.Parameters["UserIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Users.InsertUpdateUsers");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified Users
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeleteUsers", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("UserID", SqlDbType.Int, UserID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Users.DeleteUsers");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}