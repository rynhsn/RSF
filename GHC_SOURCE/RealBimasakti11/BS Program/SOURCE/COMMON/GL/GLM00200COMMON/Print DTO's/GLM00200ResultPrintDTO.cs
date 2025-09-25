using System.Collections.Generic;

namespace GLM00200COMMON
{
    public class GLM00200ResultPrintDTO
    {
        public JournalLabelPrint LabelPrint { get; set; }
        public JournalDTO HeaderData { get; set; }
        public List<JournalDetailActualGridDTO> DetailData { get; set; }
    }

    public class GLM00200ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GLM00200ResultPrintDTO Data { get; set; }
    }
}
