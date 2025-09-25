using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00170COMMON
{
    public  class PMR00170InitialProcess : R_APIResultBaseDTO
    {
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
        public int IMONTHS { get; set; }
        public int IYEAR { get; set; }

    }
}
