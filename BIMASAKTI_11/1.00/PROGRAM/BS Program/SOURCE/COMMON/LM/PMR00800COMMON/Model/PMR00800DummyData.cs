using BaseHeaderReportCOMMON;
using PMR00800COMMON.DTO_s;
using PMR00800COMMON.DTO_s.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMR00800COMMON.Model
{
    public static class PMR00800DummyData
    {
        public static PMR00800PrintDisplayDTO PrintDummyData()
        {
            var loRtn = new PMR00800PrintDisplayDTO();

            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "PMR00800",
                CPRINT_NAME = "Lease Revenue Analysis",
                CUSER_ID = "GHC",
            };

            loRtn.ReportData = new PMR00800ReportDataDTO()
            {
                Header = "Header",
                Title = "Title",
                Label = new PMR00800LabelDTO(),
                Param = new PMR00800ParamDTO()
                {
                    CCOMPANY_ID = "CCOMPANY_ID",
                    CPROPERTY_ID = "CPROPERTY_ID",
                    CPROPERTY_NAME = "CPROPERTY_NAME",
                    CFROM_BUILDING = "CFROM_BUILDING",
                    CTO_BUILDING = "CTO_BUILDING",
                    CFROM_BUILDING_NAME = "CFROM_BUILDING_NAME",
                    CTO_BUILDING_NAME = "CTO_BUILDING_NAME",
                    CLANG_ID = "EN",
                    CPERIOD = "20240815",
                    CPERIOD_MM = "CPERIOD MM",
                    CPERIOD_YYYY = "CPERIOD YYYY",
                    CREPORT_CULTURE = "EN",
                    CREPORT_FILENAME = "PMR00800",
                    CREPORT_FILETYPE = "PDF",
                    CUSER_ID = "CUSER_ID",
                    LIS_PRINT = true
                }
            };
            loRtn.ReportData.Data = new List<PMR00800SpResultDTO>();

            var buildings = new[]
            {
                new { CBUILDING_ID = "B001", CBUILDING_NAME = "Building 1" },
                new { CBUILDING_ID = "B002", CBUILDING_NAME = "Building 2" }
            };

            var units = new[]
            {
                new { CUNIT_ID = "U001", CUNIT_TYPE_ID = "T001", CUNIT_TYPE_NAME = "Type A" },
                new { CUNIT_ID = "U002", CUNIT_TYPE_ID = "T002", CUNIT_TYPE_NAME = "Type B" }
            };

            foreach (var building in buildings)
            {
                foreach (var unit in units)
                {
                    loRtn.ReportData.Data.Add(new PMR00800SpResultDTO
                    {
                        CBUILDING_ID = building.CBUILDING_ID,
                        CBUILDING_NAME = building.CBUILDING_NAME,
                        CFLOOR_ID = "F001",
                        CUNIT_ID = unit.CUNIT_ID,
                        CUNIT_TYPE_ID = unit.CUNIT_TYPE_ID,
                        CUNIT_TYPE_NAME = unit.CUNIT_TYPE_NAME,
                        CLEASE_STATUS = "Leased",
                        CUNIT_DESCRIPTION = "Unit Description",
                        CAGREEMENT_NO = "AG001",
                        CTENANT_ID = "T001",
                        CTENANT_NAME = "Tenant Name",
                        NOCCUPIED = 500,
                        NRENT = 1000,
                        NAVAILABLE = 200,
                        NRENT_PCT = 50,
                        NSC_RENT = 300,
                        NSC_AVAIL = 150,
                        NSC_PCT = 30,
                        NPL_RENT = 100,
                        NPL_AVAIL = 50,
                        NPL_PCT = 10,
                        NSPACE_RENT = 600,
                        NALL_AREA = 1000,
                        NOCC_RATE_PCT = 75
                    });
                }
            }

            var groupedBuildings = loRtn.ReportData.Data
                .GroupBy(b => new { b.CBUILDING_ID, b.CBUILDING_NAME })
                .Select(g => new
                {
                    g.Key.CBUILDING_ID,
                    g.Key.CBUILDING_NAME,
                    NRENT_PCT_TOTAL = (g.Sum(x => x.NRENT) / g.Sum(x => x.NAVAILABLE)) * 100,
                    NSC_PCT_TOTAL = (g.Sum(x => x.NSC_RENT) / g.Sum(x => x.NSC_AVAIL)) * 100,
                    NPL_PCT_TOTAL =( g.Sum(x => x.NPL_RENT) / g.Sum(x => x.NPL_AVAIL)) * 100,
                    NOCC_RATE_PCT_TOTAL = (g.Sum(x => x.NSPACE_RENT) / g.Sum(x => x.NALL_AREA)) * 100
                })
                .ToList(); // Convert to list for easier iteration

            // Update the first row of each building group with total values
            foreach (var buildingGroup in groupedBuildings)
            {
                // Find the first row for the current building group
                var firstRow = loRtn.ReportData.Data
                    .FirstOrDefault(d => d.CBUILDING_ID == buildingGroup.CBUILDING_ID);

                if (firstRow != null)
                {
                    firstRow.NRENT_PCT_TOTAL = buildingGroup.NRENT_PCT_TOTAL;
                    firstRow.NSC_PCT_TOTAL = buildingGroup.NSC_PCT_TOTAL;
                    firstRow.NPL_PCT_TOTAL = buildingGroup.NPL_PCT_TOTAL;
                    firstRow.NOCC_RATE_PCT_TOTAL = buildingGroup.NOCC_RATE_PCT_TOTAL;
                }
            }

            return loRtn;
        }
    }
}
