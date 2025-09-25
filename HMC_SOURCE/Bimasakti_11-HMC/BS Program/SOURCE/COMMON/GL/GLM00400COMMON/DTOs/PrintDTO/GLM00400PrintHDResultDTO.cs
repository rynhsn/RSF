using System;
using System.Collections.Generic;

namespace GLM00400COMMON
{
    public class GLM00400PrintHDResultDTO
    {
        public string CALLOC_NO { get; set; }
        public string CALLOC_NAME { get; set; }
        public string CYEAR { get; set; }
        public string CALLOC_ID { get; set; }

        public List<GLM00400PrintAccountDTO> AllocationAccount { get; set; }
        public List<GLM00400PrintCenterDTO> AllocationCenter { get; set; }
    }


}
