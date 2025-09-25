

using System;

namespace PMT01100Common.DTO._2._LOO._1._LOO___Offer_List
{
    public class PMT01100LOO_OfferList_OfferListDTO
    {
        //For Parameter : Updated 3 June 2024
        public string? CBUILDING_ID { get; set; }
        //For For Get Text Information Building
        public string? CBUILDING_NAME { get; set; }
        public bool LSELECTED_UNIT { get; set; }
        //For ParameterUnitList
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string? CFOLLOW_UP_DATE { get; set; }
        public DateTime? DFOLLOW_UP_DATE { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public string? CORIGINAL_REF_NO { get; set; }
        public string? CTRANS_STATUS_DESCR { get; set; }
        public string? CDOC_TYPE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
    }
}
