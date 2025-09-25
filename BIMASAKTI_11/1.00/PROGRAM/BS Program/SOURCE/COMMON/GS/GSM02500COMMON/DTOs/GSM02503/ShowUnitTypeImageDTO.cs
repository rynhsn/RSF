using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02503
{
    public class ShowUnitTypeImageDTO
    {
        public byte[] OIMAGE { get; set; }
        public string CFILE_EXTENSION { get; set; }
        public string CFILE_NAME { get; set; }
        public string CFILE_NAME_EXTENSION { get; set; }
    }
}
