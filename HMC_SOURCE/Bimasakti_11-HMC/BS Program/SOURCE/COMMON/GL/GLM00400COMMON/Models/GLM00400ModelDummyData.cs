using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GLM00400COMMON.Models
{
    public static class GenerateDataModelGLM00400
    {
        public static GLM00400ResultPrintDTO DefaultData()
        {
            GLM00400ResultPrintDTO loData = new GLM00400ResultPrintDTO();

            List<GLM00400PrintHDDTO> loHDData = new List<GLM00400PrintHDDTO>();
            List<GLM00400PrintAccountDTO> loACCOUNTData = new List<GLM00400PrintAccountDTO>();
            List<GLM00400PrintCenterDTO> loCENTERData = new List<GLM00400PrintCenterDTO>();


            int lnDetail;
            int lnDetaiCe;
            for (int i = 1; i <= 6; i++)
            {
                lnDetail = (i % 3) + 1;
                for (int s = 1; s <= lnDetail; s++)
                {
                    lnDetaiCe = (s % 2) + 1;
                    for (int k = 1; k <= lnDetaiCe; k++)
                    {
                        loHDData.Add(new GLM00400PrintHDDTO
                        {
                            CCOMPANY_ID = "Company" + k,
                            CDEPT_CODE = "Dept" + k,
                            CFROM_ALLOC_NO = "FromAlloc" + k,
                            CTO_ALLOC_NO = "ToAlloc" + k,
                            CLANGUAGE_ID = "Lang" + k,
                            CYEAR = "Year" + k,
                            CALLOC_NO = "AllocNo" + k,
                            CALLOC_NAME = "AllocName" + k,
                            CDEPARTMENT = "DeptName" + k,
                            CSOURCE_CENTER_CODE = "SourceCenterCode" + k,
                            CSOURCE_CENTER = "SourceCenter" + k,
                            CALLOC_ID = "AllocId" + k
                        });

                        loACCOUNTData.Add(new GLM00400PrintAccountDTO
                        {
                            CCOMPANY_ID = "Company" + s,
                            CALLOC_ID = "AllocId" + s,
                            CLANGUAGE_ID = "Lang" + s,
                            CALLOC_NO = "AllocNo" + s,
                            CGLACCOUNT_NO = "GLAccountNo" + s,
                            CACCOUNT = "Account" + s,
                            CDBCR = "DBCR" + s,
                            CBSIS = "BSIS" + s,
                            CBSIS_NAME = "BSISName" + s
                        });

                        loCENTERData.Add(new GLM00400PrintCenterDTO
                        {
                            CYEAR = "Year" + i,
                            CALLOC_ID = "AllocId" + i,
                            CLANGUAGE_ID = "Lang" + i,
                            CPERIOD = "Period" + s,
                            CALLOC_PERIOD = "AllocPeriod" + s,
                            CPERIOD_NO = "PeriodNo" + s,
                            CCENTER_CODE = "CenterCode" + s,
                            CCENTER_NAME = "CenterName" + s,
                            CTARGET_CENTER = "TargetCenter" + i,
                            NVALUE = i * 1000.0m
                        });
                    }
                    
                }
            }

            // Group by CALLOC_ in loHDData in loACCOUNTData and loCENTERData  and group by loCENTERData.CPERIOD, loCENTERData.CALLOC_PERIOD
            var groupedData = loHDData
                 .GroupJoin(
                     loACCOUNTData,
                     hd => hd.CALLOC_ID,
                     account => account.CALLOC_ID,
                     (hd, accounts) => new GLM00400PrintHDResultDTO
                     {
                         CALLOC_NO = hd.CALLOC_NO,
                         CALLOC_NAME = hd.CALLOC_NAME,
                         CYEAR = hd.CYEAR,
                         CALLOC_ID = hd.CALLOC_ID,
                         AllocationAccount = accounts.ToList(),
                         AllocationCenter = loCENTERData
                             .Where(center => center.CALLOC_ID == hd.CALLOC_ID)
                             .GroupBy(center => new { center.CPERIOD, center.CALLOC_PERIOD }, detail => new GLM00400PrintCenterDetailDTO
                             {
                                 CPERIOD = detail.CPERIOD,
                                 CTARGET_CENTER = detail.CTARGET_CENTER,
                                 NVALUE = detail.NVALUE
                             }) // Group by CPERIOD
                             .Select(group => new GLM00400PrintCenterDTO
                             {
                                 CPERIOD = group.Key.CPERIOD,
                                 CALLOC_PERIOD = group.Key.CALLOC_PERIOD,
                                 CenterDetail = group.ToList()
                             })
                             .ToList()
                     })
                 .ToList();

            loData.Header = loHDData.FirstOrDefault();
            loData.HeaderData = groupedData;

            return loData;
        }

        public static GLM00400ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "GL ACCOUNT LEDGER",
                CUSER_ID = "FMC",
            };

            GLM00400ResultWithBaseHeaderPrintDTO loRtn = new GLM00400ResultWithBaseHeaderPrintDTO();

            loRtn.BaseHeaderData = loParam;
            loRtn.Allocation = DefaultData();

            return loRtn;
        }
    }

}
