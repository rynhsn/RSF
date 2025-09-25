using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BaseHeaderReportCOMMON;
using HDR00200Common.DTOs.Print;

namespace HDR00200Common.Model
{
    public class HDR00200ModelReportDummyData
    {
        public static HDR00200ReportResultDTO DefaultData()
        {
            #region Maintenance
            var loParamMaintenance = new HDR00200ReportParam()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = "ASHMD",
                CPROPERTY_NAME = "Harco Mas",
                CREPORT_TYPE = "M",
                CREPORT_TYPE_NAME = "Maintenance",
                CAREA = "",
                CAREA_NAME = "",
                CFROM_BUILDING_ID = "",
                CFROM_BUILDING_NAME = "",
                CTO_BUILDING_ID = "",
                CTO_BUILDING_NAME = "",
                CFROM_DEPT_CODE = "D01",
                CFROM_DEPT_NAME = "Dept A",
                CTO_DEPT_CODE = "D01",
                CTO_DEPT_NAME = "Dept A",
                CFROM_PERIOD = "20250103",
                DFROM_PERIOD = DateTime.Now,
                CTO_PERIOD = "20250104",
                DTO_PERIOD = DateTime.Now + TimeSpan.FromDays(0),
                CCATEGORY = "",
                CSTATUS = "",
                CUSER_ID = "",
                CLANG_ID = "",
                LCOMPLAINT = false,
                LREQUEST = false,
                LINQUIRY = false,
                LHANDOVER = false,
                LSUBMITTED = true,
                LOPEN = true,
                LASSIGNED = true,
                LON_PROGRESS = true,
                LSOLVED = true,
                LCOMPLETED = true,
                LCONFIRMED = true,
                LCLOSED = true,
                LCANCELLED = true,
                LTERMINATED = true,
                LIS_PRINT = true,
                CREPORT_FILENAME = "",
                CREPORT_FILETYPE = ""
            };
            
            var loCollectionMaintenance = new List<HDR00200DataResultDTO>();

            loCollectionMaintenance.Add(
                new HDR00200DataResultDTO
                {
                    LMAINTENANCE = true,
                    LPRIVATE = true,
                    CBUILDING_ID = "B01",
                    CBUILDING_NAME = "Building A",
                    CFLOOR_ID = "F01",
                    CFLOOR_NAME = "Floor A",
                    CUNIT_LOCATION_ID = "U01",
                    CUNIT_LOCATION_NAME = "Unit Location A",
                    CASSET_CODE = "CASSET_CODE",
                    CCARE_TICKET_NO = "Care Ticket No",
                    DCARE_TICKET_DATE = DateTime.Now,
                    CCARE_DESCRIPTION = "Care Description",
                    CSTATUS = "CSTATUS",
                    CSTATUS_DESCRIPTION = "Closed",
                    CSUBSTATUS = "(Surveying)",
                    CSUBSTATUS_DESCRIPTION = "CSUBSTATUS_DESCRIPTION",
                    CTENANT_OWNER_ID = "CTENANT_OWNER_ID",
                    CTENANT_OWNER_NAME = "CTENANT_OWNER_NAME",
                    CCALLER_NAME = "CCALLER_NAME",
                    CCALLER_PHONE = "CCALLER_PHONE",
                    CCALLER_CATEGORY = "CCALLER_CATEGORY",
                    CCALLER_CATEGORY_DESCRIPTION = "CCALLER_CATEGORY_DESCRIPTION",
                    CTENANT_OR_OWNER = "CTENANT_OR_OWNER",
                    CSOLUTION = "CSOLUTION",
                    CINVOICE_REF_NO = "CINVOICE_REF_NO",
                    CCURRENCY_SYMBOL = "CCURRENCY_SYMBOL",
                    NTOTAL_AMOUNT = 2.5m,
                    LESCALATED = false,
                    DCLOSED_DATE = DateTime.Now,
                    CTASK_CTG_CODE = "CTASK_CTG_CODE",
                    CTASK_CTG_NAME = "CTASK_CTG_NAME",
                    CTASK_CODE = "CTASK_CODE",
                    CTASK_NAME = "CTASK_NAME",
                    IDURATION_TIME = 3,
                    CDURATION_TYPE = "CDURATION_TYPE",
                    CDURATION_TYPE_DESCRIPTION = "CDURATION_TYPE_DESCRIPTION",
                    CCHECKLIST_STATUS = "CCHECKLIST_STATUS",
                    CCHECKLIST_NOTES = "CCHECKLIST_NOTES",
                    DPLAN_START_DATE = DateTime.Now,
                    DPLAN_END_DATE = DateTime.Now,
                    DACTUAL_START_DATE = DateTime.Now,
                    DACTUAL_END_DATE = DateTime.Now
                });
            
            loCollectionMaintenance.Add(
                new HDR00200DataResultDTO
                {
                    LMAINTENANCE = true,
                    LPRIVATE = true,
                    CBUILDING_ID = "B01",
                    CBUILDING_NAME = "Building A",
                    CFLOOR_ID = "F01",
                    CFLOOR_NAME = "Floor A",
                    CUNIT_LOCATION_ID = "U01",
                    CUNIT_LOCATION_NAME = "Unit Location A",
                    CASSET_CODE = "CASSET_CODE",
                    CCARE_TICKET_NO = "Care Ticket No",
                    DCARE_TICKET_DATE = DateTime.Now,
                    CCARE_DESCRIPTION = "Care Description",
                    CSTATUS = "CSTATUS",
                    CSTATUS_DESCRIPTION = "Closed",
                    CSUBSTATUS = "(Surveying)",
                    CSUBSTATUS_DESCRIPTION = "CSUBSTATUS_DESCRIPTION",
                    CTENANT_OWNER_ID = "CTENANT_OWNER_ID",
                    CTENANT_OWNER_NAME = "CTENANT_OWNER_NAME",
                    CCALLER_NAME = "CCALLER_NAME",
                    CCALLER_PHONE = "CCALLER_PHONE",
                    CCALLER_CATEGORY = "CCALLER_CATEGORY",
                    CCALLER_CATEGORY_DESCRIPTION = "CCALLER_CATEGORY_DESCRIPTION",
                    CTENANT_OR_OWNER = "CTENANT_OR_OWNER",
                    CSOLUTION = "CSOLUTION",
                    CINVOICE_REF_NO = "CINVOICE_REF_NO",
                    CCURRENCY_SYMBOL = "CCURRENCY_SYMBOL",
                    NTOTAL_AMOUNT = 2.5m,
                    LESCALATED = true,
                    DCLOSED_DATE = DateTime.Now,
                    CTASK_CTG_CODE = "CTASK_CTG_CODE",
                    CTASK_CTG_NAME = "CTASK_CTG_NAME",
                    CTASK_CODE = "CTASK_CODE",
                    CTASK_NAME = "CTASK_NAME",
                    IDURATION_TIME = 3,
                    CDURATION_TYPE = "CDURATION_TYPE",
                    CDURATION_TYPE_DESCRIPTION = "CDURATION_TYPE_DESCRIPTION",
                    CCHECKLIST_STATUS = "CCHECKLIST_STATUS",
                    CCHECKLIST_NOTES = "CCHECKLIST_NOTES",
                    DPLAN_START_DATE = DateTime.Now,
                    DPLAN_END_DATE = DateTime.Now,
                    DACTUAL_START_DATE = DateTime.Now,
                    DACTUAL_END_DATE = DateTime.Now
                });
            
