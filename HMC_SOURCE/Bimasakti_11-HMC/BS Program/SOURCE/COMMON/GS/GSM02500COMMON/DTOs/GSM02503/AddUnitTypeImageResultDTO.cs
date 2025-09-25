using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02503
{
    public class AddUnitTypeImageResultDTO 
    {
        public string CIMAGE_ID { get; set; } = "";
        public string CIMAGE_NAME { get; set; } = "";
        public string CFILE_NAME { get; set; } = "";
        public string CFILE_EXTENSION { get; set; } = "";
        public byte[] OIMAGE { get; set; } 
        public bool LRESULT { get; set; } = false;
    }
}
