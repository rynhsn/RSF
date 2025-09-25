using R_APICommonDTO;
using System;

namespace PMT01500Common.DTO._1._AgreementList
{
    public class PMT01500ChangeStatusDTO : R_APIResultBaseDTO
    {
        public string? CREF_NO { get; set; }
        public string? CBLANK { get; set; } = "";
        public string? CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string? CHAND_OVER_DATE {  get; set; }
        public DateTime? DHAND_OVER_DATE {  get; set; }
        public string? CSTART_DATE {  get; set; }
        public DateTime? DSTART_DATE {  get; set; }
        public string? CEND_DATE {  get; set; }
        public DateTime? DEND_DATE {  get; set; }
        public string? CAGREEMENT_STATUS_DESCR {  get; set; }
        public string? CAGREEMENT_STATUS {  get; set; }
        public string? CACCEPT_DATE {  get; set; }
        public DateTime? DACCEPT_DATE {  get; set; }
        //Revisi kah manieesss
        public string? CDOC_NO {  get; set; }
        public string? CREASON {  get; set; }
        public string? CNOTES {  get; set; }

    }
}
