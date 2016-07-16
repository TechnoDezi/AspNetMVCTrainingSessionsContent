using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public class Request : BusinessBase
    {
        #region Properties

        public int RequestID { get; set; }
        public int PersonelID { get; set; }
        public int RequestStatusID { get; set; }
        public int UserID { get; set; }
        public DateTime RequestDate { get; set; }
        
        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedRequest
        /// </summary>
        /// <returns></returns>
        public async void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetRequestDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("RequestID", SqlDbType.Int, RequestID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    RequestID = dt.GetDataCellValue(0, "RequestID").ToInt32();
                    PersonelID = dt.GetDataCellValue(0, "PersonelID").ToInt32();
                    RequestStatusID = dt.GetDataCellValue(0, "RequestStatusID").ToInt32();
                    UserID = dt.GetDataCellValue(0, "UserID").ToInt32();
                    RequestDate = dt.GetDataCellValue(0, "RequestDate").ToDateTime();
                    
                }
            }
            catch (Exception ex)
            {
                await LogError(ex.Message, ex, "EFTest1.Models.Request.SelectRequestDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all Request for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<Request> GetListSearch(string searchValue)
        {
            List<Request> list = new List<Request>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetRequestListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new Request
                            {
                                RequestID = d.Field<int>("RequestID"),
                                PersonelID = d.Field<int>("PersonelID"),
                                RequestStatusID = d.Field<int>("RequestStatusID"),
                                UserID = d.Field<int>("UserID"),
                                RequestDate = d.Field<DateTime>("RequestDate"),
                                
                            }).ToList<Request>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Request.SelectRequestListSearch");
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
        /// Inserts or updates the specified Request
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdateRequest", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("RequestID", SqlDbType.Int, RequestID),
                    SQL.SQLParameter("PersonelID", SqlDbType.Int, PersonelID),
                    SQL.SQLParameter("RequestStatusID", SqlDbType.Int, RequestStatusID),
                    SQL.SQLParameter("UserID", SqlDbType.Int, UserID),
                    SQL.SQLParameter("RequestDate", SqlDbType.DateTime, RequestDate),
                    SQL.SQLParameter("RequestIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["RequestIDOut"].Value != DBNull.Value)
                        {
                            RequestID = (int)sqlManager.CurrentCommand.Parameters["RequestIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Request.InsertUpdateRequest");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified Request
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeleteRequest", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("RequestID", SqlDbType.Int, RequestID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Request.DeleteRequest");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}