using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Charges
{
    public class PMT01700LOO_UnitCharges_ChargesListDTO
    {
        /// FOR PARAM
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CREF_NO { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }

        public string? CSEQ_NO { get; set; }
        public string? CCHARGES_TYPE { get; set; }
        public string? CCHARGES_TYPE_DESCR { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public bool LITEM { get; set; }
        public string? CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; } = DateTime.Now;
        public string? CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; } =  DateTime.Now;
        public int IINTERVAL { get; set; }
        public string? CPERIOD_MODE { get; set; }
        public string? CINVOICE_PERIOD_DESCR { get; set; }
        public decimal NFEE_AMT { get; set; }
        public decimal NINVOICE_AMT { get; set; }
        public decimal NTOTAL_AMT { get; set; }
        public string?CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public bool LBASED_OPEN_DATE { get; set; }
        public bool LCAL_UNIT { get; set; }
        public bool LTOTAL_PRICE { get; set; }
        public bool LOVERWRITE { get; set; }
        public decimal NBOTTOM_PRICE { get; set; }

        public string? CHARGE_NAME_AND_ID 
        {
            get => _CHARGE_NAME_AND_ID;
            set => _CHARGE_NAME_AND_ID = CCHARGES_ID + " (" + CCHARGES_NAME + ")";
        }
        private string? _CHARGE_NAME_AND_ID;

 
        //NOT USED
        /*
        public string CFLOOR_ID { get; set; }
        public string CUNIT_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CTAX_ID { get; set; }
        public string CBILLING_MODE { get; set; }
        public string CFEE_METHOD { get; set; }
        public string CINVOICE_PERIOD { get; set; }
        public string CDESCRIPTION { get; set; }
        public string LACTIVE { get; set; }   
        public string CACTIVE_BY { get; set; }
        public string DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; }
        public string DINACTIVE_DATE { get; set; }
        */
    }
}
