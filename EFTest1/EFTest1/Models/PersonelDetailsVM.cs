using EFTest1.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EFTest1.Models
{
    public class PersonelDetailsVM
    {
        #region Properties

        public List<SelectListItem> TitleList { get; set; }
        public List<SelectListItem> GenderList { get; set; }
        public List<SelectListItem> EthinicOriginList { get; set; }
        public List<SelectListItem> CareerTrackList { get; set; }
        public List<SelectListItem> CareerLevelList { get; set; }
        public List<SelectListItem> PersonelRoleList { get; set; }
        public List<SelectListItem> TalentSegmentList { get; set; }
        public List<SelectListItem> OrgLevelList { get; set; }
        public List<SelectListItem> CapabilityList { get; set; }
        public List<SelectListItem> OfficeLocationList { get; set; }
        public List<SelectListItem> ContractorConversionList { get; set; }

        public int PersonelID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string SelectedTitleID { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public string SelectedGenderID { get; set; }
        [Required]
        [Display(Name = "Ethnic Origin")]
        public string SelectedEthinicOriginID { get; set; }
        [Required]
        [Display(Name = "Personnel Number")]
        public string PersonelNumber { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        [Required]
        [Display(Name = "Career Track")]
        public string SelectedCareerTrackID { get; set; }
        [Required]
        [Display(Name = "Career Level")]
        public string SelectedCareerLevelID { get; set; }
        [Required]
        [Display(Name = "Personnel Role")]
        public string SelectedPersonelRoleID { get; set; }
        [Required]
        [Display(Name = "Talent Segment")]
        public string SelectedTalentSegmentID { get; set; }
        [Required]
        [Display(Name = "Organization Level")]
        public string SelectedOrgLevelID { get; set; }
        [Required]
        [Display(Name = "Capability")]
        public string SelectedCapabilityID { get; set; }
        [Required]
        [Display(Name = "Office Location")]
        public string SelectedOfficeLocationID { get; set; }
        [Required]
        [Display(Name = "Contractor Conversion")]
        public string SelectedContractorConversionID { get; set; }

        #endregion

        internal async Task PopulateModel(int personelID)
        {
            PersonelID = personelID;

            if (personelID != 0)
            {
                Personel personnel = new Personel();
                personnel.PersonelID = personelID;
                personnel.GetDetails();

                Name = personnel.Name;
                SelectedCapabilityID = personnel.CapabilityID.ToString();
                SelectedCareerLevelID = personnel.CareerLevelID.ToString();
                SelectedCareerTrackID = personnel.CareerTrackID.ToString();
                SelectedContractorConversionID = personnel.ContractorConversionID.ToString();
                SelectedEthinicOriginID = personnel.EthinicOriginID.ToString();
                SelectedGenderID = personnel.GenderID.ToString();
                HireDate = personnel.HireDate;
                SelectedOfficeLocationID = personnel.OfficeLocationID.ToString();
                SelectedOrgLevelID = personnel.OrgLevelID.ToString();
                PersonelNumber = personnel.PersonelNumber;
                SelectedPersonelRoleID = personnel.PersonelRoleID.ToString();
                StartDate = personnel.StartDate;
                Surname = personnel.Surname;
                SelectedTalentSegmentID = personnel.TalentSegmentID.ToString();
                SelectedTitleID = personnel.TitleID.ToString();
            }
            else
            {
                SelectedCapabilityID = "0";
                SelectedCareerLevelID = "0";
                SelectedCareerTrackID = "0";
                SelectedContractorConversionID = "0";
                SelectedEthinicOriginID = "0";
                SelectedGenderID = "0";
                SelectedOfficeLocationID = "0";
                SelectedOrgLevelID = "0";
                SelectedPersonelRoleID = "0";
                SelectedTalentSegmentID = "0";
                SelectedTitleID = "0";
                StartDate = DateTime.Now;
            }
        }

        internal async Task PopulateLists()
        {
            var titlesCacheList = CacheManagerHelper.GetCacheObject<List<Title>>("GetTitleListSearch",
                delegate
                {
                    Title title = new Title();
                    return title.GetListSearch("").ToList();
                });

            TitleList = (from l in titlesCacheList
                         select new SelectListItem
                         {
                             Text = l.Name,
                             Value = l.TitleID.ToString()
                         }).ToList();

            var gendersCacheList = CacheManagerHelper.GetCacheObject<List<Gender>>("GetGenderListSearch",
                delegate
                {
                    Gender gender = new Gender();
                    return gender.GetListSearch("").ToList();
                });

            GenderList = (from l in gendersCacheList
                          select new SelectListItem
                          {
                              Text = l.Name,
                              Value = l.GenderID.ToString()
                          }).ToList();

            var ethnicOriginsCacheList = CacheManagerHelper.GetCacheObject<List<EthinicOrigin>>("GetEthinicOriginListSearch",
                delegate
                {
                    EthinicOrigin ethinicOrigin = new EthinicOrigin();
                    return ethinicOrigin.GetListSearch("").ToList();
                });

            EthinicOriginList = (from l in ethnicOriginsCacheList
                                 select new SelectListItem
                                 {
                                     Text = l.Name,
                                     Value = l.EthinicOriginID.ToString()
                                 }).ToList();

            var careerTracksCacheList = CacheManagerHelper.GetCacheObject<List<CareerTrack>>("GetCareerTrackListSearch",
                delegate
                {
                    CareerTrack careerTrack = new CareerTrack();
                    return careerTrack.GetListSearch("").ToList();
                });

            CareerTrackList = (from l in careerTracksCacheList
                               select new SelectListItem
                               {
                                   Text = l.Name,
                                   Value = l.CareerTrackID.ToString()
                               }).ToList();

            var careerLevelsCacheList = CacheManagerHelper.GetCacheObject<List<CareerLevel>>("GetCareerLevelListSearch",
                delegate
                {
                    CareerLevel careerLevel = new CareerLevel();
                    return careerLevel.GetListSearch("").ToList();
                });

            CareerLevelList = (from l in careerLevelsCacheList
                               select new SelectListItem
                               {
                                   Text = l.Name,
                                   Value = l.CareerLevelID.ToString()
                               }).ToList();

            var personnelRolesCacheList = CacheManagerHelper.GetCacheObject<List<PersonelRole>>("GetPersonelRoleListSearch",
                delegate
                {
                    PersonelRole personelRole = new PersonelRole();
                    return personelRole.GetListSearch("").ToList();
                });

            PersonelRoleList = (from l in personnelRolesCacheList
                                select new SelectListItem
                                {
                                    Text = l.Name,
                                    Value = l.PersonelRoleID.ToString()
                                }).ToList();

            var talentSegmentsCacheList = CacheManagerHelper.GetCacheObject<List<TalentSegment>>("GetTalentSegmentListSearch",
                delegate
                {
                    TalentSegment talentSegment = new TalentSegment();
                    return talentSegment.GetListSearch("").ToList();
                });

            TalentSegmentList = (from l in talentSegmentsCacheList
                                 select new SelectListItem
                                 {
                                     Text = l.Name,
                                     Value = l.TalentSegmentID.ToString()
                                 }).ToList();

            var orgLevelsCacheList = CacheManagerHelper.GetCacheObject<List<OrgLevel>>("GetOrgLevelListSearch",
                delegate
                {
                    OrgLevel orgLevel = new OrgLevel();
                    return orgLevel.GetListSearch("").ToList();
                });

            OrgLevelList = (from l in orgLevelsCacheList
                            select new SelectListItem
                            {
                                Text = l.Name,
                                Value = l.OrgLevelID.ToString()
                            }).ToList();

            var capabilitiesCacheList = CacheManagerHelper.GetCacheObject<List<Capability>>("GetCapabilityListSearch",
                delegate
                {
                    Capability capability = new Capability();
                    return capability.GetListSearch("").ToList();
                });

            CapabilityList = (from l in capabilitiesCacheList
                              select new SelectListItem
                              {
                                  Text = l.Name,
                                  Value = l.CapabilityID.ToString()
                              }).ToList();

            var officeLocationsCacheList = CacheManagerHelper.GetCacheObject<List<OfficeLocation>>("GetOfficeLocationListSearch",
                delegate
                {
                    OfficeLocation officeLocation = new OfficeLocation();
                    return officeLocation.GetListSearch("").ToList();
                });

            OfficeLocationList = (from l in officeLocationsCacheList
                                  select new SelectListItem
                                  {
                                      Text = l.Name,
                                      Value = l.OfficeLocationID.ToString()
                                  }).ToList();

            var conConversionsCacheList = CacheManagerHelper.GetCacheObject<List<ContractorConversion>>("GetContractorConversionListSearch",
                delegate
                {
                    ContractorConversion contractorConversion = new ContractorConversion();
                    return contractorConversion.GetListSearch("").ToList();
                });

            ContractorConversionList = (from l in conConversionsCacheList
                                        select new SelectListItem
                                        {
                                            Text = l.Name,
                                            Value = l.ContractorConversionID.ToString()
                                        }).ToList();
        }
    }
}