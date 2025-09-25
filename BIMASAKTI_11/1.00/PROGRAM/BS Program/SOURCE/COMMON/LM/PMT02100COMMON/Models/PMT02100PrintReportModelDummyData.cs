using BaseHeaderReportCOMMON.Models;
using PMT02100COMMON.DTOs.PMT02120Print;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PMT02100COMMON.Models
{
    public class PMT02100PrintReportModelDummyData
    {
        public static PMT02120PrintReportResultDTO DefaultDataReport()
        {
            PMT02120PrintReportResultDTO loData = new PMT02120PrintReportResultDTO()
            {
                Column = new PMT02120PrintReportColumnDTO()
            };

            int lnHeader = 5;
            int lnDetail;
            List<PMT02120PrintReportDTO> loCollection = new List<PMT02120PrintReportDTO>();

            for (int i = 1; i < 30; i++)
            {
                loCollection.Add(new PMT02120PrintReportDTO
                {
                    CCOMPANY_ID = "RCD",
                    CPROPERTY_ID = "ASHMD",
                    CDEPT_CODE = "ACC",
                    CTRANS_CODE = "12345",
                    CREF_NO = $"REF00{i%3}",
                    CUNIT_ID = "UNIT001",
                    CREF_DATE = "20240101",
                    CCONFIRMED_HO_DATE = "20240202",
                    CCONFIRMED_HO_TIME = "10:22",
                    CCONFIRMED_HO_BY = "RAM",
                    CSCHEDULED_HO_DATE = "20240303",
                    CSCHEDULED_HO_TIME = "11:30",
                    IRESCHEDULE_COUNT = 3,
                    IPRINT_COUNT = 5,
                    CPROPERTY_NAME = "Harco Mas",
                    CBUILDING_NAME = "Building 001",
                    CFLOOR_NAME = "Floor 001",
                    CUNIT_NAME = "Unit 001",
                    CUNIT_TYPE_CATEGORY_ID = "UTC001",
                    CUNIT_TYPE_CATEGORY_NAME = "Category 001",
                    CTENANT_NAME = "Tenant 001",
                    CTENANT_PHONE_NO = "081234567890",
                    CTENANT_EMAIL = "testing@gmail.com",
                    LCHECKLIST = true,
                    CEMPLOYEE_ID = $"EMP-00{i}",
                    CEMPLOYEE_NAME = $"Employee 00{i}",
                    CEMPLOYEE_TYPE = $"Type 00{i}",
                    CEMPLOYEE_PHONE_NO = $"0812{i-1}098456{i}",
                    CCATEGORY = $"CAT-00{i}",
                    CITEM = $"ITEM-00{i}",
                    IDEFAULT_QUANTITY = (i*3)%4,
                    CUNIT = "PCS"
                });
            }


            List<PMT02120PrintReportGroupingDTO> loTempData = loCollection
            .GroupBy(item => new
            {
                item.CDEPT_CODE,
                item.CREF_NO,
                item.CREF_DATE,
                item.CCONFIRMED_HO_DATE,
                item.CCONFIRMED_HO_TIME,
                item.CCONFIRMED_HO_BY,
                item.CSCHEDULED_HO_DATE,
                item.CSCHEDULED_HO_TIME,
                item.IRESCHEDULE_COUNT,
                item.IPRINT_COUNT,
                item.CPROPERTY_NAME,
                item.CBUILDING_NAME,
                item.CFLOOR_NAME,
                item.CUNIT_NAME,
                item.CUNIT_TYPE_CATEGORY_NAME,
                item.CTENANT_NAME,
                item.CTENANT_PHONE_NO,
                item.CTENANT_EMAIL
            })
            .Select(header => new PMT02120PrintReportGroupingDTO
            {
                CDEPT_CODE = header.Key.CDEPT_CODE,
                CREF_NO = header.Key.CREF_NO,
                CREF_DATE = header.Key.CREF_DATE,
                DREF_DATE = DateTime.ParseExact(header.Key.CREF_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                DCONFIRMED_DATE = !string.IsNullOrWhiteSpace(header.Key.CCONFIRMED_HO_DATE) && !string.IsNullOrWhiteSpace(header.Key.CCONFIRMED_HO_TIME) ? DateTime.ParseExact($"{header.Key.CCONFIRMED_HO_DATE} {header.Key.CCONFIRMED_HO_TIME}", "yyyyMMdd HH:mm", CultureInfo.InvariantCulture) : (DateTime?)null,
                DCONFIRMED_DATE_ONLY = !string.IsNullOrWhiteSpace(header.Key.CCONFIRMED_HO_DATE) ? DateTime.ParseExact(header.Key.CCONFIRMED_HO_DATE, "yyyyMMdd", CultureInfo.InvariantCulture) : (DateTime?)null,
                CCONFIRMED_BY = header.Key.CCONFIRMED_HO_BY,
                DSCHEDULED_DATE = !string.IsNullOrWhiteSpace(header.Key.CSCHEDULED_HO_DATE) && !string.IsNullOrWhiteSpace(header.Key.CSCHEDULED_HO_TIME) ? DateTime.ParseExact($"{header.Key.CSCHEDULED_HO_DATE} {header.Key.CSCHEDULED_HO_TIME}", "yyyyMMdd HH:mm", CultureInfo.InvariantCulture) : (DateTime?)null,
                DSCHEDULED_DATE_ONLY = !string.IsNullOrWhiteSpace(header.Key.CSCHEDULED_HO_DATE) ? DateTime.ParseExact(header.Key.CSCHEDULED_HO_DATE, "yyyyMMdd", CultureInfo.InvariantCulture) : (DateTime?)null,
                IRESCHEDULE_COUNT = header.Key.IRESCHEDULE_COUNT,
                IPRINT_COUNT = header.Key.IPRINT_COUNT,
                CPROPERTY_NAME = header.Key.CPROPERTY_NAME,
                CBUILDING_NAME = header.Key.CBUILDING_NAME,
                CFLOOR_NAME = header.Key.CFLOOR_NAME,
                CUNIT_NAME = header.Key.CUNIT_NAME,
                CUNIT_TYPE_CATEGORY_NAME = header.Key.CUNIT_TYPE_CATEGORY_NAME,
                CTENANT_NAME = header.Key.CTENANT_NAME,
                CTENANT_PHONE_NO = header.Key.CTENANT_PHONE_NO,
                CTENANT_EMAIL = header.Key.CTENANT_EMAIL,
                FooterData = new PMT02120PrintReportFooterDTO()
                {
                    CMESSAGE_CONFIRMATION = "",
                    CMESSAGE_DUTIES = ""
                },
                EmployeeData = header
                    .Select(Employee => new PMT02120PrintReportEmployeeDTO
                    {
                        CEMPLOYEE = Employee.CEMPLOYEE_ID + " - " + Employee.CEMPLOYEE_NAME,
                        CEMPLOYEE_TYPE = Employee.CEMPLOYEE_TYPE,
                        CEMPLOYEE_PHONE_NO = Employee.CEMPLOYEE_PHONE_NO
                    })
                    .ToList(),
                ChecklistData = header
                    .Select(Checklist => new PMT02120PrintReportChecklistDTO
                    {
                        CCATEGORY = Checklist.CCATEGORY,
                        CITEM = Checklist.CITEM,
                        CSTATUS = "",
                        CNOTES = "",
                        CQUANTITY = Checklist.IDEFAULT_QUANTITY != 0 ? "/" + Checklist.IDEFAULT_QUANTITY.ToString() + " " + Checklist.CUNIT : "-",
                        //CACTUAL_QUANTITY = Checklist.IDEFAULT_QUANTITY != 0 ? "" : "-",
                        IDEFAULT_QUANTITY = Checklist.IDEFAULT_QUANTITY,
                        CUNIT = Checklist.CUNIT,
                        //CUNIT = Checklist.CUNIT
                    })
                    .ToList()
            })
            .ToList();

            loTempData[0].IRESCHEDULE_COUNT = 10;
            loTempData[0].IPRINT_COUNT = 20;
            loTempData[0].DCONFIRMED_DATE = DateTime.Now.AddDays(10).AddHours(10);
            loTempData[0].DSCHEDULED_DATE = DateTime.Now.AddDays(5).AddHours(5);
            loTempData[0].CCONFIRMED_BY = "RAM";

            loTempData[1].IRESCHEDULE_COUNT = 15;
            loTempData[1].IPRINT_COUNT = 25;
            loTempData[1].DCONFIRMED_DATE = DateTime.Now.AddDays(3).AddHours(3);
            loTempData[1].DSCHEDULED_DATE = DateTime.Now.AddDays(6).AddHours(6);
            loTempData[1].CCONFIRMED_BY = "ERC";

            loTempData[2].IRESCHEDULE_COUNT = 0;
            loTempData[2].IPRINT_COUNT = 1;
            loTempData[2].DCONFIRMED_DATE = DateTime.Now;
            loTempData[2].DSCHEDULED_DATE = DateTime.Now;
            loTempData[2].CCONFIRMED_BY = "AOC";

            loData.Data = new PMT02120PrintReportHeaderDTO()
            {
                ReportData = loTempData
            };

            //loData.Data = loCollection;
            loData.LCHECKLIST = true;
            loData.LASSIGNMENT = true;

            return loData;
        }

        public static PMT02120PrintReportResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            PMT02120PrintReportResultWithBaseHeaderPrintDTO loRtn = new PMT02120PrintReportResultWithBaseHeaderPrintDTO();
            loRtn.BaseHeaderData = GenerateDataModelHeader.DefaultData().BaseHeaderData;
            loRtn.ReportData = PMT02100PrintReportModelDummyData.DefaultDataReport();

            return loRtn;
        }
    }
}
