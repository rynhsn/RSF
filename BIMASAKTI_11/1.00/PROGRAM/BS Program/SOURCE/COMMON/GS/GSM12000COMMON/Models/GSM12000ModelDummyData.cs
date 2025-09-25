using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GSM12000COMMON.Models
{
    public static class GenerateDataModelGSM12000
    {
        public static GSM12000ResultPrintDTO DefaultData()
        {
            GSM12000ResultPrintDTO loData = new GSM12000ResultPrintDTO();

            List<GSM12000ResultPrintSPDTO> loTempResult = new List<GSM12000ResultPrintSPDTO>();
            int lnDetail;
            for (int i = 1; i <= 2; i++)
            {
                loTempResult.Add(new GSM12000ResultPrintSPDTO
                {
                    CCOMPANY_ID = $"COMP{i:D2}",
                    CMESSAGE_TYPE = $"TYPE{i % 3}",
                    CMESSAGE_TYPE_DESCR = $"Description for TYPE{i % 3}",
                    CMESSAGE_NO = $"MSG{i:000}",
                    TMESSAGE_DESCRIPTION = $"This is message number {i}",
                    CADDITIONAL_DESCRIPTION = $"Additional info {i}",
                    LACTIVE = i % 2 == 0,
                    CACTIVE_BY = i % 2 == 0 ? $"User{i}" : null,
                    DACTIVE_DATE = i % 2 == 0 ? DateTime.Now.AddDays(-i) : (DateTime?)null,
                    CINACTIVE_BY = i % 2 != 0 ? $"User{i}" : null,
                    DINACTIVE_DATE = i % 2 != 0 ? DateTime.Now.AddDays(-i) : (DateTime?)null,
                    CCREATE_BY = $"Creator{i}",
                    DCREATE_DATE = DateTime.Now.AddDays(-i * 2),
                    CUPDATE_BY = $"Updater{i}",
                    DUPDATE_DATE = DateTime.Now.AddDays(-i)
                });
            }

            //set header data
            loData.Data = loTempResult;

            return loData;
        }

        public static GSM12000ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "GL ACCOUNT LEDGER",
                CUSER_ID = "FMC",
            };

            GSM12000ResultWithBaseHeaderPrintDTO loRtn = new GSM12000ResultWithBaseHeaderPrintDTO { Column = new GSM12000ColumnDTO() };

            loRtn.BaseHeaderData = loParam;
            loRtn.MessageMaster = DefaultData();

            return loRtn;
        }
    }

}
