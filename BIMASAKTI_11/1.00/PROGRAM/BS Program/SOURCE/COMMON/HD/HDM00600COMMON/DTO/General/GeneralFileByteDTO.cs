using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDM00600COMMON.DTO.General
{
    public class GeneralFileByteDTO : R_APIResultBaseDTO
    {
        public byte[] FileBytes { get; set; }
    }
}
