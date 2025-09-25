using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00200COMMON.Print_DTO.Detail.SubDetail
{
    public class DocumentDTO
    {
        public string? CDOCUMENT_DETAIL_NO { get; set; }
        public string? CDOCUMENT_DETAIL_DATE { get; set; }
        public DateTime? DDOCUMENT_DETAIL_DATE { get; set; }
        public string? CDOCUMENT_DETAIL_EXPIRED_DATE { get; set; }
        public DateTime? DDOCUMENT_DETAIL_EXPIRED_DATE { get; set; }
        public string? CDOCUMENT_DETAIL_FILE { get; set; }
        public string? CDOCUMENT_DETAIL_DESCRIPTION { get; set; }
    }
}
