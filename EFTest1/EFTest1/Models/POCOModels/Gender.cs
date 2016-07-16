using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public class Gender : BusinessBase
    {
        #region Properties

        public int GenderID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedGender
        /// </summary>
        /// <returns></returns>
        public void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetGenderDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("GenderID", SqlDbType.Int, GenderID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    GenderID = dt.GetDataCellValue(0, "GenderID").ToInt32();
                    Name = dt.GetDataCellValue(0, "Name");
                    Description = dt.GetDataCellValue(0, "Description");
                    Code = dt.GetDataCellValue(0, "Code");
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Gender.SelectGenderDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all Gender for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<Gender> GetListSearch(string searchValue)
        {
            List<Gender> list = new List<Gender>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetGenderListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new Gender
                            {
                                GenderID = d.Field<int>("GenderID"),
                                Name = d.Field<string>("Name"),
                                Description = d.Field<string>("Description"),
                                Code = d.Field<string>("Code"),
                                
                            }).ToList<Gender>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Gender.SelectGenderListSearch");
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
        /// Inserts or updates the specified Gender
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdateGender", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("GenderID", SqlDbType.Int, GenderID),
                    SQL.SQLParameter("Name", SqlDbType.VarChar, Name),
                    SQL.SQLParameter("Description", SqlDbType.VarChar, Description),
                    SQL.SQLParameter("Code", SqlDbType.VarChar, Code),
                    SQL.SQLParameter("GenderIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["GenderIDOut"].Value != DBNull.Value)
                        {
                            GenderID = (int)sqlManager.CurrentCommand.Parameters["GenderIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Gender.InsertUpdateGender");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified Gender
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeleteGender", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("GenderID", SqlDbType.Int, GenderID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Gender.DeleteGender");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}