using System;
using System.Collections.Generic;
using System.Text;

namespace FAT01100Common.DTOs
{
    public class FAT01100GetAssetParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
    }
}