            loCollectionMaintenance.Add(
                new HDR00200DataResultDTO
                {
                    LMAINTENANCE = true,
                    LPRIVATE = true,
                    CBUILDING_ID = "B01",
                    CBUILDING_NAME = "Building A",
                    CFLOOR_ID = "F01",
                    CFLOOR_NAME = "Floor A",
                    CUNIT_LOCATION_ID = "U01",
                    CUNIT_LOCATION_NAME = "Unit Location A",
                    CASSET_CODE = "CASSET_CODE",
                    CCARE_TICKET_NO = "Care Ticket No",
                    DCARE_TICKET_DATE = DateTime.Now,
                    CCARE_DESCRIPTION = "Care Description",
                    CSTATUS = "CSTATUS",
                    CSTATUS_DESCRIPTION = "Closed",
                    CSUBSTATUS = "(Surveying)",
                    CSUBSTATUS_DESCRIPTION = "CSUBSTATUS_DESCRIPTION",
                    CTENANT_OWNER_ID = "CTENANT_OWNER_ID",
                    CTENANT_OWNER_NAME = "CTENANT_OWNER_NAME",
                    CCALLER_NAME = "CCALLER_NAME",
                    CCALLER_PHONE = "CCALLER_PHONE",
                    CCALLER_CATEGORY = "CCALLER_CATEGORY",
                    CCALLER_CATEGORY_DESCRIPTION = "CCALLER_CATEGORY_DESCRIPTION",
                    CTENANT_OR_OWNER = "CTENANT_OR_OWNER",
                    CSOLUTION = "CSOLUTION",
                    CINVOICE_REF_NO = "CINVOICE_REF_NO",
                    CCURRENCY_SYMBOL = "CCURRENCY_SYMBOL",
                    NTOTAL_AMOUNT = 2.5m,
                    LESCALATED = true,
                    DCLOSED_DATE = DateTime.Now,
                    CTASK_CTG_CODE = "CTASK_CTG_CODE",
                    CTASK_CTG_NAME = "CTASK_CTG_NAME",
                    CTASK_CODE = "CTASK_CODE01",
                    CTASK_NAME = "CTASK_NAME01",
                    IDURATION_TIME = 3,
                    CDURATION_TYPE = "CDURATION_TYPE",
                    CDURATION_TYPE_DESCRIPTION = "CDURATION_TYPE_DESCRIPTION",
                    CCHECKLIST_STATUS = "CCHECKLIST_STATUS",
                    CCHECKLIST_NOTES = "CCHECKLIST_NOTES",
                    DPLAN_START_DATE = DateTime.Now,
                    DPLAN_END_DATE = DateTime.Now,
                    DACTUAL_START_DATE = DateTime.Now,
                    DACTUAL_END_DATE = DateTime.Now
                });
            
            loCollectionMaintenance.Add(
                new HDR00200DataResultDTO
                {
                    LMAINTENANCE = true,
                    LPRIVATE = true,
                    CBUILDING_ID = "B01",
                    CBUILDING_NAME = "Building A",
                    CFLOOR_ID = "F01",
                    CFLOOR_NAME = "Floor A",
                    CUNIT_LOCATION_ID = "U01",
                    CUNIT_LOCATION_NAME = "Unit Location A",
                    CASSET_CODE = "CASSET_CODE",
                    CCARE_TICKET_NO = "Care Ticket No",
                    DCARE_TICKET_DATE = DateTime.Now,
                    CCARE_DESCRIPTION = "Care Description",
                    CSTATUS = "CSTATUS",
                    CSTATUS_DESCRIPTION = "Closed",
                    CSUBSTATUS = "(Surveying)",
                    CSUBSTATUS_DESCRIPTION = "CSUBSTATUS_DESCRIPTION",
                    CTENANT_OWNER_ID = "CTENANT_OWNER_ID",
                    CTENANT_OWNER_NAME = "CTENANT_OWNER_NAME",
                    CCALLER_NAME = "CCALLER_NAME",
                    CCALLER_PHONE = "CCALLER_PHONE",
                    CCALLER_CATEGORY = "CCALLER_CATEGORY",
                    CCALLER_CATEGORY_DESCRIPTION = "CCALLER_CATEGORY_DESCRIPTION",
                    CTENANT_OR_OWNER = "CTENANT_OR_OWNER",
                    CSOLUTION = "CSOLUTION",
                    CINVOICE_REF_NO = "CINVOICE_REF_NO",
                    CCURRENCY_SYMBOL = "CCURRENCY_SYMBOL",
                    NTOTAL_AMOUNT = 2.5m,
                    LESCALATED = true,
                    DCLOSED_DATE = DateTime.Now,
                    CTASK_CTG_CODE = "CTASK_CTG_CODE01",
                    CTASK_CTG_NAME = "CTASK_CTG_NAME01",
                    CTASK_CODE = "CTASK_CODE",
                    CTASK_NAME = "CTASK_NAME",
                    IDURATION_TIME = 3,
                    CDURATION_TYPE = "CDURATION_TYPE",
                    CDURATION_TYPE_DESCRIPTION = "CDURATION_TYPE_DESCRIPTION",
                    CCHECKLIST_STATUS = "CCHECKLIST_STATUS",
                    CCHECKLIST_NOTES = "CCHECKLIST_NOTES",
                    DPLAN_START_DATE = DateTime.Now,
                    DPLAN_END_DATE = DateTime.Now,
                    DACTUAL_START_DATE = DateTime.Now,
                    DACTUAL_END_DATE = DateTime.Now
                });

