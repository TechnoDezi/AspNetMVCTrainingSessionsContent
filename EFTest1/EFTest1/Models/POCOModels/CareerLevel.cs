using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public class CareerLevel : BusinessBase
    {
        #region Properties

        public int CareerLevelID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedCareerLevel
        /// </summary>
        /// <returns></returns>
        public void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetCareerLevelDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("CareerLevelID", SqlDbType.Int, CareerLevelID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    CareerLevelID = dt.GetDataCellValue(0, "CareerLevelID").ToInt32();
                    Name = dt.GetDataCellValue(0, "Name");
                    Description = dt.GetDataCellValue(0, "Description");
                    Code = dt.GetDataCellValue(0, "Code");
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.CareerLevel.SelectCareerLevelDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all CareerLevel for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<CareerLevel> GetListSearch(string searchValue)
        {
            List<CareerLevel> list = new List<CareerLevel>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetCareerLevelListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new CareerLevel
                            {
                                CareerLevelID = d.Field<int>("CareerLevelID"),
                                Name = d.Field<string>("Name"),
                                Description = d.Field<string>("Description"),
                                Code = d.Field<string>("Code"),
                                
                            }).ToList<CareerLevel>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.CareerLevel.SelectCareerLevelListSearch");
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
        /// Inserts or updates the specified CareerLevel
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdateCareerLevel", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("CareerLevelID", SqlDbType.Int, CareerLevelID),
                    SQL.SQLParameter("Name", SqlDbType.VarChar, Name),
                    SQL.SQLParameter("Description", SqlDbType.VarChar, Description),
                    SQL.SQLParameter("Code", SqlDbType.VarChar, Code),
                    SQL.SQLParameter("CareerLevelIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["CareerLevelIDOut"].Value != DBNull.Value)
                        {
                            CareerLevelID = (int)sqlManager.CurrentCommand.Parameters["CareerLevelIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.CareerLevel.InsertUpdateCareerLevel");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified CareerLevel
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeleteCareerLevel", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("CareerLevelID", SqlDbType.Int, CareerLevelID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.CareerLevel.DeleteCareerLevel");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}