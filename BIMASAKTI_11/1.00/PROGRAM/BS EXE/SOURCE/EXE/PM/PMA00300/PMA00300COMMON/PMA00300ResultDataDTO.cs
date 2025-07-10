using PMA00300COMMON.VA_DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA00300COMMON
{
    public class PMA00300ResultDataDTO
    {
        public PMA00300LabelDTO Label { get; set; }
        public PMA00300BaseHeaderDTO? Header { get; set; }
        public PMA00300DataReportDTO Data { get; set; }
        public List<PMA00300VADTO> VirtualAccountData { get; set; }
        public List<PMA00300UtilityDetail> DataUnitList { get; set; }
        public List<PMA00300UtilityChargesDetail> DataUtility1 { get; set; }
        public List<PMA00300UtilityChargesDetail> DataUtility2 { get; set; }
        public List<PMA00300UtilityChargesDetail> DataUtility3 { get; set; }
        public List<PMA00300UtilityChargesDetail> DataUtility4 { get; set; }
    }
}
