using System.Collections.Generic;

namespace GLM00400COMMON
{
    public class GLM00400ResultPrintDTO
    {
        public string Title { get; set; }
        public GLM00400PrintHDDTO Header { get; set; }
        public List<GLM00400PrintHDResultDTO> HeaderData { get; set; }
    }

    public class GLM00400ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GLM00400ResultPrintDTO Allocation { get; set; }
    }
}
