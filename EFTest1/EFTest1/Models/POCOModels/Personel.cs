using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using KooBoo.Framework;
using KooBoo.Framework.Data;

namespace EFTest1.Models
{
    public class Personel : BusinessBase
    {
        #region Properties

        public int PersonelID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int TitleID { get; set; }
        public int GenderID { get; set; }
        public int EthinicOriginID { get; set; }
        public string PersonelNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime HireDate { get; set; }
        public int CareerTrackID { get; set; }
        public int CareerLevelID { get; set; }
        public int PersonelRoleID { get; set; }
        public int TalentSegmentID { get; set; }
        public int OrgLevelID { get; set; }
        public int CapabilityID { get; set; }
        public int OfficeLocationID { get; set; }
        public int ContractorConversionID { get; set; }
        
        #endregion

        #region Select Methods

        /// <summary>
        /// Selects the details for the specifiedPersonel
        /// </summary>
        /// <returns></returns>
        public void GetDetails()
        {
            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetPersonelDetails", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("PersonelID", SqlDbType.Int, PersonelID)
                    
                });

                if (dt.Rows.Count > 0)
                {
                    PersonelID = dt.GetDataCellValue(0, "PersonelID").ToInt32();
                    Name = dt.GetDataCellValue(0, "Name");
                    Surname = dt.GetDataCellValue(0, "Surname");
                    TitleID = dt.GetDataCellValue(0, "TitleID").ToInt32();
                    GenderID = dt.GetDataCellValue(0, "GenderID").ToInt32();
                    EthinicOriginID = dt.GetDataCellValue(0, "EthinicOriginID").ToInt32();
                    PersonelNumber = dt.GetDataCellValue(0, "PersonelNumber");
                    StartDate = dt.GetDataCellValue(0, "StartDate").ToDateTime();
                    HireDate = dt.GetDataCellValue(0, "HireDate").ToDateTime();
                    CareerTrackID = dt.GetDataCellValue(0, "CareerTrackID").ToInt32();
                    CareerLevelID = dt.GetDataCellValue(0, "CareerLevelID").ToInt32();
                    PersonelRoleID = dt.GetDataCellValue(0, "PersonelRoleID").ToInt32();
                    TalentSegmentID = dt.GetDataCellValue(0, "TalentSegmentID").ToInt32();
                    OrgLevelID = dt.GetDataCellValue(0, "OrgLevelID").ToInt32();
                    CapabilityID = dt.GetDataCellValue(0, "CapabilityID").ToInt32();
                    OfficeLocationID = dt.GetDataCellValue(0, "OfficeLocationID").ToInt32();
                    ContractorConversionID = dt.GetDataCellValue(0, "ContractorConversionID").ToInt32();
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Personel.SelectPersonelDetails");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        /// <summary>
        /// Selects a list of all Personel for the search criteria
        /// </summary>
        /// <returns></returns>
        public List<Personel> GetListSearch(string searchValue)
        {
            List<Personel> list = new List<Personel>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("GetPersonelListSearch", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new Personel
                            {
                                PersonelID = d.Field<int>("PersonelID"),
                                Name = d.Field<string>("Name"),
                                Surname = d.Field<string>("Surname"),
                                TitleID = d.Field<int>("TitleID"),
                                GenderID = d.Field<int>("GenderID"),
                                EthinicOriginID = d.Field<int>("EthinicOriginID"),
                                PersonelNumber = d.Field<string>("PersonelNumber"),
                                StartDate = d.Field<DateTime>("StartDate"),
                                HireDate = d.Field<DateTime>("HireDate"),
                                CareerTrackID = d.Field<int>("CareerTrackID"),
                                CareerLevelID = d.Field<int>("CareerLevelID"),
                                PersonelRoleID = d.Field<int>("PersonelRoleID"),
                                TalentSegmentID = d.Field<int>("TalentSegmentID"),
                                OrgLevelID = d.Field<int>("OrgLevelID"),
                                CapabilityID = d.Field<int>("CapabilityID"),
                                OfficeLocationID = d.Field<int>("OfficeLocationID"),
                                ContractorConversionID = d.Field<int>("ContractorConversionID"),
                                
                            }).ToList<Personel>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Personel.SelectPersonelListSearch");
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
        /// Inserts or updates the specified Personel
        /// </summary>
        public void AddUpdate()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("AddUpdatePersonel", CommandType.StoredProcedure, new List<SqlParameter>()
                {
                    SQL.SQLParameter("PersonelID", SqlDbType.Int, PersonelID),
                    SQL.SQLParameter("Name", SqlDbType.VarChar, Name),
                    SQL.SQLParameter("Surname", SqlDbType.VarChar, Surname),
                    SQL.SQLParameter("TitleID", SqlDbType.Int, TitleID),
                    SQL.SQLParameter("GenderID", SqlDbType.Int, GenderID),
                    SQL.SQLParameter("EthinicOriginID", SqlDbType.Int, EthinicOriginID),
                    SQL.SQLParameter("PersonelNumber", SqlDbType.VarChar, PersonelNumber),
                    SQL.SQLParameter("StartDate", SqlDbType.DateTime, StartDate),
                    SQL.SQLParameter("HireDate", SqlDbType.DateTime, HireDate),
                    SQL.SQLParameter("CareerTrackID", SqlDbType.Int, CareerTrackID),
                    SQL.SQLParameter("CareerLevelID", SqlDbType.Int, CareerLevelID),
                    SQL.SQLParameter("PersonelRoleID", SqlDbType.Int, PersonelRoleID),
                    SQL.SQLParameter("TalentSegmentID", SqlDbType.Int, TalentSegmentID),
                    SQL.SQLParameter("OrgLevelID", SqlDbType.Int, OrgLevelID),
                    SQL.SQLParameter("CapabilityID", SqlDbType.Int, CapabilityID),
                    SQL.SQLParameter("OfficeLocationID", SqlDbType.Int, OfficeLocationID),
                    SQL.SQLParameter("ContractorConversionID", SqlDbType.Int, ContractorConversionID),
                    SQL.SQLParameter("PersonelIDOut", SqlDbType.Int, 4, ParameterDirection.Output)
                    
                });

                //Set output values
                if (sqlManager.CurrentCommand != null)
                {
                    
                        if (sqlManager.CurrentCommand.Parameters["PersonelIDOut"].Value != DBNull.Value)
                        {
                            PersonelID = (int)sqlManager.CurrentCommand.Parameters["PersonelIDOut"].Value;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Personel.InsertUpdatePersonel");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified Personel
        /// </summary>
        public void Delete()
        {
            try
            {
                sqlManager.ExcecuteNonQuery("DeletePersonel", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("PersonelID", SqlDbType.Int, PersonelID)
                    
                });
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.Personel.DeletePersonel");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }
        }

        #endregion
    }
}