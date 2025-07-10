using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02120Print
{
    public class PMT02120PrintReportDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUNIT_ID { get; set; }
        public string CREF_DATE { get; set; }
        public string CCONFIRMED_HO_DATE { get; set; }
        public string CCONFIRMED_HO_TIME { get; set; } // Consider combining date and time if needed
        public string CCONFIRMED_HO_BY { get; set; }
        public string CSCHEDULED_HO_DATE { get; set; } // Can be converted to DateTime if parsed
        public string CSCHEDULED_HO_TIME { get; set; } // Same as above
        public int IRESCHEDULE_COUNT { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CFLOOR_NAME { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CUNIT_TYPE_CATEGORY_ID { get; set; }
        public string CUNIT_TYPE_CATEGORY_NAME { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CTENANT_PHONE_NO { get; set; }
        public string CTENANT_EMAIL { get; set; }
        public bool LCHECKLIST { get; set; }
        public string CEMPLOYEE_ID { get; set; }
        public string CEMPLOYEE_NAME { get; set; }
        public string CEMPLOYEE_TYPE { get; set; }
        public string CEMPLOYEE_PHONE_NO { get; set; }
        public string CCATEGORY { get; set; }
        public string CITEM { get; set; }
        public int IDEFAULT_QUANTITY { get; set; }

        //LOGO
        public byte[] OLOGO { get; set; }
        public string CCOMPANY_NAME { get; set; }
        public string CDATETIME_NOW { get; set; }

        //CR02
        public int IPRINT_COUNT { get; set; }

        //CR05
        public string CUNIT { get; set; }
    }
}
