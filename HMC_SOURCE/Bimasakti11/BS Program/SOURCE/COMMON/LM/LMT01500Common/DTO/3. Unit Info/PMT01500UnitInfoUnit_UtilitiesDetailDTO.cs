using System;

namespace PMT01500Common.DTO._3._Unit_Info
{
    public class PMT01500UnitInfoUnit_UtilitiesDetailDTO
    {
        //External Parametr
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUSER_ID { get; set; }
        public string? CSEQ_NO { get; set; } = "";
        //New Version
        public string? CUNIT_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CUTILITY_TYPE_DESCR { get; set; }
        //Real DTO
        public string? CCHARGES_TYPE { get; set; }
        public string? CCHARGES_TYPE_DESCR { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public string? CTAX_ID { get; set; } = "";
        public string? CTAX_NAME { get; set; }
        public string? CMETER_NO { get; set; }
        public Int32 IMETER_START { get; set; }
        public string? CSTART_INV_PRD { get; set; } = "";
        public string? CSTART_INV_PRD_YEAR { get; set; } = "";
        public string? CSTART_INV_PRD_MONTH { get; set; } = "";
        public string? CINV_PERIOD { get; set; }
        public string? CINVGRP_CODE { get; set; }
        public string? CINVGRP_NAME { get; set; }
        public string? CSTATUS { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }


        public void SetYearAndMonthFromCSTART_INV_PRD()
        {
            if (CSTART_INV_PRD != null && CSTART_INV_PRD.Length >= 6)
            {
                CSTART_INV_PRD_YEAR = CSTART_INV_PRD.Substring(0, 4); // Ambil 4 karakter pertama sebagai tahun
                CSTART_INV_PRD_MONTH = CSTART_INV_PRD.Substring(4, 2); // Ambil 2 karakter berikutnya sebagai bulan
            }
        }

        public void SetCSTART_INV_PRDFromYearAndMonth()
        {
            if (CSTART_INV_PRD_YEAR != null && CSTART_INV_PRD_MONTH != null)
            {
                CSTART_INV_PRD = CSTART_INV_PRD_YEAR + CSTART_INV_PRD_MONTH;
            }
        }
    }
}
