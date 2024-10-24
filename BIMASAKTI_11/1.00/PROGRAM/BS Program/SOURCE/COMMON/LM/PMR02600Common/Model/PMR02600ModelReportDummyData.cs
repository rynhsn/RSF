using System;
using System.Collections.Generic;
using System.Globalization;
using BaseHeaderReportCOMMON;
using PMR02600Common.DTOs.Print;

namespace PMR02600Common.Model
{
    public class PMR02600ModelReportDummyData
    {
        public static PMR02600ReportResultDTO DefaultData()
        {
            var loCollection = new List<PMR02600DataResultDTO>
            {
                new PMR02600DataResultDTO
                {
                    CBUILDING_ID = "TOWER1",
                    CBUILDING_NAME = "TOWER WULAN",
                    CUNIT_ID = "Unit001",
                    CUNIT_TYPE_NAME = "Anchor Anchor Anchor Anchor Anchor Anchor Anchor Anchor Anchor Anchor Anchor Anchor Anchor Anchor Anchor Anchor",
                    CUNIT_DESCRIPTION = "",
                    CTENANT_ID = "C0826",
                    CTENANT_NAME = "PT. PANCA",
                    NLEASED_AREA = 39.39M,
                    NOCCUPIABLE_AREA = 39.39M,
                    NOCCUPANCY = 100M,
                    NTOTAL_LEASE_AREA = 184M,
                    NTOTAL_LEASED_AREA = 184M,
                    NAVAIL_AREA = 0M,
                    NTOTAL_LEASE_AREA_PCT = 100M,
                    NTOTAL_LEASED_AREA_PCT = 100M,
                    NAVAIL_AREA_PCT = 0M
                },
                new PMR02600DataResultDTO
                {
                    CBUILDING_ID = "TOWER1",
                    CBUILDING_NAME = "TOWER WULAN",
                    CUNIT_ID = "Unit002",
                    CUNIT_TYPE_NAME = "Big Shop",
                    CUNIT_DESCRIPTION = "TEST DESCRIPTION",
                    CTENANT_ID = "C1138",
                    CTENANT_NAME = "PT. J.CO",
                    NLEASED_AREA = 144.97M,
                    NOCCUPIABLE_AREA = 144.97M,
                    NOCCUPANCY = 100M,
                    NTOTAL_LEASE_AREA = 184M,
                    NTOTAL_LEASED_AREA = 184M,
                    NAVAIL_AREA = 0M,
                    NTOTAL_LEASE_AREA_PCT = 100M,
                    NTOTAL_LEASED_AREA_PCT = 100M,
                    NAVAIL_AREA_PCT = 0M
                },
                new PMR02600DataResultDTO
                {
                    CBUILDING_ID = "TOWER2",
                    CBUILDING_NAME = "MULAN TOWER",
                    CUNIT_ID = "Unit001",
                    CUNIT_TYPE_NAME = "Anchor",
                    CUNIT_DESCRIPTION = "",
                    CTENANT_ID = "C0826",
                    CTENANT_NAME = "PT. PANCA",
                    NLEASED_AREA = 39.39M,
                    NOCCUPIABLE_AREA = 39.39M,
                    NOCCUPANCY = 100M,
                    NTOTAL_LEASE_AREA = 184M,
                    NTOTAL_LEASED_AREA = 184M,
                    NAVAIL_AREA = 0M,
                    NTOTAL_LEASE_AREA_PCT = 100M,
                    NTOTAL_LEASED_AREA_PCT = 100M,
                    NAVAIL_AREA_PCT = 0M
                },
                new PMR02600DataResultDTO
                {
                    CBUILDING_ID = "TOWER2",
                    CBUILDING_NAME = "MULAN TOWER",
                    CUNIT_ID = "Unit002",
                    CUNIT_TYPE_NAME = "Big Shop",
                    CUNIT_DESCRIPTION = "TEST DESCRIPTION",
                    CTENANT_ID = "C1138",
                    CTENANT_NAME = "PT. J.CO",
                    NLEASED_AREA = 144.97M,
                    NOCCUPIABLE_AREA = 144.97M,
                    NOCCUPANCY = 100M,
                    NTOTAL_LEASE_AREA = 184M,
                    NTOTAL_LEASED_AREA = 184M,
                    NAVAIL_AREA = 0M,
                    NTOTAL_LEASE_AREA_PCT = 100M,
                    NTOTAL_LEASED_AREA_PCT = 100M,
                    NAVAIL_AREA_PCT = 0M
                },
            };

            var loData = new PMR02600ReportResultDTO
            {
                Title = "Occupancy Report",
                Label = new PMR02600ReportLabelDTO(),
                Header = new PMR02600ReportHeaderDTO
                {
                    CPROPERTY = "Harco Mas(ASHMD)",
                    CFROM_BUILDING = "TOWER WULAN(TOWER1)",
                    CTO_BUILDING = "MULAN TOWER(TOWER2)",
                    CPERIOD = "20240725"
                },
                Data = loCollection
            };

            loData.Header.CPERIOD_DISPLAY = DateTime.TryParseExact(loData.Header.CPERIOD, "yyyyMMdd",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate)
                ? refDate.ToString("dd MMM yyyy")
                : null;
            
            loData.Header.DPERIOD = DateTime.TryParseExact(loData.Header.CPERIOD, "yyyyMMdd",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var period)
                ? period
                : (DateTime?)null;

            return loData;
        }

        public static PMR02600ReportWithBaseHeaderDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO
            {
                CCOMPANY_NAME = "PT. Realta Chakradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Occupancy Report",
                CUSER_ID = "rhc"
            };

            var loData = new PMR02600ReportWithBaseHeaderDTO
            {
                BaseHeaderData = loParam,
                Data = DefaultData()
            };

            return loData;
        }
    }
}