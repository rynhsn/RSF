using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GLR00300Common.GLR00300Print;

namespace GLR00300Common.Model
{
    public static class GenerateDataModel
    {
        #region Foramt A - D

        public static GLR00300AccountTrialBalanceResultFormat_AtoD_DTO DefaultData_Format_AtoD()
        {
            GLR00300AccountTrialBalanceResultFormat_AtoD_DTO loReturn =
                new GLR00300AccountTrialBalanceResultFormat_AtoD_DTO()
                {
                    Title = "Account Trial Balance",
                    Header = new GLR00300HeaderAccountTrialBalanceDTO()
                    {
                        CPERIOD = "Period",
                        CFROM_ACCOUNT_NO = "123.000.000",
                        CTO_ACCOUNT_NO = "456.000.000",
                        CFROM_CENTER_CODE = "123.000.000",
                        CTO_CENTER_CODE = "456.000.000",
                        CTB_TYPE_NAME = "Normal",
                        CCURRENCY = "IDR",
                        CJOURNAL_ADJ_MODE_NAME = "SPLIT",
                        CPRINT_METHOD_NAME = "SUPRESS NO TRANSACTION",
                        CBUDGET_NO = " - "
                    },
                    Column = new AccountTrialBalanceColumnDTO(),
                    Label = new GLR00300LabelDTO(),
                    Data = new List<GLR00300DataAccountTrialBalanceAD>()
                };

            List<GLR00300DataAccountTrialBalanceAD> loCollectionData = new List<GLR00300DataAccountTrialBalanceAD>();

            //GENERATE DATA DUMMY
            for (int j = 1; j < 15; j++)
            {
                loCollectionData.Add(new GLR00300DataAccountTrialBalanceAD()
                {
                    CGLACCOUNT_NO = $"15.000.1.00{j}",
                    CGLACCOUNT_NAME = $"ELECTRICAL NEW SYSTEM {j}",
                    CDBCR = "D",
                    CBSIS = "BS",
                    CCENTER = $"712 -  Condo CGR {j}",
                    NBEGIN_BALANCE = 900000m * j * 17000m,
                    NCREDIT = 400000m * j * 1100m,
                    NDEBIT = 500000m * j * 9700m,
                    NDEBIT_ADJ = 600000m * j * 3700m,
                    NCREDIT_ADJ = 700000m * j * 4700m,
                    NEND_BALANCE = 800000m * j * 5700m,
                    NBUDGET = 80000.8m * 1000 * j
                }
                );
            }
            loReturn.Data = loCollectionData;
            return loReturn;
        }

        public static GLR00300AccountTrialBalanceResult_FormatAtoD_WithBaseHeaderDTO DefaultDataWithHeaderFormat_AtoD()
        {
            GLR00300AccountTrialBalanceResult_FormatAtoD_WithBaseHeaderDTO loRtn = new GLR00300AccountTrialBalanceResult_FormatAtoD_WithBaseHeaderDTO();
            loRtn.BaseHeaderData = GenerateDataModelHeader.DefaultData().BaseHeaderData;
            loRtn.GLR00300AccountTrialBalanceResult_FormatAtoD_DataFormat = DefaultData_Format_AtoD();

            return loRtn;
        }
        #endregion

        #region Format E - H
        public static GLR00300AccountTrialBalanceResultFormat_EtoH_DTO DefaultData_Format_EtoH()
        {
            GLR00300AccountTrialBalanceResultFormat_EtoH_DTO loReturn =
                new GLR00300AccountTrialBalanceResultFormat_EtoH_DTO()
                {
                    Title = "Account Trial Balance",
                    Header = new GLR00300HeaderAccountTrialBalanceDTO()
                    {
                        CPERIOD = "Period",
                        CFROM_ACCOUNT_NO = "123.000.000",
                        CTO_ACCOUNT_NO = "456.000.000",
                        CFROM_CENTER_CODE = "123.000.000",
                        CTO_CENTER_CODE = "456.000.000",
                        CTB_TYPE_NAME = "Normal",
                        CCURRENCY = "IDR",
                        CJOURNAL_ADJ_MODE_NAME = "SPLIT",
                        CPRINT_METHOD_NAME = "SUPRESS NO TRANSACTION",
                        CBUDGET_NO = " - "
                    },
                    Column = new AccountTrialBalanceColumnDTO(),
                    Label = new GLR00300LabelDTO(),
                };

            List<GLR00300_DataDetail_AccountTrialBalance> loCollectionData = new List<GLR00300_DataDetail_AccountTrialBalance>();

            //GENERATE DATA DUMMY
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 6; j++)
                {
                    loCollectionData.Add(new GLR00300_DataDetail_AccountTrialBalance()
                    {
                        CGLACCOUNT_NO = $"15.000.1.00{i}",
                        CGLACCOUNT_NAME = $"ELECTRICAL NEW SYSTEM {i}",
                        CDBCR = "D",
                        CBSIS = "BS",
                        CCENTER = $"712 -  Condo CGR {j}",
                        NBEGIN_BALANCE = 900000m * i * 17000m,
                        NCREDIT = 400000m * i * 1100m,
                        NDEBIT = 500000m * i * 9700m,
                        NDEBIT_ADJ = 600000m * i * 3700m,
                        NCREDIT_ADJ = 700000m * i * 4700m,
                        NEND_BALANCE = 800000m * i * 5700m,
                        NBUDGET = 80000.8m * 1000 * i
                    }
                    );
                }
            }
            //Grouping Data From DB to Ready to Display on Report
            var loTempData = loCollectionData
                .GroupBy(data => new
                {
                    data.CGLACCOUNT_NO,
                    data.CGLACCOUNT_NAME,
                    data.CDBCR,
                    data.CBSIS,
                }).Select(dataDetail => new GLRR00300DataAccountTrialBalance
                {
                    CGLACCOUNT_NO = dataDetail.Key.CGLACCOUNT_NO,
                    CGLACCOUNT_NAME = dataDetail.Key.CGLACCOUNT_NAME,
                    CDBCR = dataDetail.Key.CDBCR,
                    CBSIS = dataDetail.Key.CBSIS,
                    DataDetail = dataDetail
                    .Select(itemDetail => new GLRR00300DataDetailAccountTrialBalance
                    {
                        CCENTER = itemDetail.CCENTER,
                        NBEGIN_BALANCE = itemDetail.NBEGIN_BALANCE,
                        NCREDIT = itemDetail.NCREDIT,
                        NDEBIT = itemDetail.NDEBIT,
                        NDEBIT_ADJ = itemDetail.NDEBIT_ADJ,
                        NCREDIT_ADJ = itemDetail.NCREDIT_ADJ,
                        NEND_BALANCE = itemDetail.NEND_BALANCE,
                        NBUDGET = itemDetail.NBUDGET,
                    }).ToList()
                }).ToList();

            loReturn.Data = loTempData;


            return loReturn;
        }

        public static GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO DefaultDataWithHeaderFormat_EtoH()
        {
            GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO loRtn = new GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO();
            loRtn.BaseHeaderData = GenerateDataModelHeader.DefaultData().BaseHeaderData;
            loRtn.GLR00300AccountTrialBalanceResult_FormatEtoH_DataFormat = DefaultData_Format_EtoH();

            return loRtn;
        }

        #endregion

    }
}
