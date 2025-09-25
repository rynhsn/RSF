using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04500Common
{
    public class GSM04500UploadFromExcelDTO
    {
        public string JournalGroup { get; set; }
        public string JournalGroupName { get; set; }
        public bool EnableAccrual { get; set; }
        public string Notes { get; set; }

    }


    public class GSM04500UploadErrorValidateDTO
    {
        public int No { get; set; }
        public string JournalGroup { get; set; }
        public string JournalGroupName { get; set; }
        public bool EnableAccrual { get; set; }//From Excel
        public string ErrorMessage { get; set; }
        public string ErrorFlag { get; set; } = "Y";
    }
    public class GSM04500FieldTemporaryTableDTO
    {
        public int No { get; set; }
        public string JournalGroup { get; set; }
        public string JournalGroupName { get; set; }
        public bool EnableAccrual { get; set; }

    }

    public class GSM04500ListUploadErrorValidateDTO : R_APIResultBaseDTO
    {
        public List<GSM04500UploadErrorValidateDTO> Data { get; set; }
    }
}
