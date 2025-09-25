using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04500Common
{
    public class GSM04500JournalGroupTypeDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }
    public class GSM04500JournalGroupTypeListDTO : R_APIResultBaseDTO
    {
        public List<GSM04500JournalGroupTypeDTO> Data { get; set; }
    }
}
