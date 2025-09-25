using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO._1._Other_Unit_List
{
    public class PMT01700OtherUnitList_OtherUnitListDTO
    {
        public bool LSELECTED_UNIT { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }

        public string? CFLOOR_ID { get; set; }
        public string? COTHER_UNIT_ID { get; set; }
        public string? COTHER_UNIT_TYPE_ID { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public string? CLOCATION { get; set; }
        public string? CLEASE_STATUS { get; set; }
        public string? CLEASE_STATUS_NAME { get; set; }
        public string? CTENANT_NAME { get; set; }
        public int ITOTAL_LOO { get; set; }
        public string? CAGREEMENT_NO { get; set; }
        public string? CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }

        public string? CBUILDING_ID_AND_NAME
        {
            get => _CBUILDING_ID_AND_NAME;
            set
            {
                if (string.IsNullOrEmpty(CBUILDING_NAME) || string.IsNullOrEmpty(CBUILDING_ID))
                {
                    _CBUILDING_ID_AND_NAME = "";
                }
                else
                {
                    _CBUILDING_ID_AND_NAME = CBUILDING_NAME + " (" + CBUILDING_ID + ")";
                }
            }
        }
        private string? _CBUILDING_ID_AND_NAME;
    }

}
