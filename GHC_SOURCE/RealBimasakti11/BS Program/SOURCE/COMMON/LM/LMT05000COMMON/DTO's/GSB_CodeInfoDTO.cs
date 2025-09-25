using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PMT05000COMMON
{
    public class GSB_CodeInfoDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }

    public class GSB_CodeInfoParam : GSB_CodeInfoDTO
    {
        public string CLASS_APPLICATION { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CLASS_ID { get; set; }
        public string REC_ID_LIST { get; set; }
        public string LANG_ID { get; set; }
    }
}
