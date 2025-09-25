using System.Collections.Generic;

namespace CBR00600COMMON
{
    public class CBR00600PrintDataDTO
    {
        public List<CBR00600OfficialReceiptHeaderDTO> OfficialReceipt { get; set; }
        public List<CBR00600AllocationHeaderDTO> Allocation { get; set; }
        public List<CBR00600JournalHeaderDTO> Journal { get; set; }
    }

    public class CBR00600ResultPrintDTO
    {
        public CBR00600PrintDataDTO Data { get; set; }
        public CBR00600ColoumnDTO Column { get; set; }
        public CBR00600BaseHeaderDTO BaseHeaderData { get; set; }
        public CBR00600DTO Parameter { get; set; }
    }
}
