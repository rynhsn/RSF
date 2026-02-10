using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FAT00300Common.Requests
{
    public class FAT00300GetTransCodeInfoParamDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CTRANS_CODE { get; set; } = string.Empty; 
    }
}