            loCollectionMaintenance.Add(
                new HDR00200DataResultDTO
                {
                    LMAINTENANCE = true,
                    LPRIVATE = true,
                    CBUILDING_ID = "B01",
                    CBUILDING_NAME = "Building A",
                    CFLOOR_ID = "F01",
                    CFLOOR_NAME = "Floor A",
                    CUNIT_LOCATION_ID = "U01",
                    CUNIT_LOCATION_NAME = "Unit Location A",
                    CASSET_CODE = "CASSET_CODE01",
                    CCARE_TICKET_NO = "Care Ticket No",
                    DCARE_TICKET_DATE = DateTime.Now,
                    CCARE_DESCRIPTION = "Care Description",
                    CSTATUS = "CSTATUS",
                    CSTATUS_DESCRIPTION = "Closed",
                    CSUBSTATUS = "(Surveying)",
                    CSUBSTATUS_DESCRIPTION = "CSUBSTATUS_DESCRIPTION",
                    CTENANT_OWNER_ID = "CTENANT_OWNER_ID",
                    CTENANT_OWNER_NAME = "CTENANT_OWNER_NAME",
                    CCALLER_NAME = "CCALLER_NAME",
                    CCALLER_PHONE = "CCALLER_PHONE",
                    CCALLER_CATEGORY = "CCALLER_CATEGORY",
                    CCALLER_CATEGORY_DESCRIPTION = "CCALLER_CATEGORY_DESCRIPTION",
                    CTENANT_OR_OWNER = "CTENANT_OR_OWNER",
                    CSOLUTION = "CSOLUTION",
                    CINVOICE_REF_NO = "CINVOICE_REF_NO",
                    CCURRENCY_SYMBOL = "CCURRENCY_SYMBOL",
                    NTOTAL_AMOUNT = 2.5m,
                    LESCALATED = true,
                    DCLOSED_DATE = DateTime.Now,
                    CTASK_CTG_CODE = "CTASK_CTG_CODE01",
                    CTASK_CTG_NAME = "CTASK_CTG_NAME01",
                    CTASK_CODE = "CTASK_CODE01",
                    CTASK_NAME = "CTASK_NAME01",
                    IDURATION_TIME = 3,
                    CDURATION_TYPE = "CDURATION_TYPE",
                    CDURATION_TYPE_DESCRIPTION = "CDURATION_TYPE_DESCRIPTION",
                    CCHECKLIST_STATUS = "CCHECKLIST_STATUS",
                    CCHECKLIST_NOTES = "CCHECKLIST_NOTES",
                    DPLAN_START_DATE = DateTime.Now,
                    DPLAN_END_DATE = DateTime.Now,
                    DACTUAL_START_DATE = DateTime.Now,
                    DACTUAL_END_DATE = DateTime.Now
                });

            loCollectionMaintenance.Add(
                new HDR00200DataResultDTO
                {
                    LMAINTENANCE = true,
                    LPRIVATE = true,
                    CBUILDING_ID = "B01",
                    CBUILDING_NAME = "Building A",
                    CFLOOR_ID = "F01",
                    CFLOOR_NAME = "Floor A",
                    CUNIT_LOCATION_ID = "U01",
                    CUNIT_LOCATION_NAME = "Unit Location A",
                    CASSET_CODE = "CASSET_CODE",
                    CCARE_TICKET_NO = "Care Ticket No",
                    DCARE_TICKET_DATE = DateTime.Now,
                    CCARE_DESCRIPTION = "Care Description",
                    CSTATUS = "CSTATUS",
                    CSTATUS_DESCRIPTION = "Closed",
                    CSUBSTATUS = "(Surveying)",
                    CSUBSTATUS_DESCRIPTION = "CSUBSTATUS_DESCRIPTION",
                    CTENANT_OWNER_ID = "CTENANT_OWNER_ID",
                    CTENANT_OWNER_NAME = "CTENANT_OWNER_NAME",
                    CCALLER_NAME = "CCALLER_NAME",
                    CCALLER_PHONE = "CCALLER_PHONE",
                    CCALLER_CATEGORY = "CCALLER_CATEGORY",
                    CCALLER_CATEGORY_DESCRIPTION = "CCALLER_CATEGORY_DESCRIPTION",
                    CTENANT_OR_OWNER = "CTENANT_OR_OWNER",
                    CSOLUTION = "CSOLUTION",
                    CINVOICE_REF_NO = "CINVOICE_REF_NO",
                    CCURRENCY_SYMBOL = "CCURRENCY_SYMBOL",
                    NTOTAL_AMOUNT = 2.5m,
                    LESCALATED = true,
                    DCLOSED_DATE = DateTime.Now,
                    CTASK_CTG_CODE = "CTASK_CTG_CODE01",
                    CTASK_CTG_NAME = "CTASK_CTG_NAME01",
                    CTASK_CODE = "CTASK_CODE01",
                    CTASK_NAME = "CTASK_NAME01",
                    IDURATION_TIME = 3,
                    CDURATION_TYPE = "CDURATION_TYPE",
                    CDURATION_TYPE_DESCRIPTION = "CDURATION_TYPE_DESCRIPTION",
                    CCHECKLIST_STATUS = "CCHECKLIST_STATUS",
                    CCHECKLIST_NOTES = "CCHECKLIST_NOTES",
                    DPLAN_START_DATE = DateTime.Now,
                    DPLAN_END_DATE = DateTime.Now,
                    DACTUAL_START_DATE = DateTime.Now,
                    DACTUAL_END_DATE = DateTime.Now
                });
            
