using PMR00100Common.DTOs;
using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR00100Service.DTOs
{
    public class PMR00100PrintLogKeyDTO
    {
        public PrintParamDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}
