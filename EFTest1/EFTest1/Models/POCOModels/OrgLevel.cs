using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public class OrgLevel : BusinessBase
    {
        #region Properties

        public int OrgLevelID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedOrgLevel
        /// </summary>
        /// <returns></returns>
        public void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetOrgLevelDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("OrgLevelID", SqlDbType.Int, OrgLevelID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    OrgLevelID = dt.GetDataCellValue(0, "OrgLevelID").ToInt32();
                    Name = dt.GetDataCellValue(0, "Name");
                    Description = dt.GetDataCellValue(0, "Description");
                    Code = dt.GetDataCellValue(0, "Code");
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.OrgLevel.SelectOrgLevelDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all OrgLevel for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<OrgLevel> GetListSearch(string searchValue)
        {
            List<OrgLevel> list = new List<OrgLevel>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetOrgLevelListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new OrgLevel
                            {
                                OrgLevelID = d.Field<int>("OrgLevelID"),
                                Name = d.Field<string>("Name"),
                                Description = d.Field<string>("Description"),
                                Code = d.Field<string>("Code"),
                                
                            }).ToList<OrgLevel>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.OrgLevel.SelectOrgLevelListSearch");
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
        /// Inserts or updates the specified OrgLevel
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdateOrgLevel", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("OrgLevelID", SqlDbType.Int, OrgLevelID),
                    SQL.SQLParameter("Name", SqlDbType.VarChar, Name),
                    SQL.SQLParameter("Description", SqlDbType.VarChar, Description),
                    SQL.SQLParameter("Code", SqlDbType.VarChar, Code),
                    SQL.SQLParameter("OrgLevelIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["OrgLevelIDOut"].Value != DBNull.Value)
                        {
                            OrgLevelID = (int)sqlManager.CurrentCommand.Parameters["OrgLevelIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.OrgLevel.InsertUpdateOrgLevel");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified OrgLevel
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeleteOrgLevel", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("OrgLevelID", SqlDbType.Int, OrgLevelID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.OrgLevel.DeleteOrgLevel");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}