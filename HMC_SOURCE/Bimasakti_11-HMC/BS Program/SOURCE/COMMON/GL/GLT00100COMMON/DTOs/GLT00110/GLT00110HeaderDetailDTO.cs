using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GLT00100COMMON
{
    public class GLT00110HeaderDetailDTO
    {
        public GLT00110DTO HeaderData { get; set; }
        public List<GLT00111DTO> DetailData { get; set; }
    }
}