            loCollectionMaintenance.Add(
                new HDR00200DataResultDTO
                {
                    LMAINTENANCE = true,
                    LPRIVATE = true,
                    CBUILDING_ID = "B01",
                    CBUILDING_NAME = "Building A",
                    CFLOOR_ID = "F01",
                    CFLOOR_NAME = "Floor A",
                    CUNIT_LOCATION_ID = "U01",
                    CUNIT_LOCATION_NAME = "Unit Location A",
                    CASSET_CODE = "CASSET_CODE",
                    CCARE_TICKET_NO = "Care Ticket No",
                    DCARE_TICKET_DATE = DateTime.Now,
                    CCARE_DESCRIPTION = "Care Description",
                    CSTATUS = "CSTATUS",
                    CSTATUS_DESCRIPTION = "Closed",
                    CSUBSTATUS = "(Surveying)",
                    CSUBSTATUS_DESCRIPTION = "CSUBSTATUS_DESCRIPTION",
                    CTENANT_OWNER_ID = "CTENANT_OWNER_ID",
                    CTENANT_OWNER_NAME = "CTENANT_OWNER_NAME",
                    CCALLER_NAME = "CCALLER_NAME",
                    CCALLER_PHONE = "CCALLER_PHONE",
                    CCALLER_CATEGORY = "CCALLER_CATEGORY",
                    CCALLER_CATEGORY_DESCRIPTION = "CCALLER_CATEGORY_DESCRIPTION",
                    CTENANT_OR_OWNER = "CTENANT_OR_OWNER",
                    CSOLUTION = "CSOLUTION",
                    CINVOICE_REF_NO = "CINVOICE_REF_NO",
                    CCURRENCY_SYMBOL = "CCURRENCY_SYMBOL",
                    NTOTAL_AMOUNT = 2.5m,
                    LESCALATED = true,
                    DCLOSED_DATE = DateTime.Now,
                    CTASK_CTG_CODE = "CTASK_CTG_CODE01",
                    CTASK_CTG_NAME = "CTASK_CTG_NAME01",
                    CTASK_CODE = "CTASK_CODE01",
                    CTASK_NAME = "CTASK_NAME01",
                    IDURATION_TIME = 3,
                    CDURATION_TYPE = "CDURATION_TYPE",
                    CDURATION_TYPE_DESCRIPTION = "CDURATION_TYPE_DESCRIPTION",
                    CCHECKLIST_STATUS = "CCHECKLIST_STATUS",
                    CCHECKLIST_NOTES = "CCHECKLIST_NOTES",
                    DPLAN_START_DATE = DateTime.Now,
                    DPLAN_END_DATE = DateTime.Now,
                    DACTUAL_START_DATE = DateTime.Now,
                    DACTUAL_END_DATE = DateTime.Now
                });

            // ambil hanya yang datanya LMAINTENANCE = true
            
            var loTempDataMaintenance = loCollectionMaintenance.GroupBy(data1a => new
            {
                data1a.CTASK_CTG_CODE,
                data1a.CTASK_CTG_NAME
            }).Select(data1b => new HDR00200GroupCareMaintenanceDTO()
            {
                CTASK_CTG_CODE = data1b.Key.CTASK_CTG_CODE,
                CTASK_CTG_NAME = data1b.Key.CTASK_CTG_NAME,
                SUM_ESCALATED = data1b.Count(x => x.LESCALATED),
                SUM_ALL = data1b.Count(),
                
                Task = data1b.GroupBy(data2a => new
                {
                    data2a.CTASK_CODE,
                    data2a.CTASK_NAME,
                    data2a.IDURATION_TIME,
                    data2a.CDURATION_TYPE_DESCRIPTION
                }).Select(data2b => new HDR00200GroupTaskMaintenanceDTO()
                {
                    CTASK_CODE = data2b.Key.CTASK_CODE,
                    CTASK_NAME = data2b.Key.CTASK_NAME,
                    IDURATION_TIME = data2b.Key.IDURATION_TIME,
                    CDURATION_TYPE_DESCRIPTION = data2b.Key.CDURATION_TYPE_DESCRIPTION,
                    SUM_ESCALATED = data2b.Count(x => x.LESCALATED),
                    SUM_ALL = data2b.Count(),

                    Asset = data2b.GroupBy(data3a => new
                    {
                        data3a.CASSET_CODE,
                        
                    }).Select(data3b => new HDR00200GroupAssetMaintenanceDTO()
                    {
                        CASSET_CODE = data3b.Key.CASSET_CODE,
                        SUM_ESCALATED = data3b.Count(x => x.LESCALATED),
                        SUM_ALL = data3b.Count(),
                        Data = data3b.ToList()
                    }).ToList()
                }).ToList()
            }).ToList();

            #endregion
            
            #region Private
            var loParamPrivate = new HDR00200ReportParam()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = "ASHMD",
                CPROPERTY_NAME = "Harco Mas",
                CREPORT_TYPE = "C",
                CREPORT_TYPE_NAME = "CARE",
                CAREA = "01",
                CAREA_NAME = "Private",
                CFROM_BUILDING_ID = "TW-A",
                CFROM_BUILDING_NAME = "Tower A",
                CTO_BUILDING_ID = "TW-B",
                CTO_BUILDING_NAME = "Tower B",
                CFROM_DEPT_CODE = "D01",
                CFROM_DEPT_NAME = "Dept A",
                CTO_DEPT_CODE = "D01",
                CTO_DEPT_NAME = "Dept A",
                CFROM_PERIOD = "20250103",
                DFROM_PERIOD = DateTime.Now,
                CTO_PERIOD = "20250104",
                DTO_PERIOD = DateTime.Now + TimeSpan.FromDays(0),
                CCATEGORY = "02,03,04",
                CSTATUS = "01,05,07",
                CUSER_ID = "",
                CLANG_ID = "",
                LCOMPLAINT = true,
                LREQUEST = true,
                LINQUIRY = true,
                LHANDOVER = false,
                LSUBMITTED = true,
                LOPEN = false,
                LASSIGNED = false,
                LON_PROGRESS = false,
                LSOLVED = true,
                LCOMPLETED = false,
                LCONFIRMED = true,
                LCLOSED = false,
                LCANCELLED = false,
                LTERMINATED = false,
                LIS_PRINT = false,
                CREPORT_FILENAME = "",
                CREPORT_FILETYPE = ""
            };


