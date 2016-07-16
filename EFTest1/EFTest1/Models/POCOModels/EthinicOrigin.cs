using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public class EthinicOrigin : BusinessBase
    {
        #region Properties

        public int EthinicOriginID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedEthinicOrigin
        /// </summary>
        /// <returns></returns>
        public void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetEthinicOriginDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("EthinicOriginID", SqlDbType.Int, EthinicOriginID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    EthinicOriginID = dt.GetDataCellValue(0, "EthinicOriginID").ToInt32();
                    Name = dt.GetDataCellValue(0, "Name");
                    Description = dt.GetDataCellValue(0, "Description");
                    Code = dt.GetDataCellValue(0, "Code");
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.EthinicOrigin.SelectEthinicOriginDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all EthinicOrigin for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<EthinicOrigin> GetListSearch(string searchValue)
        {
            List<EthinicOrigin> list = new List<EthinicOrigin>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetEthinicOriginListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new EthinicOrigin
                            {
                                EthinicOriginID = d.Field<int>("EthinicOriginID"),
                                Name = d.Field<string>("Name"),
                                Description = d.Field<string>("Description"),
                                Code = d.Field<string>("Code"),
                                
                            }).ToList<EthinicOrigin>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.EthinicOrigin.SelectEthinicOriginListSearch");
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
        /// Inserts or updates the specified EthinicOrigin
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdateEthinicOrigin", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("EthinicOriginID", SqlDbType.Int, EthinicOriginID),
                    SQL.SQLParameter("Name", SqlDbType.VarChar, Name),
                    SQL.SQLParameter("Description", SqlDbType.VarChar, Description),
                    SQL.SQLParameter("Code", SqlDbType.VarChar, Code),
                    SQL.SQLParameter("EthinicOriginIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["EthinicOriginIDOut"].Value != DBNull.Value)
                        {
                            EthinicOriginID = (int)sqlManager.CurrentCommand.Parameters["EthinicOriginIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.EthinicOrigin.InsertUpdateEthinicOrigin");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified EthinicOrigin
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeleteEthinicOrigin", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("EthinicOriginID", SqlDbType.Int, EthinicOriginID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.EthinicOrigin.DeleteEthinicOrigin");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}