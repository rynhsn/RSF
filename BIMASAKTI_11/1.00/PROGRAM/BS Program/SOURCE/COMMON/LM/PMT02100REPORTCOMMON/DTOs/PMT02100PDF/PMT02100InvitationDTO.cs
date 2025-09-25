using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100REPORTCOMMON.DTOs.PMT02100PDF
{
    public class PMT02100InvitationDTO
    {
        public string CREF_NO { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CFLOOR_ID { get; set; }

        public string CSCHEDULED_HO_DATE { get; set; }
        public string CSCHEDULED_HO_TIME { get; set; }
        public DateTime? DSCHEDULED_HO_DATE { get; set; }
        public string CSCHEDULED_HO_BY { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CUNIT_ID { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public string CUNIT_TYPE_CATEGORY_NAME { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CTENANT_PHONE_NO { get; set; }
        public string CTENANT_EMAIL { get; set; }
        public int IINVOICE_PERIOD { get; set; }
        public decimal NGRAND_TOTAL { get; set; }    
        public string CCURRENCY_SYMBOL { get; set; }
        public bool LWITH_DETAIL { get; set; }
        public List<PMT02100InvitationDetailDTO> Detail { get; set; }
    }
}
