using System;
using System.Collections.Generic;

namespace PMR02600Common.DTOs.Print
{
    public class PMR02600ReportResultDTO
    {
        public string Title { get; set; }

        public PMR02600ReportLabelDTO Label { get; set; }

        public PMR02600ReportHeaderDTO Header { get; set; }
        public List<PMR02600DataResultDTO> Data { get; set; }
    }
    
    public class PMR02600DataResultDTO
    {
        public string CBUILDING_ID { get; set; } = "";
        public string CBUILDING_NAME { get; set; } = "";
        public string CFLOOR_ID { get; set; } = "";
        public string CUNIT_ID { get; set; } = "";
        public string CUNIT_TYPE_ID { get; set; } = "";
        public string CUNIT_TYPE_NAME { get; set; } = "";
        public string CLEASE_STATUS { get; set; } = "";
        public string CACTUAL_START_DATE { get; set; } = "";
        public string CACTUAL_END_DATE { get; set; } = "";
        public string CUNIT_DESCRIPTION { get; set; } = "";
        public string CTENANT_ID { get; set; } = "";
        public string CTENANT_NAME { get; set; } = "";
        public decimal NLEASED_AREA { get; set; }
        public decimal NOCCUPIABLE_AREA { get; set; }
        public decimal NOCCUPANCY { get; set; }
        public decimal NTOTAL_LEASED_AREA { get; set; }
        public decimal NTOTAL_LEASE_AREA { get; set; }
        public decimal NAVAIL_AREA { get; set; }
        public decimal NTOTAL_LEASE_AREA_PCT { get; set; }
        public decimal NTOTAL_LEASED_AREA_PCT { get; set; }
        public decimal NAVAIL_AREA_PCT { get; set; }
    }

    public class PMR02600ReportLabelDTO
    {
        public string PROPERTY { get; set; } = "Property";
        public string BUILDING { get; set; } = "Building";
        public string CUT_OFF_DATE { get; set; } = "Cut Off Date";
        public string UNIT_ID { get; set; } = "Unit Id";
        public string UNIT_TYPE { get; set; } = "Unit Type";
        public string UNIT_DESC { get; set; } = "Unit Description";
        public string TENANT_ID { get; set; } = "Tenant Id";
        public string TENANT_NAME { get; set; } = "Tenant Name";
        public string LEASED_AREA { get; set; } = "Leased Area";
        public string OCCUPIABLE_AREA { get; set; } = "Occupiable Area";
        public string OCCUPANCY { get; set; } = "Occupancy";
        public string TOTAL_LEASE_AREA { get; set; } = "Total Lease Area";
        public string TOTAL_LEASED_AREA { get; set; } = "Total Leased Area";
        public string TOTAL_AVAILABLE { get; set; } = "Total Available";
        public string S_D { get; set; } = "s/d";
    }
    
    public class PMR02600ReportHeaderDTO
    {
        public string CPROPERTY { get; set; } = "";
        public string CFROM_BUILDING { get; set; } = "";
        public string CTO_BUILDING { get; set; } = "";
        public string CPERIOD { get; set; } = "";
        public string CPERIOD_DISPLAY { get; set; } = "";
    }

    public class PMR02600ReportWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMR02600ReportResultDTO Data { get; set; }
    }
}