            var loCollectionPrivate = new List<HDR00200DataResultDTO>();
            
            loCollectionPrivate.Add(
                new HDR00200DataResultDTO
                {
                    LMAINTENANCE = false,
                    LPRIVATE = true,
                    CBUILDING_ID = "B01",
                    CBUILDING_NAME = "Building A",
                    CFLOOR_ID = "F01",
                    CFLOOR_NAME = "Floor A",
                    CUNIT_LOCATION_ID = "U01",
                    CUNIT_LOCATION_NAME = "Unit Location A",
                    CASSET_CODE = "CASSET_CODE",
                    CCARE_TICKET_NO = "Care Ticket No",
                    DCARE_TICKET_DATE = DateTime.Now,
                    CCARE_DESCRIPTION = "Care Description",
                    CSTATUS = "CSTATUS",
                    CSTATUS_DESCRIPTION = "Closed",
                    CSUBSTATUS = "(Surveying)",
                    CSUBSTATUS_DESCRIPTION = "CSUBSTATUS_DESCRIPTION",
                    CTENANT_OWNER_ID = "CTENANT_OWNER_ID",
                    CTENANT_OWNER_NAME = "CTENANT_OWNER_NAME",
                    CCALLER_NAME = "CCALLER_NAME",
                    CCALLER_PHONE = "CCALLER_PHONE",
                    CCALLER_CATEGORY = "CCALLER_CATEGORY",
                    CCALLER_CATEGORY_DESCRIPTION = "CCALLER_CATEGORY_DESCRIPTION",
                    CTENANT_OR_OWNER = "CTENANT_OR_OWNER",
                    CSOLUTION = "CSOLUTION",
                    CINVOICE_REF_NO = "CINVOICE_REF_NO",
                    CCURRENCY_SYMBOL = "CCURRENCY_SYMBOL",
                    NTOTAL_AMOUNT = 2.5m,
                    LESCALATED = false,
                    DCLOSED_DATE = DateTime.Now,
                    CTASK_CTG_CODE = "CTASK_CTG_CODE",
                    CTASK_CTG_NAME = "CTASK_CTG_NAME",
                    CTASK_CODE = "CTASK_CODE",
                    CTASK_NAME = "CTASK_NAME",
                    IDURATION_TIME = 3,
                    CDURATION_TYPE = "CDURATION_TYPE",
                    CDURATION_TYPE_DESCRIPTION = "CDURATION_TYPE_DESCRIPTION",
                    CCHECKLIST_STATUS = "CCHECKLIST_STATUS",
                    CCHECKLIST_NOTES = "CCHECKLIST_NOTES",
                    DPLAN_START_DATE = DateTime.Now,
                    DPLAN_END_DATE = DateTime.Now,
                    DACTUAL_START_DATE = DateTime.Now,
                    DACTUAL_END_DATE = DateTime.Now
                });
            
            loCollectionPrivate.Add(
                new HDR00200DataResultDTO
                {
                    LMAINTENANCE = false,
                    LPRIVATE = true,
                    CBUILDING_ID = "B01",
                    CBUILDING_NAME = "Building A",
                    CFLOOR_ID = "F01",
                    CFLOOR_NAME = "Floor A",
                    CUNIT_LOCATION_ID = "U01",
                    CUNIT_LOCATION_NAME = "Unit Location A",
                    CASSET_CODE = "CASSET_CODE",
                    CCARE_TICKET_NO = "Care Ticket No",
                    DCARE_TICKET_DATE = DateTime.Now,
                    CCARE_DESCRIPTION = "Care Description",
                    CSTATUS = "CSTATUS",
                    CSTATUS_DESCRIPTION = "Closed",
                    CSUBSTATUS = "(Surveying)",
                    CSUBSTATUS_DESCRIPTION = "CSUBSTATUS_DESCRIPTION",
                    CTENANT_OWNER_ID = "CTENANT_OWNER_ID",
                    CTENANT_OWNER_NAME = "CTENANT_OWNER_NAME",
                    CCALLER_NAME = "CCALLER_NAME",
                    CCALLER_PHONE = "CCALLER_PHONE",
                    CCALLER_CATEGORY = "CCALLER_CATEGORY",
                    CCALLER_CATEGORY_DESCRIPTION = "CCALLER_CATEGORY_DESCRIPTION",
                    CTENANT_OR_OWNER = "CTENANT_OR_OWNER",
                    CSOLUTION = "CSOLUTION",
                    CINVOICE_REF_NO = "CINVOICE_REF_NO",
                    CCURRENCY_SYMBOL = "CCURRENCY_SYMBOL",
                    NTOTAL_AMOUNT = 2.5m,
                    LESCALATED = false,
                    DCLOSED_DATE = DateTime.Now,
                    CTASK_CTG_CODE = "CTASK_CTG_CODE",
                    CTASK_CTG_NAME = "CTASK_CTG_NAME",
                    CTASK_CODE = "CTASK_CODE",
                    CTASK_NAME = "CTASK_NAME",
                    IDURATION_TIME = 3,
                    CDURATION_TYPE = "CDURATION_TYPE",
                    CDURATION_TYPE_DESCRIPTION = "CDURATION_TYPE_DESCRIPTION",
                    CCHECKLIST_STATUS = "CCHECKLIST_STATUS",
                    CCHECKLIST_NOTES = "CCHECKLIST_NOTES",
                    DPLAN_START_DATE = DateTime.Now,
                    DPLAN_END_DATE = DateTime.Now,
                    DACTUAL_START_DATE = DateTime.Now,
                    DACTUAL_END_DATE = DateTime.Now
                });
            
