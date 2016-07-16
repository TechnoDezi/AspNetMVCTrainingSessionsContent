using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public class ContractorConversion : BusinessBase
    {
        #region Properties

        public int ContractorConversionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedContractorConversion
        /// </summary>
        /// <returns></returns>
        public void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetContractorConversionDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("ContractorConversionID", SqlDbType.Int, ContractorConversionID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    ContractorConversionID = dt.GetDataCellValue(0, "ContractorConversionID").ToInt32();
                    Name = dt.GetDataCellValue(0, "Name");
                    Description = dt.GetDataCellValue(0, "Description");
                    Code = dt.GetDataCellValue(0, "Code");
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.ContractorConversion.SelectContractorConversionDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all ContractorConversion for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<ContractorConversion> GetListSearch(string searchValue)
        {
            List<ContractorConversion> list = new List<ContractorConversion>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetContractorConversionListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new ContractorConversion
                            {
                                ContractorConversionID = d.Field<int>("ContractorConversionID"),
                                Name = d.Field<string>("Name"),
                                Description = d.Field<string>("Description"),
                                Code = d.Field<string>("Code"),
                                
                            }).ToList<ContractorConversion>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.ContractorConversion.SelectContractorConversionListSearch");
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
        /// Inserts or updates the specified ContractorConversion
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdateContractorConversion", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("ContractorConversionID", SqlDbType.Int, ContractorConversionID),
                    SQL.SQLParameter("Name", SqlDbType.VarChar, Name),
                    SQL.SQLParameter("Description", SqlDbType.VarChar, Description),
                    SQL.SQLParameter("Code", SqlDbType.VarChar, Code),
                    SQL.SQLParameter("ContractorConversionIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["ContractorConversionIDOut"].Value != DBNull.Value)
                        {
                            ContractorConversionID = (int)sqlManager.CurrentCommand.Parameters["ContractorConversionIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.ContractorConversion.InsertUpdateContractorConversion");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified ContractorConversion
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeleteContractorConversion", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("ContractorConversionID", SqlDbType.Int, ContractorConversionID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.ContractorConversion.DeleteContractorConversion");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}