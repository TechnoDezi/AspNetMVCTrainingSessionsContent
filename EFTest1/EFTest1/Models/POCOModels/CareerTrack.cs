using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public class CareerTrack : BusinessBase
    {
        #region Properties

        public int CareerTrackID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedCareerTrack
        /// </summary>
        /// <returns></returns>
        public void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetCareerTrackDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("CareerTrackID", SqlDbType.Int, CareerTrackID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    CareerTrackID = dt.GetDataCellValue(0, "CareerTrackID").ToInt32();
                    Name = dt.GetDataCellValue(0, "Name");
                    Description = dt.GetDataCellValue(0, "Description");
                    Code = dt.GetDataCellValue(0, "Code");
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.CareerTrack.SelectCareerTrackDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all CareerTrack for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<CareerTrack> GetListSearch(string searchValue)
        {
            List<CareerTrack> list = new List<CareerTrack>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetCareerTrackListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new CareerTrack
                            {
                                CareerTrackID = d.Field<int>("CareerTrackID"),
                                Name = d.Field<string>("Name"),
                                Description = d.Field<string>("Description"),
                                Code = d.Field<string>("Code"),
                                
                            }).ToList<CareerTrack>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.CareerTrack.SelectCareerTrackListSearch");
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
        /// Inserts or updates the specified CareerTrack
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdateCareerTrack", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("CareerTrackID", SqlDbType.Int, CareerTrackID),
                    SQL.SQLParameter("Name", SqlDbType.VarChar, Name),
                    SQL.SQLParameter("Description", SqlDbType.VarChar, Description),
                    SQL.SQLParameter("Code", SqlDbType.VarChar, Code),
                    SQL.SQLParameter("CareerTrackIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["CareerTrackIDOut"].Value != DBNull.Value)
                        {
                            CareerTrackID = (int)sqlManager.CurrentCommand.Parameters["CareerTrackIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.CareerTrack.InsertUpdateCareerTrack");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified CareerTrack
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeleteCareerTrack", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("CareerTrackID", SqlDbType.Int, CareerTrackID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.CareerTrack.DeleteCareerTrack");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}