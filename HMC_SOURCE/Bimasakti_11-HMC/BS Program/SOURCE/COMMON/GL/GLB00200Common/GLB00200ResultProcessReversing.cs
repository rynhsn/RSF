using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLB00200Common
{
    public class GLB00200ResultProcessReversing
    {
        public bool LSUCCESSED { get; set; }
        public string CREF_NO { get; set; }
    }

    public class GLB00200ResultProcessReversingListDTO : R_APIResultBaseDTO
    {
        public List<GLB00200ResultProcessReversing> Data { get; set; }
    }
}
