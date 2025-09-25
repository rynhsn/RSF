using System;

namespace PMT01500Common.DTO._7._Document
{
    public class PMT01500DocumentListDTO
    {
        public string? CDOC_NO { get; set; }
        public string? CDOC_DATE { get; set; }
        public string? CEXPIRED_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        //Ver 25 Apr 2024 : Penambahan Handle DateTime
        public DateTime? DDOC_DATE { get; set; }
        public DateTime? DEXPIRED_DATE { get; set; }
    }
}
