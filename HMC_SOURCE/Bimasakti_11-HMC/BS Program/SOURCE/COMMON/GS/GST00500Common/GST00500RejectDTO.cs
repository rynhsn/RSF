using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GST00500Common
{
    public class GST00500RejectDTO
    {
        //List from db
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }
    public class GST00500RejectListDTO : R_APIResultBaseDTO
    {
        public List<GST00500RejectDTO> Data { get; set; }
    }
}
