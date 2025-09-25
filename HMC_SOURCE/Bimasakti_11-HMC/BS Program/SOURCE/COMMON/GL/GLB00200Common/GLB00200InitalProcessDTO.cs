using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLB00200Common
{
    public class GLB00200InitalProcessDTO : R_APIResultBaseDTO
    {
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
        public bool LINCREMENT_FLAG { get; set; }
    }
}
