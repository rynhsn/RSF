using System;

namespace PMA00300COMMON
{
    public class PMA00300DataReportDTO
    {
        //HEADER
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CSTATEMENT_DATE { get; set; }
        public DateTime DSTATEMENT_DATE { get; set; }
        public string CDUE_DATE { get; set; }
        public DateTime DDUE_DATE { get; set; }
        public string CLOI_AGRMT_REC_ID { get; set; } = "";
        public string CSTORAGE_ID { get; set; }
        public string CUSER_ID { get; set; }
        //SUBHEADER
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CBILLING_ADDRESS { get; set; }
        public string CREF_NO { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CUNIT_ID_LIST { get; set; }
        public string CUNIT_DESCRIPTION { get; set; }
        public string CCURRENCY { get; set; }
        public string CCURRENCY_CODE { get; set; }

        public decimal NPREVIOUS_BALANCE { get; set; }
        public decimal NPREVIOUS_PAYMENT { get; set; }
        public decimal NCURRENT_PENALTY { get; set; }
        public decimal NNEW_BILLING        { get; set; }
        public decimal NNEW_BALANCE { get; set; }
        //SUPRESS if value 0
        public decimal NSALES { get; set; }
        public decimal NRENT { get; set; }
        public decimal NDEPOSIT { get; set; }
        public decimal NREVENUE_SHARING { get; set; }
        public decimal NSERVICE_CHARGE { get; set; }
        public decimal NSINKING_FUND { get; set; }
        public decimal NPROMO_LEVY { get; set; }
        public decimal NGENERAL_CHARGE { get; set; }
        public decimal NELECTRICITY { get; set; }
        public decimal NCHILLER { get; set; }
        public decimal NWATER { get; set; }
        public decimal NGAS { get; set; }
        public decimal NPARKING { get; set; }
        public decimal NOVERTIME { get; set; }
        public decimal NGENERAL_UTILITY { get; set; }
    }
}