            loCollectionPrivate.Add(
                new HDR00200DataResultDTO
                {
                    LMAINTENANCE = false,
                    LPRIVATE = true,
                    CBUILDING_ID = "B01",
                    CBUILDING_NAME = "Building A",
                    CFLOOR_ID = "F01",
                    CFLOOR_NAME = "Floor A",
                    CUNIT_LOCATION_ID = "U01",
                    CUNIT_LOCATION_NAME = "Unit Location A",
                    CASSET_CODE = "CASSET_CODE01",
                    CCARE_TICKET_NO = "Care Ticket No",
                    DCARE_TICKET_DATE = DateTime.Now,
                    CCARE_DESCRIPTION = "Care Description",
                    CSTATUS = "CSTATUS",
                    CSTATUS_DESCRIPTION = "Closed",
                    CSUBSTATUS = "(Surveying)",
                    CSUBSTATUS_DESCRIPTION = "CSUBSTATUS_DESCRIPTION",
                    CTENANT_OWNER_ID = "CTENANT_OWNER_ID",
                    CTENANT_OWNER_NAME = "CTENANT_OWNER_NAME",
                    CCALLER_NAME = "CCALLER_NAME",
                    CCALLER_PHONE = "CCALLER_PHONE",
                    CCALLER_CATEGORY = "CCALLER_CATEGORY",
                    CCALLER_CATEGORY_DESCRIPTION = "CCALLER_CATEGORY_DESCRIPTION",
                    CTENANT_OR_OWNER = "CTENANT_OR_OWNER",
                    CSOLUTION = "CSOLUTION",
                    CINVOICE_REF_NO = "CINVOICE_REF_NO",
                    CCURRENCY_SYMBOL = "CCURRENCY_SYMBOL",
                    NTOTAL_AMOUNT = 2.5m,
                    LESCALATED = true,
                    DCLOSED_DATE = DateTime.Now,
                    CTASK_CTG_CODE = "CTASK_CTG_CODE",
                    CTASK_CTG_NAME = "CTASK_CTG_NAME",
                    CTASK_CODE = "CTASK_CODE",
                    CTASK_NAME = "CTASK_NAME",
                    IDURATION_TIME = 3,
                    CDURATION_TYPE = "CDURATION_TYPE",
                    CDURATION_TYPE_DESCRIPTION = "CDURATION_TYPE_DESCRIPTION",
                    CCHECKLIST_STATUS = "CCHECKLIST_STATUS",
                    CCHECKLIST_NOTES = "CCHECKLIST_NOTES",
                    DPLAN_START_DATE = DateTime.Now,
                    DPLAN_END_DATE = DateTime.Now,
                    DACTUAL_START_DATE = DateTime.Now,
                    DACTUAL_END_DATE = DateTime.Now
                });
            
            var loTempDataPrivate = loCollectionPrivate.Where(x => x.LMAINTENANCE == false && x.LPRIVATE).GroupBy(data1a => new
            {
                data1a.CBUILDING_ID,
                data1a.CBUILDING_NAME,
            }).Select(data1b => new HDR00200GroupCarePrivateDTO()
            {
                CBUILDING_ID = data1b.Key.CBUILDING_ID,
                CBUILDING_NAME = data1b.Key.CBUILDING_NAME,
                SUM_WITH_INVOICE = data1b.Count(x => x.CINVOICE_REF_NO != "-"),
                SUM_ESCALATED = data1b.Count(x => x.LESCALATED),
                SUM_ALL = data1b.Count(),
                
                Floor = data1b.GroupBy(data2a => new
                {
                    data2a.CFLOOR_ID,
                    data2a.CFLOOR_NAME
                }).Select(data2b => new HDR00200GroupFloorPrivateDTO()
                {
                    CFLOOR_ID = data2b.Key.CFLOOR_ID,
                    CFLOOR_NAME = data2b.Key.CFLOOR_NAME,
                    SUM_WITH_INVOICE = data2b.Count(x => x.CINVOICE_REF_NO != "-"),
                    SUM_ESCALATED = data2b.Count(x => x.LESCALATED),
                    SUM_ALL = data2b.Count(),
                    
                    Unit = data2b.GroupBy(data3a => new
                    {
                        data3a.CUNIT_LOCATION_ID,
                        data3a.CUNIT_LOCATION_NAME
                    }).Select(data3b => new HDR00200GroupUnitPrivateDTO()
                    {
                        CUNIT_LOCATION_ID = data3b.Key.CUNIT_LOCATION_ID,
                        SUM_WITH_INVOICE = data3b.Count(x => x.CINVOICE_REF_NO != "-"),
                        SUM_ESCALATED = data3b.Count(x => x.LESCALATED),
                        SUM_ALL = data3b.Count(),
                        Data = data3b.ToList()
                    }).ToList()
                }).ToList()
            }).ToList();
            
            #endregion


            #region Public

