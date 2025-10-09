using Lookup_PMCOMMON.DTOs.UtilityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML02000
{
    public class LML02000ParameterDTO : BaseDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CPARENT_ID { get; set; } = "";
        public bool LCHILD_ONLY { get; set; } = false ;
        public string CLANGUAGE_ID { get; set; } = "ENU";
    }
}
