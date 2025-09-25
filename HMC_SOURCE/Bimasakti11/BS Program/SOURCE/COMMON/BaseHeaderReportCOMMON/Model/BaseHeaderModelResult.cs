using System;
using System.IO;
using System.Reflection;

namespace BaseHeaderReportCOMMON.Models
{
    public static class GenerateDataModelHeader
    {
        public static BaseHeaderResult DefaultData()
        {
            BaseHeaderResult loRtn = new BaseHeaderResult();
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "GL ACCOUNT LEDGER",
                CUSER_ID = "FMC",
            };
            loRtn.BaseHeaderData = loParam;

            return loRtn;
        }
    }
}
