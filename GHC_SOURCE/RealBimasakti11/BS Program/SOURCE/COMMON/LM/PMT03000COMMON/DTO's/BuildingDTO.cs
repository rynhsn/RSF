using PMT03000COMMON.DTO_s.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT03000COMMON
{
    public class BuildingDTO : GeneralParamDTO
    {
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CBUILDING_DISPLAY { get; set; }
        public bool LACTIVE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}
