using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO._2._LOO._1._LOO___Offer_List
{
    public class PMT01700LOO_OfferList_OfferListDTO : R_APIResultBaseDTO
    {
        public bool LSELECTED_UNIT { get; set; }

        public string? CPROPERTY_ID { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        //For ParameterUnitList
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }

        public string? CDEPT_NAME { get; set; }
        public string? CUNIT_DESCRIPTION { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string? CFOLLOW_UP_DATE { get; set; }
        public DateTime? DFOLLOW_UP_DATE { get; set; }
        public string? CEXPIRED_DATE { get; set; }
        public DateTime? DEXPIRED_DATE { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public string? CORIGINAL_REF_NO { get; set; }
        public string? CTRANS_STATUS_DESCR { get; set; }
        public string? CDOC_TYPE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public bool IS_PROCESS_SUCCESS { get; set; }
        //06/07/2024
        public string? CTRANS_STATUS { get; set; }
    }
}
