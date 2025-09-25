using System;
using System.Collections.Generic;
using System.Text;

namespace PMB04000COMMONPrintBatch.ParamDTO
{
    public class PMB04000ParamReportDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CLANG_ID { get; set; }
        public bool LPRINT_ONE_TIME { get; set; }
        public string CDEPT_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public bool LPRINT { get; set; }
        public string? LIST_REFNO { get; set; }
        public bool LDISTRIBUTE { get; set; }
        public string? CSTORAGE_ID { get; set; }
        public string? CTEMPLATE_ID { get; set; }
        public bool LIS_PRINT { get; set; }
        public string CREPORT_FILENAME { get; set; } = "";
        public string CREPORT_FILETYPE { get; set; } = "";
        
        public string? CSIGN_ID01 { get; set; } = "";
        public string? CSIGN_ID02 { get; set; } = "";
        public string? CSIGN_ID03 { get; set; } = "";
        public string? CSIGN_ID04 { get; set; } = "";
        public string? CSIGN_ID05 { get; set; } = "";
        public string? CSIGN_ID06 { get; set; } = "";
        public string? CSTORAGE_ID01 { get; set; }
        public string? CSTORAGE_ID02 { get; set; }
        public string? CSTORAGE_ID03 { get; set; }
        public string? CSTORAGE_ID04 { get; set; }
        public string? CSTORAGE_ID05 { get; set; }
        public string? CSTORAGE_ID06 { get; set; }
        public string? CSIGN_NAME01 { get; set; }
        public string? CSIGN_NAME02 { get; set; }
        public string? CSIGN_NAME03 { get; set; }
        public string? CSIGN_NAME04 { get; set; }
        public string? CSIGN_NAME05 { get; set; }
        public string? CSIGN_NAME06 { get; set; }
        public string? CSIGN_POSITION01 { get; set; }
        public string? CSIGN_POSITION02 { get; set; }
        public string? CSIGN_POSITION03 { get; set; }
        public string? CSIGN_POSITION04 { get; set; }
        public string? CSIGN_POSITION05 { get; set; }
        public string? CSIGN_POSITION06 { get; set; }

    }
}
