using System;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL03400DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CSIGN_ID { get; set; } 
        public string CSIGN_NAME { get; set; }
        public string CSIGN_POSITION { get; set; }
        public string CSIGN_FILE_NAME { get; set; }
        public string CSIGN_FILE_EXTENSION { get; set; }

        public string CSTORAGE_ID { get; set; }
        public byte[] OSIGN_FILE { get; set; }

        public string CNOTES { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
