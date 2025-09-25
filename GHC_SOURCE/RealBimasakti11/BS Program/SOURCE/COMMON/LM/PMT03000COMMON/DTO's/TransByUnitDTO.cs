using System;
using System.Collections.Generic;
using System.Text;

namespace PMT03000COMMON
{
    public class TransByUnitDTO : BuildingUnitDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CSTATUS { get; set; }
    }
}
