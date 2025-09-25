using System;
using System.Collections.Generic;
using System.Text;
using BaseHeaderReportCOMMON;

namespace BaseHeaderReportCOMMON.Models
{
    public class GenerateDataModelHeader
    {
        public static BaseHeaderResult DefaultData()
        {
            BaseHeaderResult loRtn = new BaseHeaderResult();
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "00-00-1",
                CPRINT_NAME = "Account Trial Balance",
                CUSER_ID = "HMC",
            };
            loRtn.BaseHeaderData = loParam;


            return loRtn;
        }
    }
}
