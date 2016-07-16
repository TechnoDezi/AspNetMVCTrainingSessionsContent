using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public partial class Capability : BusinessBase
    {
        #region Properties

        public int CapabilityID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedCapability
        /// </summary>
        /// <returns></returns>
        public void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetCapabilityDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("CapabilityID", SqlDbType.Int, CapabilityID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    CapabilityID = dt.GetDataCellValue(0, "CapabilityID").ToInt32();
                    Name = dt.GetDataCellValue(0, "Name");
                    Description = dt.GetDataCellValue(0, "Description");
                    Code = dt.GetDataCellValue(0, "Code");
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Capability.SelectCapabilityDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all Capability for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<Capability> GetListSearch(string searchValue)
        {
            List<Capability> list = new List<Capability>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetCapabilityListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new Capability
                            {
                                CapabilityID = d.Field<int>("CapabilityID"),
                                Name = d.Field<string>("Name"),
                                Description = d.Field<string>("Description"),
                                Code = d.Field<string>("Code"),
                                
                            }).ToList<Capability>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Capability.SelectCapabilityListSearch");
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
        /// Inserts or updates the specified Capability
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdateCapability", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("CapabilityID", SqlDbType.Int, CapabilityID),
                    SQL.SQLParameter("Name", SqlDbType.VarChar, Name),
                    SQL.SQLParameter("Description", SqlDbType.VarChar, Description),
                    SQL.SQLParameter("Code", SqlDbType.VarChar, Code),
                    SQL.SQLParameter("CapabilityIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["CapabilityIDOut"].Value != DBNull.Value)
                        {
                            CapabilityID = (int)sqlManager.CurrentCommand.Parameters["CapabilityIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Capability.InsertUpdateCapability");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified Capability
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeleteCapability", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("CapabilityID", SqlDbType.Int, CapabilityID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Capability.DeleteCapability");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}