using System;
using System.Collections.Generic;
using System.Text;

namespace BaseHeaderReportCOMMON
{
    public class BaseHeaderResult
    {
        public BaseHeaderDTO BaseHeaderData { get; set; }
        public BaseHeaderColumnDTO BaseHeaderColumn { get; set; } = new BaseHeaderColumnDTO();
    }
}