            var loParamPublic = new HDR00200ReportParam()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = "ASHMD",
                CPROPERTY_NAME = "Harco Mas",
                CREPORT_TYPE = "C",
                CREPORT_TYPE_NAME = "CARE",
                CAREA = "02",
                CAREA_NAME = "Private",
                CFROM_BUILDING_ID = "",
                CFROM_BUILDING_NAME = "",
                CTO_BUILDING_ID = "",
                CTO_BUILDING_NAME = "",
                CFROM_DEPT_CODE = "D01",
                CFROM_DEPT_NAME = "Dept A",
                CTO_DEPT_CODE = "D01",
                CTO_DEPT_NAME = "Dept A",
                CFROM_PERIOD = "20250103",
                DFROM_PERIOD = DateTime.Now,
                CTO_PERIOD = "20250104",
                DTO_PERIOD = DateTime.Now + TimeSpan.FromDays(0),
                CCATEGORY = "02,03,04",
                CSTATUS = "01,05,07",
                CUSER_ID = "",
                CLANG_ID = "",
                LCOMPLAINT = true,
                LREQUEST = true,
                LINQUIRY = true,
                LHANDOVER = false,
                LSUBMITTED = true,
                LOPEN = false,
                LASSIGNED = false,
                LON_PROGRESS = false,
                LSOLVED = true,
                LCOMPLETED = false,
                LCONFIRMED = true,
                LCLOSED = false,
                LCANCELLED = false,
                LTERMINATED = false,
                LIS_PRINT = false,
                CREPORT_FILENAME = "",
                CREPORT_FILETYPE = ""
            };


            var loCollectionPublic = new List<HDR00200DataResultDTO>();
            
            loCollectionPublic.Add(
                new HDR00200DataResultDTO
                {
                    LMAINTENANCE = false,
                    LPRIVATE = false,
                    CBUILDING_ID = "B01",
                    CBUILDING_NAME = "Building A",
                    CFLOOR_ID = "F01",
                    CFLOOR_NAME = "Floor A",
                    CUNIT_LOCATION_ID = "U01",
                    CUNIT_LOCATION_NAME = "Unit Location A",
                    CASSET_CODE = "CASSET_CODE",
                    CCARE_TICKET_NO = "Care Ticket No",
                    DCARE_TICKET_DATE = DateTime.Now,
                    CCARE_DESCRIPTION = "Care Description",
                    CSTATUS = "CSTATUS",
                    CSTATUS_DESCRIPTION = "Closed",
                    CSUBSTATUS = "(Surveying)",
                    CSUBSTATUS_DESCRIPTION = "CSUBSTATUS_DESCRIPTION",
                    CTENANT_OWNER_ID = "CTENANT_OWNER_ID",
                    CTENANT_OWNER_NAME = "CTENANT_OWNER_NAME",
                    CCALLER_NAME = "CCALLER_NAME",
                    CCALLER_PHONE = "CCALLER_PHONE",
                    CCALLER_CATEGORY = "CCALLER_CATEGORY",
                    CCALLER_CATEGORY_DESCRIPTION = "CCALLER_CATEGORY_DESCRIPTION",
                    CTENANT_OR_OWNER = "CTENANT_OR_OWNER",
                    CSOLUTION = "CSOLUTION",
                    CINVOICE_REF_NO = "CINVOICE_REF_NO",
                    CCURRENCY_SYMBOL = "CCURRENCY_SYMBOL",
                    NTOTAL_AMOUNT = 2.5m,
                    LESCALATED = false,
                    DCLOSED_DATE = DateTime.Now,
                    CTASK_CTG_CODE = "CTASK_CTG_CODE",
                    CTASK_CTG_NAME = "CTASK_CTG_NAME",
                    CTASK_CODE = "CTASK_CODE",
                    CTASK_NAME = "CTASK_NAME",
                    IDURATION_TIME = 3,
                    CDURATION_TYPE = "CDURATION_TYPE",
                    CDURATION_TYPE_DESCRIPTION = "CDURATION_TYPE_DESCRIPTION",
                    CCHECKLIST_STATUS = "CCHECKLIST_STATUS",
                    CCHECKLIST_NOTES = "CCHECKLIST_NOTES",
                    DPLAN_START_DATE = DateTime.Now,
                    DPLAN_END_DATE = DateTime.Now,
                    DACTUAL_START_DATE = DateTime.Now,
                    DACTUAL_END_DATE = DateTime.Now
                });
            
            loCollectionPublic.Add(
                new HDR00200DataResultDTO
                {
                    LMAINTENANCE = false,
                    LPRIVATE = false,
                    CBUILDING_ID = "B01",
                    CBUILDING_NAME = "Building A",
                    CFLOOR_ID = "F01",
                    CFLOOR_NAME = "Floor A",
                    CUNIT_LOCATION_ID = "U01",
                    CUNIT_LOCATION_NAME = "Unit Location A",
                    CASSET_CODE = "CASSET_CODE",
                    CCARE_TICKET_NO = "Care Ticket No",
                    DCARE_TICKET_DATE = DateTime.Now,
                    CCARE_DESCRIPTION = "Care Description",
                    CSTATUS = "CSTATUS",
                    CSTATUS_DESCRIPTION = "Closed",
                    CSUBSTATUS = "(Surveying)",
                    CSUBSTATUS_DESCRIPTION = "CSUBSTATUS_DESCRIPTION",
                    CTENANT_OWNER_ID = "CTENANT_OWNER_ID",
                    CTENANT_OWNER_NAME = "CTENANT_OWNER_NAME",
                    CCALLER_NAME = "CCALLER_NAME",
                    CCALLER_PHONE = "CCALLER_PHONE",
                    CCALLER_CATEGORY = "CCALLER_CATEGORY",
                    CCALLER_CATEGORY_DESCRIPTION = "CCALLER_CATEGORY_DESCRIPTION",
                    CTENANT_OR_OWNER = "CTENANT_OR_OWNER",
                    CSOLUTION = "CSOLUTION",
                    CINVOICE_REF_NO = "CINVOICE_REF_NO",
                    CCURRENCY_SYMBOL = "CCURRENCY_SYMBOL",
                    NTOTAL_AMOUNT = 2.5m,
                    LESCALATED = false,
                    DCLOSED_DATE = DateTime.Now,
                    CTASK_CTG_CODE = "CTASK_CTG_CODE",
                    CTASK_CTG_NAME = "CTASK_CTG_NAME",
                    CTASK_CODE = "CTASK_CODE",
                    CTASK_NAME = "CTASK_NAME",
                    IDURATION_TIME = 3,
                    CDURATION_TYPE = "CDURATION_TYPE",
                    CDURATION_TYPE_DESCRIPTION = "CDURATION_TYPE_DESCRIPTION",
                    CCHECKLIST_STATUS = "CCHECKLIST_STATUS",
                    CCHECKLIST_NOTES = "CCHECKLIST_NOTES",
                    DPLAN_START_DATE = DateTime.Now,
                    DPLAN_END_DATE = DateTime.Now,
                    DACTUAL_START_DATE = DateTime.Now,
                    DACTUAL_END_DATE = DateTime.Now
                });
            
            loCollectionPublic.Add(
                new HDR00200DataResultDTO
                {
                    LMAINTENANCE = false,
                    LPRIVATE = false,
                    CBUILDING_ID = "B01",
                    CBUILDING_NAME = "Building A",
                    CFLOOR_ID = "F01",
                    CFLOOR_NAME = "Floor A",
                    CUNIT_LOCATION_ID = "U01",
                    CUNIT_LOCATION_NAME = "Unit Location A",
                    CASSET_CODE = "CASSET_CODE01",
                    CCARE_TICKET_NO = "Care Ticket No",
                    DCARE_TICKET_DATE = DateTime.Now,
                    CCARE_DESCRIPTION = "Care Description",
                    CSTATUS = "CSTATUS",
                    CSTATUS_DESCRIPTION = "Closed",
                    CSUBSTATUS = "(Surveying)",
                    CSUBSTATUS_DESCRIPTION = "CSUBSTATUS_DESCRIPTION",
                    CTENANT_OWNER_ID = "CTENANT_OWNER_ID",
                    CTENANT_OWNER_NAME = "CTENANT_OWNER_NAME",
                    CCALLER_NAME = "CCALLER_NAME",
                    CCALLER_PHONE = "CCALLER_PHONE",
                    CCALLER_CATEGORY = "CCALLER_CATEGORY",
                    CCALLER_CATEGORY_DESCRIPTION = "CCALLER_CATEGORY_DESCRIPTION",
                    CTENANT_OR_OWNER = "CTENANT_OR_OWNER",
                    CSOLUTION = "CSOLUTION",
                    CINVOICE_REF_NO = "CINVOICE_REF_NO",
                    CCURRENCY_SYMBOL = "CCURRENCY_SYMBOL",
                    NTOTAL_AMOUNT = 2.5m,
                    LESCALATED = true,
                    DCLOSED_DATE = DateTime.Now,
                    CTASK_CTG_CODE = "CTASK_CTG_CODE",
                    CTASK_CTG_NAME = "CTASK_CTG_NAME",
                    CTASK_CODE = "CTASK_CODE",
                    CTASK_NAME = "CTASK_NAME",
                    IDURATION_TIME = 3,
                    CDURATION_TYPE = "CDURATION_TYPE",
                    CDURATION_TYPE_DESCRIPTION = "CDURATION_TYPE_DESCRIPTION",
                    CCHECKLIST_STATUS = "CCHECKLIST_STATUS",
                    CCHECKLIST_NOTES = "CCHECKLIST_NOTES",
                    DPLAN_START_DATE = DateTime.Now,
                    DPLAN_END_DATE = DateTime.Now,
                    DACTUAL_START_DATE = DateTime.Now,
                    DACTUAL_END_DATE = DateTime.Now
                });

            var loTempDataPublic = loCollectionPublic.Where(x => x.LMAINTENANCE == false && x.LPRIVATE == false).GroupBy(data1a => new
            {
                data1a.CUNIT_LOCATION_ID,
                data1a.CUNIT_LOCATION_NAME,
                data1a.CBUILDING_ID,
                data1a.CFLOOR_ID,
            }).Select(data1b => new HDR00200GroupCarePublicDTO()
            {
                CUNIT_LOCATION_ID = data1b.Key.CUNIT_LOCATION_ID,
                CUNIT_LOCATION_NAME = data1b.Key.CUNIT_LOCATION_NAME,
                //jika bulding dan floor tidak kosong maka tampilkan (building | floor), jika building ada dan floor kosong maka tampilkan (building), lalu jika keuanya kosong maka string kosong
                CBUILDING_FLOOR = (data1b.Key.CBUILDING_ID != "" && data1b.Key.CFLOOR_ID != "") ? $"{data1b.Key.CBUILDING_ID} | {data1b.Key.CFLOOR_ID}" : (data1b.Key.CBUILDING_ID != "" && data1b.Key.CFLOOR_ID == "") ? data1b.Key.CBUILDING_ID : "",
                CBUILDING_ID = data1b.Key.CBUILDING_ID,
                CFLOOR_ID = data1b.Key.CFLOOR_ID,
                SUM_ESCALATED = data1b.Count(x => x.LESCALATED),
                SUM_ALL = data1b.Count(),
                Data = data1b.ToList()
            }).ToList();

            #endregion
            
            var loData = new HDR00200ReportResultDTO
            {
                Title = "CARE Report",
                Label = new HDR00200ReportLabelDTO(),
                Header = new HDR00200ReportHeaderDTO
                {
                    PropertyDisplay = loParamPublic.CPROPERTY_NAME,
                    PeriodFrom = loParamPublic.DFROM_PERIOD,
                    PeriodTo = loParamPublic.DTO_PERIOD,
                    AreaDisplay = loParamPublic.CAREA_NAME,
                    CategoryDisplay = loParamPublic.CREPORT_TYPE_NAME,
                    BuildingDisplay = loParamPublic.CFROM_BUILDING_NAME == loParamPublic.CTO_BUILDING_NAME
                        ? loParamPublic.CFROM_BUILDING_NAME
                        : $"{loParamPublic.CFROM_BUILDING_NAME} to {loParamPublic.CTO_BUILDING_NAME}",
                    DepartmentDisplay = loParamPublic.CFROM_DEPT_NAME == loParamPublic.CTO_DEPT_NAME
                        ? loParamPublic.CFROM_DEPT_NAME
                        : $"{loParamPublic.CFROM_DEPT_NAME} to {loParamPublic.CTO_DEPT_NAME}",
                    CheckHandover = loParamPublic.LHANDOVER,
                    CheckComplaint = loParamPublic.LCOMPLAINT,
                    CheckRequest = loParamPublic.LREQUEST,
                    CheckInquiry = loParamPublic.LINQUIRY,
                    CheckSubmitted = loParamPublic.LSUBMITTED,
                    CheckOpen = loParamPublic.LOPEN,
                    CheckAssigned = loParamPublic.LASSIGNED,
                    CheckOnProgress = loParamPublic.LON_PROGRESS,
                    CheckSolved = loParamPublic.LSOLVED,
                    CheckCompleted = loParamPublic.LCOMPLETED,
                    CheckConfirmed = loParamPublic.LCONFIRMED,
                    CheckClosed = loParamPublic.LCLOSED,
                    CheckCancelled = loParamPublic.LCANCELLED,
                    CheckTerminated = loParamPublic.LTERMINATED
                },
                DataMaintenance = loTempDataMaintenance,
                DataPrivate = loTempDataPrivate,
                DataPublic = loTempDataPublic,
                SUM_WITH_INVOICE = loCollectionMaintenance.Count(x => x.CINVOICE_REF_NO != "-"),
                SUM_ESCALATED = loCollectionMaintenance.Count(x => x.LESCALATED),
                SUM_ALL = loCollectionMaintenance.Count()
            };

            return loData;
        }

        public static HDR00200ReportWithBaseHeaderDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO
            {
                CCOMPANY_NAME = "PT. Realta Chakradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "CARE Report",
                CUSER_ID = "RHC"
            };

            var loData = new HDR00200ReportWithBaseHeaderDTO
            {
                BaseHeaderData = loParam,
                Data = DefaultData()
            };

            return loData;
        }
    }
}