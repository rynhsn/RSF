using System.Collections.Generic;
using R_APICommonDTO;

namespace GSM04500Common.DTOs
{
    
    public class GSM04500UploadFromFileDTO
    {
        public string JournalGroup { get; set; }
        public string JournalGroupName { get; set; }
        public bool EnableAccrual { get; set; }
        public string Notes { get; set; }
    }

    public class GSM04500UploadFromSystemDTO
    {
        public int No { get; set; }
        public string JournalGroup { get; set; }
        public string JournalGroupName { get; set; }
        public bool EnableAccrual { get; set; }//From Excel
        public string ErrorMessage { get; set; }
        public string ErrorFlag { get; set; } = "Y";
    }
    
    public class GSM04500UploadForSystemDTO
    {
        public int No { get; set; }
        public string JournalGroup { get; set; }
        public string JournalGroupName { get; set; }
        public bool EnableAccrual { get; set; }
    }
    
    public class GSM04500ListUploadErrorValidateDTO : R_APIResultBaseDTO
    {
        public List<GSM04500UploadFromSystemDTO> Data { get; set; }
    }
}