using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public class RequestStatus : BusinessBase
    {
        #region Properties

        public int RequestStatusID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedRequestStatus
        /// </summary>
        /// <returns></returns>
        public void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetRequestStatusDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("RequestStatusID", SqlDbType.Int, RequestStatusID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    RequestStatusID = dt.GetDataCellValue(0, "RequestStatusID").ToInt32();
                    Name = dt.GetDataCellValue(0, "Name");
                    Description = dt.GetDataCellValue(0, "Description");
                    Code = dt.GetDataCellValue(0, "Code");
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.RequestStatus.SelectRequestStatusDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all RequestStatus for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<RequestStatus> GetListSearch(string searchValue)
        {
            List<RequestStatus> list = new List<RequestStatus>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetRequestStatusListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new RequestStatus
                            {
                                RequestStatusID = d.Field<int>("RequestStatusID"),
                                Name = d.Field<string>("Name"),
                                Description = d.Field<string>("Description"),
                                Code = d.Field<string>("Code"),
                                
                            }).ToList<RequestStatus>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.RequestStatus.SelectRequestStatusListSearch");
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
        /// Inserts or updates the specified RequestStatus
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdateRequestStatus", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("RequestStatusID", SqlDbType.Int, RequestStatusID),
                    SQL.SQLParameter("Name", SqlDbType.VarChar, Name),
                    SQL.SQLParameter("Description", SqlDbType.VarChar, Description),
                    SQL.SQLParameter("Code", SqlDbType.VarChar, Code),
                    SQL.SQLParameter("RequestStatusIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["RequestStatusIDOut"].Value != DBNull.Value)
                        {
                            RequestStatusID = (int)sqlManager.CurrentCommand.Parameters["RequestStatusIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.RequestStatus.InsertUpdateRequestStatus");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified RequestStatus
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeleteRequestStatus", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("RequestStatusID", SqlDbType.Int, RequestStatusID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.RequestStatus.DeleteRequestStatus");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}