using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02100
{
    public class PMT02100HandoverDTO
    {
        //Open
        public bool LSELECTED { get; set; }
        public string CUNIT_TYPE_CATEGORY_ID { get; set; }
        public string CUNIT_TYPE_CATEGORY_NAME { get; set; }
        public string CUNIT_ID { get; set; }
        public string CHO_PLAN_DATE { get; set; }
        public DateTime? DHO_PLAN_DATE { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CTENANT_DISPLAY { get; set; }
        public string CTENANT_PHONE_NO { get; set; }
        public string CTENANT_EMAIL { get; set; }
        public string CTENANT_CITY_DESCRIPTION { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }

        //Scheduled
        public string CSCHEDULED_HO_DATE { get; set; }
        public DateTime? DSCHEDULED_HO_DATE { get; set; }
        public string CSCHEDULED_HO_TIME { get; set; }
        public string CSCHEDULED_HO_TIME_HOURS { get; set; }
        public string CSCHEDULED_HO_TIME_MINUTES { get; set; }
        public int ISCHEDULED_HO_TIME_HOURS { get; set; }
        public int ISCHEDULED_HO_TIME_MINUTES { get; set; }

        //Confirm
        public string CHO_EXPIRY_DATE { get; set; }
        public bool LEXPIRED { get; set; }
        public DateTime? DHO_EXPIRY_DATE { get; set; }
        public int IPRINT_COUNT { get; set; }
        public int IREINVITATION_COUNT { get; set; }
        public string CSCHEDULED_HO_BY { get; set; }

        //History
        public string CHO_ACTUAL_DATE { get; set; }
        public DateTime? DHO_ACTUAL_DATE { get; set; }
        public bool LFORCED_HO { get; set; }

        //Confirm Schedule
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }

        //Reschedule
        public string CREASON { get; set; }

        //Hidden
        public string CTRANS_CODE { get; set; }
    }
}
