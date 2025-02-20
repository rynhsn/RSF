using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BaseHeaderReportCOMMON;
using PMR00460Common.DTOs.Print;

namespace PMR00460Common.Model
{
    public class PMR00460ModelReportDummyData
    {
        public static PMR00460ReportResultDTO DefaultData()
        {
            var loCollection = new List<PMR00460DataResultDTO>();

            loCollection.Add(
                new PMR00460DataResultDTO
                {
                    CBUILDING_ID = "TW-1",
                    CBUILDING_NAME = "Tower 1",
                    CSCHEDULE_DATE = "20241111",
                    CUNIT_ID = "L1-2BR-001",
                    CUNIT_TYPE_CATEGORY = "2BR",
                    CREF_NO = "AGRU-2024110005",
                    CTENANT_ID = "TNT09",
                    CTENANT_NAME = "Christian",
                    CTENANT_EMAIL = "christian@gmail.com",
                    CSTAFF_ID = "STF012",
                    CSTAFF_NAME = "Andi",
                    CHANDOVER_STATUS = "Confirmed",
                    CCHECKLIST_DESCRIPTION = "Kamar Tidur 1",
                    CNOTES = "ini notes",
                    LCHECKLIST_STATUS = true,
                    CCARE_REF_NO = "",
                    IDEFAULT_QUANTITY = 5,
                    IACTUAL_QUANTITY = 2,
                    LCHECKLIST = true
                });
            
            loCollection.Add(
                new PMR00460DataResultDTO
                {
                    CBUILDING_ID = "TW-1",
                    CBUILDING_NAME = "Tower 1",
                    CSCHEDULE_DATE = "20241111",
                    CUNIT_ID = "L1-2BR-001",
                    CUNIT_TYPE_CATEGORY = "2BR",
                    CREF_NO = "AGRU-2024110005",
                    CTENANT_ID = "TNT09",
                    CTENANT_NAME = "Christian",
                    CTENANT_EMAIL = "christian@gmail.com",
                    CSTAFF_ID = "STF010",
                    CSTAFF_NAME = "Jessica",
                    CHANDOVER_STATUS = "Confirmed",
                    CCHECKLIST_DESCRIPTION = "Dapur",
                    CNOTES = "Test Note",
                    LCHECKLIST_STATUS = false,
                    CCARE_REF_NO = "CARE-HO-202411-L1-001",
                    IDEFAULT_QUANTITY = 0,
                    IACTUAL_QUANTITY = 0,
                    LCHECKLIST = true
                });
            
            loCollection.Add(
                new PMR00460DataResultDTO
                {
                    CBUILDING_ID = "TW-1",
                    CBUILDING_NAME = "Tower 1",
                    CSCHEDULE_DATE = "20241111",
                    CUNIT_ID = "L1-2BR-001",
                    CUNIT_TYPE_CATEGORY = "2BR",
                    CREF_NO = "AGRU-2024110005",
                    CTENANT_ID = "TNT09",
                    CTENANT_NAME = "Christian",
                    CTENANT_EMAIL = "christian@gmail.com",
                    CSTAFF_ID = "STF012",
                    CSTAFF_NAME = "Andi",
                    CHANDOVER_STATUS = "Confirmed",
                    CCHECKLIST_DESCRIPTION = "Kamar Tidur 1",
                    CNOTES = "ini notes",
                    LCHECKLIST_STATUS = true,
                    CCARE_REF_NO = "",
                    IDEFAULT_QUANTITY = 5,
                    IACTUAL_QUANTITY = 2,
                    LCHECKLIST = false
                });
            
            loCollection.Add(
                new PMR00460DataResultDTO
                {
                    CBUILDING_ID = "TW-1",
                    CBUILDING_NAME = "Tower 1",
                    CSCHEDULE_DATE = "20241111",
                    CUNIT_ID = "L1-2BR-001",
                    CUNIT_TYPE_CATEGORY = "2BR",
                    CREF_NO = "AGRU-2024110005",
                    CTENANT_ID = "TNT09",
                    CTENANT_NAME = "Christian",
                    CTENANT_EMAIL = "christian@gmail.com",
                    CSTAFF_ID = "STF010",
                    CSTAFF_NAME = "Jessica",
                    CHANDOVER_STATUS = "Confirmed",
                    CCHECKLIST_DESCRIPTION = "Dapur",
                    CNOTES = "Test Note",
                    LCHECKLIST_STATUS = false,
                    CCARE_REF_NO = "CARE-HO-202411-L1-001",
                    IDEFAULT_QUANTITY = 0,
                    IACTUAL_QUANTITY = 0,
                    LCHECKLIST = false
                });

            
            loCollection.Add(
                new PMR00460DataResultDTO
                {
                    CBUILDING_ID = "TW-1",
                    CBUILDING_NAME = "Tower 1",
                    CSCHEDULE_DATE = "20241111",
                    CUNIT_ID = "L1-2BR-001",
                    CUNIT_TYPE_CATEGORY = "2BR",
                    CREF_NO = "AGRU-2024110006",
                    CTENANT_ID = "TNT09",
                    CTENANT_NAME = "Christian",
                    CTENANT_EMAIL = "christian@gmail.com",
                    CSTAFF_ID = "STF012",
                    CSTAFF_NAME = "Andi",
                    CHANDOVER_STATUS = "Confirmed",
                    CCHECKLIST_DESCRIPTION = "Kamar Tidur 1",
                    CNOTES = "ini notes",
                    LCHECKLIST_STATUS = true,
                    CCARE_REF_NO = "",
                    IDEFAULT_QUANTITY = 5,
                    IACTUAL_QUANTITY = 2,
                    LCHECKLIST = true
                });
            
            loCollection.Add(
                new PMR00460DataResultDTO
                {
                    CBUILDING_ID = "TW-1",
                    CBUILDING_NAME = "Tower 1",
                    CSCHEDULE_DATE = "20241111",
                    CUNIT_ID = "L1-2BR-001",
                    CUNIT_TYPE_CATEGORY = "2BR",
                    CREF_NO = "AGRU-2024110006",
                    CTENANT_ID = "TNT09",
                    CTENANT_NAME = "Christian",
                    CTENANT_EMAIL = "christian@gmail.com",
                    CSTAFF_ID = "STF010",
                    CSTAFF_NAME = "Jessica",
                    CHANDOVER_STATUS = "Confirmed",
                    CCHECKLIST_DESCRIPTION = "Dapur",
                    CNOTES = "Test Note",
                    LCHECKLIST_STATUS = false,
                    CCARE_REF_NO = "CARE-HO-202411-L1-001",
                    IDEFAULT_QUANTITY = 0,
                    IACTUAL_QUANTITY = 0,
                    LCHECKLIST = true
                });
            
            loCollection.Add(
                new PMR00460DataResultDTO
                {
                    CBUILDING_ID = "TW-1",
                    CBUILDING_NAME = "Tower 1",
                    CSCHEDULE_DATE = "20241111",
                    CUNIT_ID = "L1-2BR-001",
                    CUNIT_TYPE_CATEGORY = "2BR",
                    CREF_NO = "AGRU-2024110006",
                    CTENANT_ID = "TNT09",
                    CTENANT_NAME = "Christian",
                    CTENANT_EMAIL = "christian@gmail.com",
                    CSTAFF_ID = "STF012",
                    CSTAFF_NAME = "Andi",
                    CHANDOVER_STATUS = "Confirmed",
                    CCHECKLIST_DESCRIPTION = "Kamar Tidur 1",
                    CNOTES = "ini notes",
                    LCHECKLIST_STATUS = true,
                    CCARE_REF_NO = "",
                    IDEFAULT_QUANTITY = 5,
                    IACTUAL_QUANTITY = 2,
                    LCHECKLIST = false
                });
            
            loCollection.Add(
                new PMR00460DataResultDTO
                {
                    CBUILDING_ID = "TW-1",
                    CBUILDING_NAME = "Tower 1",
                    CSCHEDULE_DATE = "20241111",
                    CUNIT_ID = "L1-2BR-001",
                    CUNIT_TYPE_CATEGORY = "2BR",
                    CREF_NO = "AGRU-2024110006",
                    CTENANT_ID = "TNT09",
                    CTENANT_NAME = "Christian",
                    CTENANT_EMAIL = "christian@gmail.com",
                    CSTAFF_ID = "STF010",
                    CSTAFF_NAME = "Jessica",
                    CHANDOVER_STATUS = "Confirmed",
                    CCHECKLIST_DESCRIPTION = "Dapur",
                    CNOTES = "Test Note",
                    LCHECKLIST_STATUS = false,
                    CCARE_REF_NO = "CARE-HO-202411-L1-001",
                    IDEFAULT_QUANTITY = -1,
                    IACTUAL_QUANTITY = 0,
                    LCHECKLIST = false
                });

            var loResult = new List<PMR00460ReportDataDTO>();
            
            var loGroupBuilding = loCollection.GroupBy(x => x.CBUILDING_ID).ToList();

            foreach (var item in loGroupBuilding)
            {
                //group by ref no dan LCHECKLIST = false
                // var loGroupLoi = item.GroupBy(x => x.CREF_NO).Where(x => x.All(y => y.LCHECKLIST == false)).ToList();
                var loGroupLoi = item.GroupBy(x => x.CREF_NO).ToList();
                var loGroup = new PMR00460ReportDataDTO()
                {
                    CBUILDING_ID =item.Key,
                    CBUILDING_NAME = item.First().CBUILDING_NAME,
                    LOI = new List<PMR00460GroupBuildingDTO>()
                };
                
                foreach (var item1 in loGroupLoi)
                {
                    var loGroupLoiSummary = item1.Where(x => x.LCHECKLIST == false).ToList();
                    var loGroupLoiDetail = item1.Where(x => x.LCHECKLIST == true).ToList();
                    var loGroupByLoi = new PMR00460GroupBuildingDTO()
                    {
                        CREF_NO = item1.Key,
                        Summary = new List<PMR00460GroupByLoiSummaryDTO>(),
                        Detail = new List<PMR00460GroupByLoiDetailDTO>()
                    };
                    
                    foreach (var item2 in loGroupLoiSummary)
                    {
                        loGroupByLoi.Summary.Add(new PMR00460GroupByLoiSummaryDTO()
                        {
                            CREF_NO = item2.CREF_NO,
                            LCHECKLIST = item2.LCHECKLIST,
                            CSCHEDULE_DATE = item2.CSCHEDULE_DATE,
                            DSCHEDULE_DATE = DateTime.TryParseExact(item2.CSCHEDULE_DATE, "yyyyMMdd",
                                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate)
                                ? refDate : (DateTime?)null,
                            CUNIT_ID = item2.CUNIT_ID,
                            CUNIT_TYPE_CATEGORY = item2.CUNIT_TYPE_CATEGORY,
                            CTENANT_ID = item2.CTENANT_ID,
                            CTENANT_NAME = item2.CTENANT_NAME,
                            CTENANT_EMAIL = item2.CTENANT_EMAIL,
                            CSTAFF_ID = item2.CSTAFF_ID,
                            CSTAFF_NAME = item2.CSTAFF_NAME,
                            CHANDOVER_STATUS = item2.CHANDOVER_STATUS
                        });
                    }
                    
                    foreach (var item2 in loGroupLoiDetail)
                    {
                        loGroupByLoi.Detail.Add(new PMR00460GroupByLoiDetailDTO()
                        {
                            CCHECKLIST_DESCRIPTION = item2.CCHECKLIST_DESCRIPTION,
                            CNOTES = item2.CNOTES,
                            CCHECKLIST_STATUS = item2.LCHECKLIST_STATUS ? "OK" : "Not OK",
                            CCARE_REF_NO = item2.CCARE_REF_NO,
                            IDEFAULT_QUANTITY = item2.IDEFAULT_QUANTITY,
                            IACTUAL_QUANTITY = item2.IACTUAL_QUANTITY
                        });
                    }
                    
                    loGroup.LOI.Add(loGroupByLoi);
                }
                
                loResult.Add(loGroup);
            }
            
            var loData = new PMR00460ReportResultDTO
            {
                Title = "Supplier Statement",
                Label = new PMR00460ReportLabelDTO(),
                Header = new PMR00460ReportHeaderDTO
                {
                    CPROPERTY_NAME = "Harco Mas",
                    CBUILDING = "Building A to Building B",
                    CPERIOD = "January 2024 to December 2024",
                    CREPORT_TYPE = "D",
                    CREPORT_TYPE_NAME = "Detail",
                    LOPEN = true,
                    LSCHEDULED = true,
                    LCONFIRMED = true,
                    LCLOSED = true,
                    CTYPE = "S",
                    CTYPE_NAME = "Strata"
                },
                Data = loResult
            };

            foreach (var item in loData.Data)
            {
                item.LOI.ForEach(x => x.Summary.ForEach(y =>
                {
                    y.DSCHEDULE_DATE = DateTime.TryParseExact(y.CSCHEDULE_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var scheduleDate) ? scheduleDate : (DateTime?)null;
                }));
            }

            return loData;
        }

        public static PMR00460ReportWithBaseHeaderDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO
            {
                CCOMPANY_NAME = "PT. Realta Chakradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Handover Report",
                CUSER_ID = "RHC"
            };

            var loData = new PMR00460ReportWithBaseHeaderDTO
            {
                BaseHeaderData = loParam,
                Data = DefaultData()
            };

            return loData;
        }
    }
}