using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFTest1.Models
{
    public class RequestSearchVM
    {
        public List<RequestSearchVM_Data> RequestList { get; set; }

        internal void PopulateModel(string searchValue)
        {
            RequestSearch requestSearch = new RequestSearch();

            RequestList = (from l in requestSearch.GetSearchResults(searchValue).ToList()
                                 select new RequestSearchVM_Data
                                 {
                                     RequestID = l.RequestID,
                                     AssignedTo = l.AssignedTo,
                                     Gender = l.PersonelGender,
                                     Name = l.PersonelName,
                                     RequestStatus = l.RequestStatus,
                                     Surname = l.PersonelSurname,
                                     Title = l.PersonelTitle
                                 }).ToList();
        }
    }

    public class RequestSearchVM_Data
    {
        public int RequestID { get; set; }

        [Display(Name="Status")]
        public string RequestStatus { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Display(Name = "Assigned To")]
        public string AssignedTo { get; set; }
    }
}