using KooBoo.Framework.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EFTest1.Models
{
    public class RequestSearch:BusinessBase
    {
        #region Properties

        public int RequestID { get; set; }
        public string RequestStatus { get; set; }
        public string PersonelName { get; set; }
        public string PersonelSurname { get; set; }
        public string PersonelTitle { get; set; }
        public string PersonelGender { get; set; }
        public string AssignedTo { get; set; }

        #endregion

        #region Get Methods

        public List<RequestSearch> GetSearchResults(string searchValue)
        {
            List<RequestSearch> list = new List<RequestSearch>();

            try
            {
                DataTable dt = sqlManager.ExcecuteDataTable("SearchRequests", CommandType.StoredProcedure, new List<SqlParameter>() {
                    SQL.SQLParameter("SearchValue", SqlDbType.VarChar, searchValue)
                });

                if (dt.Rows.Count > 0)
                {
                    list = (from d in dt.AsEnumerable()
                            select new RequestSearch
                            {
                                RequestID = d.Field<int>("RequestID"),
                                AssignedTo = d.Field<string>("AssignedTo"),
                                PersonelGender = d.Field<string>("Gender"),
                                PersonelName = d.Field<string>("Name"),
                                PersonelSurname = d.Field<string>("Surname"),
                                PersonelTitle = d.Field<string>("Title"),
                                RequestStatus = d.Field<string>("RequestStatus"),

                            }).ToList<RequestSearch>();
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex, "EFTest1.Models.RequestSearch.SearchRequests");
            }
            finally
            {
                sqlManager.CloseConnectionNoTransaction();
            }

            return list;
        }

        #endregion

    }
}