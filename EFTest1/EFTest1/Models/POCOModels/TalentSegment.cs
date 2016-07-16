using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public class TalentSegment : BusinessBase
    {
        #region Properties

        public int TalentSegmentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedTalentSegment
        /// </summary>
        /// <returns></returns>
        public void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetTalentSegmentDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("TalentSegmentID", SqlDbType.Int, TalentSegmentID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    TalentSegmentID = dt.GetDataCellValue(0, "TalentSegmentID").ToInt32();
                    Name = dt.GetDataCellValue(0, "Name");
                    Description = dt.GetDataCellValue(0, "Description");
                    Code = dt.GetDataCellValue(0, "Code");
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.TalentSegment.SelectTalentSegmentDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all TalentSegment for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<TalentSegment> GetListSearch(string searchValue)
        {
            List<TalentSegment> list = new List<TalentSegment>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetTalentSegmentListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new TalentSegment
                            {
                                TalentSegmentID = d.Field<int>("TalentSegmentID"),
                                Name = d.Field<string>("Name"),
                                Description = d.Field<string>("Description"),
                                Code = d.Field<string>("Code"),
                                
                            }).ToList<TalentSegment>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.TalentSegment.SelectTalentSegmentListSearch");
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
        /// Inserts or updates the specified TalentSegment
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdateTalentSegment", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("TalentSegmentID", SqlDbType.Int, TalentSegmentID),
                    SQL.SQLParameter("Name", SqlDbType.VarChar, Name),
                    SQL.SQLParameter("Description", SqlDbType.VarChar, Description),
                    SQL.SQLParameter("Code", SqlDbType.VarChar, Code),
                    SQL.SQLParameter("TalentSegmentIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["TalentSegmentIDOut"].Value != DBNull.Value)
                        {
                            TalentSegmentID = (int)sqlManager.CurrentCommand.Parameters["TalentSegmentIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.TalentSegment.InsertUpdateTalentSegment");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified TalentSegment
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeleteTalentSegment", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("TalentSegmentID", SqlDbType.Int, TalentSegmentID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.TalentSegment.DeleteTalentSegment");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}