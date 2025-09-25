using System;
using System.Globalization;

namespace PMT01300ReportCommon
{
    public class PMT01300ReportDataDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CCOMPANY_NAME { get; set; }
        public string? CCOMPANY_ADDRESS { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CPROPERTY_NAME { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CPROPERTY_CADDRESS { get; set; }
        public string? CPROPERTY_CITY { get; set; }
        public string? CPROPERTY_PROVINCE { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CSTART_TIME { get; set; }
        public string? CEND_TIME { get; set; }
        public int IMONTHS { get; set; }
        public int IYEARS { get; set; }
        public int IDAYS { get; set; }
        public int IHOURS { get; set; }
        public string? CSALESMAN_ID { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CTENANT_ADDRESS { get; set; }
        public string? CTENANT_CITY { get; set; }
        public string? CTENANT_POSTAL_CODE { get; set; }
        public string? CTENANT_EMAIL { get; set; }
        public string? CTENANT_PHONE1 { get; set; }
        public string? CTENANT_PHONE2 { get; set; }
        public string? CTENANT_ATTENTION1_NAME { get; set; }
        public string? CTENANT_ATTENTION1_POSITION { get; set; }
        public string? CTENANT_GROUP_NAME { get; set; }
        public string? CUNIT_DESCRIPTION { get; set; }
        public string? CNOTES { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public string? CCURRENCY_NAME { get; set; }
        public string? CUNIT_LIST { get; set; }
        public decimal NTOTAL_GROSS_AREA { get; set; }
        public decimal NTOTAL_NET_AREA { get; set; }
        public decimal NTOTAL_ACTUAL_AREA { get; set; }
        public decimal NTOTAL_COMMON_AREA { get; set; }
        public decimal NBOOKING_FEE { get; set; }
        public string? CLEASE_MODE { get; set; }
        public string? CLEASE_MODE_DESCR { get; set; }
        public string? CCHARGE_MODE { get; set; }
        public string? CCHARGE_MODE_DESCR { get; set; }
        public string? CTRANS_STATUS { get; set; }
        public string? CTRANS_STATUS_DESCR { get; set; }
        public string? CAGREEMENT_STATUS { get; set; }
        public string? CAGREEMENT_STATUS_DESCR { get; set; }
        public string? CUNIT_TYPE_CATEGORY_ID { get; set; }
        public string? CUNIT_TYPE_CATEGORY_NAME { get; set; }
        public int ITOTAL_UNIT { get; set; }
        public string? CBILLING_RULE_TYPE { get; set; }
        public string? CBILLING_RULE_CODE { get; set; }
        public string? CBILLING_RULE_NAME { get; set; }
        public bool LWITH_DP { get; set; }
        public int IDP_PERCENTAGE { get; set; }
        public int IDP_INTERVAL { get; set; }
        public string? CDP_PERIOD_MODE { get; set; }
        public int IINSTALLMENT_PERCENTAGE { get; set; }
        public int IINSTALLMENT_INTERVAL { get; set; }
        public string? CINSTALLMENT_PERIOD_MODE { get; set; }
        public decimal NLEASE_AMT { get; set; }
        public decimal NLEASE_TOTAL_AMT { get; set; }
        public decimal NLEASE_DP_AMT { get; set; }
        public decimal NLEASE_INSTALLMENT_AMT { get; set; }
        public decimal NCHARGES_AMT { get; set; }
        public decimal NCHARGES_TOTAL_AMT { get; set; }
        public decimal NDEPOSIT_TOTAL_AMT { get; set; }
        public decimal NCAPACITY { get; set; }
        public string? CTC_CODE { get; set; }
        public int IREVISE_SEQ_NO { get; set; }
        public bool LWITH_FO { get; set; }

        public string? CACTUAL_START_TIME { get; set; }
        public string? CACTUAL_END_TIME { get; set; }
        public string? CREC_ID { get; set; }
        public decimal NACTUAL_PRICE { get; set; }
        public decimal NSELL_PRICE { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CFLOOR_NAME { get; set; }
        public string? CLOCATION { get; set; }
        
        
        public string? CNAME01 { get; set; }
        public string? CNAME02 { get; set; }
        public string? CNAME03 { get; set; }
        public string? CNAME04 { get; set; }
        public string? CNAME05 { get; set; }
        public string? CNAME06 { get; set; }
        public string? CPOSITION01 { get; set; }
        public string? CPOSITION02 { get; set; }
        public string? CPOSITION03 { get; set; }
        public string? CPOSITION04 { get; set; }
        public string? CPOSITION05 { get; set; }
        public string? CPOSITION06 { get; set; }
        public byte[]? OSIGN01 { get; set; }
        public byte[]? OSIGN02 { get; set; }
        public byte[]? OSIGN03 { get; set; }
        public byte[]? OSIGN04 { get; set; }
        public byte[]? OSIGN05 { get; set; }
        public byte[]? OSIGN06 { get; set; }


        private string? _CREF_DATE;
        public string? CREF_DATE
        {
            get => _CREF_DATE;
            set
            {
                _CREF_DATE = value;
                DREF_DATE = ConvertStringToDateTimeFormat(value);
            }
        }
        public DateTime? DREF_DATE { get; private set; }

        private string? _CSTART_DATE;
        public string? CSTART_DATE
        {
            get => _CSTART_DATE;
            set
            {
                _CSTART_DATE = value;
                DSTART_DATE = ConvertStringToDateTimeFormat(value);
            }
        }
        public DateTime? DSTART_DATE { get; private set; }

        private string? _CEND_DATE;
        public string? CEND_DATE
        {
            get => _CEND_DATE;
            set
            {
                _CEND_DATE = value;
                DEND_DATE = ConvertStringToDateTimeFormat(value);
            }
        }
        public DateTime? DEND_DATE { get; private set; }

        private string? _CEXPIRED_DATE;
        public string? CEXPIRED_DATE
        {
            get => _CEXPIRED_DATE;
            set
            {
                _CEXPIRED_DATE = value;
                DEXPIRED_DATE = ConvertStringToDateTimeFormat(value);
            }
        }
        public DateTime? DEXPIRED_DATE { get; private set; }

        private string? _CHO_PLAN_DATE;
        public string? CHO_PLAN_DATE
        {
            get => _CHO_PLAN_DATE;
            set
            {
                _CHO_PLAN_DATE = value;
                DHO_PLAN_DATE = ConvertStringToDateTimeFormat(value);
            }
        }
        public DateTime? DHO_PLAN_DATE { get; private set; }

        private string? _CHO_ACTUAL_DATE;
        public string? CHO_ACTUAL_DATE
        {
            get => _CHO_ACTUAL_DATE;
            set
            {
                _CHO_ACTUAL_DATE = value;
                DHO_ACTUAL_DATE = ConvertStringToDateTimeFormat(value);
            }
        }
        public DateTime? DHO_ACTUAL_DATE { get; private set; }

        private string? _CACTUAL_START_DATE;
        public string? CACTUAL_START_DATE
        {
            get => _CACTUAL_START_DATE;
            set
            {
                _CACTUAL_START_DATE = value;
                DACTUAL_START_DATE = ConvertStringToDateTimeFormat(value);
            }
        }
        public DateTime? DACTUAL_START_DATE { get; private set; }

        private string? _CACTUAL_END_DATE;
        public string? CACTUAL_END_DATE
        {
            get => _CACTUAL_END_DATE;
            set
            {
                _CACTUAL_END_DATE = value;
                DACTUAL_END_DATE = ConvertStringToDateTimeFormat(value);
            }
        }
        public DateTime? DACTUAL_END_DATE { get; private set; }

        private string? _CSOLD_DATE;
        public string? CSOLD_DATE
        {
            get => _CSOLD_DATE;
            set
            {
                _CSOLD_DATE = value;
                DSOLD_DATE = ConvertStringToDateTimeFormat(value);
            }
        }
        public DateTime? DSOLD_DATE { get; private set; }

        //Utility
        public DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                return null;
            }

            // Jika string hanya memiliki 6 karakter (YYYYMM), tambahkan "01" sebagai tanggal
            if (pcEntity.Length == 6)
            {
                pcEntity += "01";
            }

            if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            return null;
        }
    }
}