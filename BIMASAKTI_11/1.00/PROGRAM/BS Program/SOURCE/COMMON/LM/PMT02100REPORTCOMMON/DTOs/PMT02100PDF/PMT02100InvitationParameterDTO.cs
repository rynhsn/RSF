using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100REPORTCOMMON.DTOs.PMT02100PDF
{
    public class PMT02100InvitationParameterDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUSER_ID { get; set; }


        //Confirm Schedule Parameter
        //public string CPROPERTY_ID { get; set; }
        //public string CDEPT_CODE { get; set; }
        //public string CTRANS_CODE { get; set; }
        //public string CREF_NO { get; set; }
        public string CSCHEDULED_HO_DATE { get; set; }
        public string CSCHEDULED_HO_TIME { get; set; }
        public string CTENANT_EMAIL { get; set; }
        public bool LCONFIRM_SCHEDULE { get; set; }
    }
}
