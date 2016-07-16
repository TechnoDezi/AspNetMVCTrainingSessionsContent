using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public class PersonelRole : BusinessBase
    {
        #region Properties

        public int PersonelRoleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedPersonelRole
        /// </summary>
        /// <returns></returns>
        public void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetPersonelRoleDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("PersonelRoleID", SqlDbType.Int, PersonelRoleID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    PersonelRoleID = dt.GetDataCellValue(0, "PersonelRoleID").ToInt32();
                    Name = dt.GetDataCellValue(0, "Name");
                    Description = dt.GetDataCellValue(0, "Description");
                    Code = dt.GetDataCellValue(0, "Code");
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.PersonelRole.SelectPersonelRoleDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all PersonelRole for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<PersonelRole> GetListSearch(string searchValue)
        {
            List<PersonelRole> list = new List<PersonelRole>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetPersonelRoleListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new PersonelRole
                            {
                                PersonelRoleID = d.Field<int>("PersonelRoleID"),
                                Name = d.Field<string>("Name"),
                                Description = d.Field<string>("Description"),
                                Code = d.Field<string>("Code"),
                                
                            }).ToList<PersonelRole>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.PersonelRole.SelectPersonelRoleListSearch");
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
        /// Inserts or updates the specified PersonelRole
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdatePersonelRole", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("PersonelRoleID", SqlDbType.Int, PersonelRoleID),
                    SQL.SQLParameter("Name", SqlDbType.VarChar, Name),
                    SQL.SQLParameter("Description", SqlDbType.VarChar, Description),
                    SQL.SQLParameter("Code", SqlDbType.VarChar, Code),
                    SQL.SQLParameter("PersonelRoleIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["PersonelRoleIDOut"].Value != DBNull.Value)
                        {
                            PersonelRoleID = (int)sqlManager.CurrentCommand.Parameters["PersonelRoleIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.PersonelRole.InsertUpdatePersonelRole");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified PersonelRole
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeletePersonelRole", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("PersonelRoleID", SqlDbType.Int, PersonelRoleID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.PersonelRole.DeletePersonelRole");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}