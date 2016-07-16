using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public class OfficeLocation : BusinessBase
    {
        #region Properties

        public int OfficeLocationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedOfficeLocation
        /// </summary>
        /// <returns></returns>
        public void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetOfficeLocationDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("OfficeLocationID", SqlDbType.Int, OfficeLocationID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    OfficeLocationID = dt.GetDataCellValue(0, "OfficeLocationID").ToInt32();
                    Name = dt.GetDataCellValue(0, "Name");
                    Description = dt.GetDataCellValue(0, "Description");
                    Code = dt.GetDataCellValue(0, "Code");
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.OfficeLocation.SelectOfficeLocationDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all OfficeLocation for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<OfficeLocation> GetListSearch(string searchValue)
        {
            List<OfficeLocation> list = new List<OfficeLocation>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetOfficeLocationListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new OfficeLocation
                            {
                                OfficeLocationID = d.Field<int>("OfficeLocationID"),
                                Name = d.Field<string>("Name"),
                                Description = d.Field<string>("Description"),
                                Code = d.Field<string>("Code"),
                                
                            }).ToList<OfficeLocation>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.OfficeLocation.SelectOfficeLocationListSearch");
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
        /// Inserts or updates the specified OfficeLocation
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdateOfficeLocation", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("OfficeLocationID", SqlDbType.Int, OfficeLocationID),
                    SQL.SQLParameter("Name", SqlDbType.VarChar, Name),
                    SQL.SQLParameter("Description", SqlDbType.VarChar, Description),
                    SQL.SQLParameter("Code", SqlDbType.VarChar, Code),
                    SQL.SQLParameter("OfficeLocationIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["OfficeLocationIDOut"].Value != DBNull.Value)
                        {
                            OfficeLocationID = (int)sqlManager.CurrentCommand.Parameters["OfficeLocationIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.OfficeLocation.InsertUpdateOfficeLocation");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified OfficeLocation
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeleteOfficeLocation", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("OfficeLocationID", SqlDbType.Int, OfficeLocationID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.OfficeLocation.DeleteOfficeLocation");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}