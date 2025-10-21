using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM10000COMMON.Upload
{
    public class PMM10000TemplateDTO : R_APIResultBaseDTO    
    {
        public byte[] FileBytes { get; set; }
    }
}
