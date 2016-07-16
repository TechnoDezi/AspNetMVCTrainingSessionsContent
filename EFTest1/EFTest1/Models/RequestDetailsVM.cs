using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EFTest1.Helpers;

namespace EFTest1.Models
{
    public class RequestDetailsVM
    {
        public List<SelectListItem> RequestStatusList { get; set; }
        public List<SelectListItem> UsersList { get; set; }

        public int RequestID { get; set; }

        [Required]
        [Display(Name = "Request Status")]
        public string SelectedRequestStatusID { get; set; }
        [Required]
        [Display(Name = "Assigned To")]
        public string SelectedUserID { get; set; }
        [Required]
        [Display(Name = "Request Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime RequestDate { get; set; }

        public PersonelDetailsVM PersonelDetails { get; set; }

        internal async Task PopulateModel()
        {
            int personelID = 0;

            if (RequestID != 0)
            {
                Request request = new Request();
                request.RequestID = RequestID;
                request.GetDetails();

                SelectedRequestStatusID = request.RequestStatusID.ToString();
                SelectedUserID = request.UserID.ToString();
                RequestDate = request.RequestDate;
                personelID = request.PersonelID;
            }
            else
            {
                SelectedRequestStatusID = "0";
                SelectedUserID = "0";
                RequestDate = DateTime.Now;
            }

            PersonelDetails = new PersonelDetailsVM();
            await PersonelDetails.PopulateModel(personelID);
        }

        internal async Task PopulateLists()
        {
            var usersCacheList = CacheManagerHelper.GetCacheObject<List<Users>>("GetUsersListSearch",
                delegate
                {
                    Users users = new Users();
                    return users.GetListSearch("").ToList();
                });

            UsersList = (from l in usersCacheList
                         select new SelectListItem
                         {
                             Text = l.Email,
                             Value = l.UserID.ToString()
                         }).ToList();

            var requestStatusCacheList = CacheManagerHelper.GetCacheObject<List<RequestStatus>>("GetRequestStatusListSearch",
                delegate
                {
                    RequestStatus requestStatus = new RequestStatus();
                    return requestStatus.GetListSearch("").ToList();
                });

            RequestStatusList = (from l in requestStatusCacheList
                                 select new SelectListItem
                                 {
                                     Text = l.Name,
                                     Value = l.RequestStatusID.ToString()
                                 }).ToList();

            await PersonelDetails.PopulateLists();

        }
    }
}