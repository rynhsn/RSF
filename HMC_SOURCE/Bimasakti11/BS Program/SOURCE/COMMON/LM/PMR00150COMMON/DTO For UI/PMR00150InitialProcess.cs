using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON
{
    public  class PMR00150InitialProcess : R_APIResultBaseDTO
    {
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
        public int IMONTHS { get; set; }
        public int IYEAR { get; set; }

    }
}
