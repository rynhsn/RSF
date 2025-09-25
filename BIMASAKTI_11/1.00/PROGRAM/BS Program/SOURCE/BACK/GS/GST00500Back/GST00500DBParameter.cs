using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GST00500Back
{
    public class GST00500DBParameter
    {
        public string CCOMPANYID { get; set; }
        public string CUSER_ID { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public string CTRANS_TYPE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CREF_NO { get; set; }


        public bool APPROVE_TRANSACTION { get; set; }
        public string CREASON_CODE { get; set; }
        public string TNOTES { get; set; }
    }
}
