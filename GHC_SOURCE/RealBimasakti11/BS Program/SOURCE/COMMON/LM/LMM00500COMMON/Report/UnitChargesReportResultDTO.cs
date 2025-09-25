using System;
using System.Collections.Generic;
using System.Text;

namespace PMM00500Common.Report
{
    public class UnitChargesReportResultDTO
    {
        public string title { get; set; }
        public UnitChargesHeaderDTO Header { get; set; }
        public UnitChargesColumnDTO Column { get; set; }
        public List<UnitChargersDetail1DTO> DataUnitCharges { get; set; }
    }

    public class PMM00500UnitChargesResultWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public UnitChargesReportResultDTO PMM00500PrintData { get; set; }
    }
}
