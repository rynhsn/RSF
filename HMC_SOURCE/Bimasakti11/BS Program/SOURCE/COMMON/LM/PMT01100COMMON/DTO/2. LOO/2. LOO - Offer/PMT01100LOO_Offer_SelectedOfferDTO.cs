﻿using PMT01100Common.DTO._1._Unit_List;
using System;
using System.Collections.Generic;

namespace PMT01100Common.DTO._2._LOO._2._LOO___Offer
{
    public class PMT01100LOO_Offer_SelectedOfferDTO
    {
        //Updated 5 Juni 2024 : For sign the Data is New Prospect or Existing
        public string? CMODE_CRUD { get; set; }
        //For Front
        public int IDAYS { get; set; }
        public int IMONTH { get; set; }
        public int IYEAR { get; set; }

        public DateTime? DFOLLOW_UP_DATE { get; set; }
        public DateTime? DEXPIRED_DATE { get; set; }
        public DateTime? DHAND_OVER_DATE { get; set; }
        public DateTime? DID_EXPIRED_DATE { get; set; }
        //public DateTime? DDOC_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }

        //Real DTOs
        public string? CTENANT_NAME { get; set; }
        public string? CADDRESS { get; set; }
        public string? CEMAIL { get; set; }
        public string? CPHONE1 { get; set; }
        public string? CPHONE2 { get; set; }
        public string? CTENANT_CATEGORY_ID { get; set; }
        public string? CTAX_TYPE { get; set; }
        public string? CTAX_ID { get; set; }
        public string? CTAX_NAME { get; set; }
        public string? CID_TYPE { get; set; }
        public string? CID_NO { get; set; }
        public string? CID_EXPIRED_DATE { get; set; }
        public string? CTAX_ADDRESS { get; set; }
        public string? CATTENTION1_NAME { get; set; }
        public string? CATTENTION1_EMAIL { get; set; }
        public string? CATTENTION1_MOBILE_PHONE1 { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public string? CORIGINAL_REF_NO { get; set; }
        public string? CFOLLOW_UP_DATE { get; set; }
        public string? CEXPIRED_DATE { get; set; }
        public string? CHAND_OVER_DATE { get; set; }
        public string? CTRANS_STATUS_DESCR { get; set; } = "";
        public string? CAGREEMENT_STATUS_DESCR { get; set; } = "";
        public string? CBILLING_RULE_CODE { get; set; } = "";
        public string? CBILLING_RULE_NAME { get; set; } = "";
        public Decimal NBOOKING_FEE { get; set; }
        public string? CTC_CODE { get; set; } = "";
        public bool LWITH_FO { get; set; }
        public int IREVISE_SEQ_NO { get; set; }
        //DB Parameter
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; } = "";
        public string? CREF_DATE { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string CDOC_NO { get; set; } = "";
        public string? CDOC_DATE { get; set; } = "";
        public string? CSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public string? CDAYS { get; set; }
        public string? CMONTH { get; set; }
        public string? CYEAR { get; set; }
        public string? CSALESMAN_ID { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CUNIT_DESCRIPTION { get; set; } = "";
        public string? CNOTES { get; set; } = "";
        public string? CCURRENCY_CODE { get; set; } = "";
        public string? CLEASE_MODE { get; set; } = "";
        public string? CCHARGE_MODE { get; set; } = "";
        public string? CACTION { get; set; }
        public string? CUSER_ID { get; set; }
        public List<PMT01100LOO_Offer_SelectedDataUnitListDTO>? ODATA_UNIT_LIST { get; set; }
    }
}
