using System.Collections.Generic;
using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GLI00100Common.DTOs;
using GLI00100Common.DTOs.Print;

namespace GLI00100Common.Model
{
    public static class GLI00100ModelPrintDummyData
    {
        public static GLI00100PrintResultDTO DefaultData()
        {
            var loData = new GLI00100PrintResultDTO()
            {
                Title = "Account Status",
                HeaderTitle = new GLI00100PrintHeaderTitleDTO(),
                // Header = new GLI00100PrintHeaderDTO
                // {
                //     CGLACCOUNT_NO = "15.10.0001",
                //     CGLACCOUNT_NAME = "Kas",
                //     CBSIS_LONG_NAME = "Balance Sheet",
                //     CDBCR_LONG_NAME = "Debit",
                //     CYEAR = "2023",
                //     CCENTER = "HRD-Human Resources Department",
                //     CBUDGET = "BW2023 - Budget 2023",
                //     CCURRENCY = "IDR (Local Currency)"
                // },
                Column = new GLI00100PrintColumnDTO(),
                Row = new GLI00100PrintRowDTO(),
                Data = new GLI00100AccountAnalysisDTO
                {
                    CGLACCOUNT_NO = "15.10.0001",
                    CGLACCOUNT_NAME = "Kas",
                    CBSIS_LONG_NAME = "Balance Sheet",
                    CDBCR_LONG_NAME = "Debit",
                    CYEAR = "2023",
                    CCENTER = "HRD-Human Resources Department",
                    CBUDGET = "BW2023 - Budget 2023",
                    CCURRENCY = "IDR (Local Currency)",
                    NCURRENT_OPENING = 120000000.00m,
                    NLAST_OPENING = 90000000.00m,
                    NMONTH1 = 9000000.00m,
                    NMONTH2 = 9090000.00m,
                    NMONTH3 = 9180900.00m,
                    NMONTH4 = 9272709.00m,
                    NMONTH5 = 9365436.09m,
                    NMONTH6 = 9459090.45m,
                    NMONTH7 = 9553681.36m,
                    NMONTH8 = 9649218.17m,
                    NMONTH9 = 9745710.35m,
                    NMONTH10 = 9843167.45m,
                    NMONTH11 = 9941599.13m,
                    NMONTH12 = 10041015.12m,
                    NBUDGET1 = 10000000.00m,
                    NBUDGET2 = 10000000.00m,
                    NBUDGET3 = 10000000.00m,
                    NBUDGET4 = 10000000.00m,
                    NBUDGET5 = 10000000.00m,
                    NBUDGET6 = 10000000.00m,
                    NBUDGET7 = 10000000.00m,
                    NBUDGET8 = 10000000.00m,
                    NBUDGET9 = 10000000.00m,
                    NBUDGET10 = 10000000.00m,
                    NBUDGET11 = 10000000.00m,
                    NBUDGET12 = 10000000.00m,
                    NCURRENT1 = 129000000.00m,
                    NCURRENT2 = 129000000.00m,
                    NCURRENT3 = 129000000.00m,
                    NCURRENT4 = 129000000.00m,
                    NCURRENT5 = 129000000.00m,
                    NCURRENT6 = 129000000.00m,
                    NCURRENT7 = 129000000.00m,
                    NCURRENT8 = 129000000.00m,
                    NCURRENT9 = 129000000.00m,
                    NCURRENT10 = 129000000.00m,
                    NCURRENT11 = 129000000.00m,
                    NCURRENT12 = 129000000.00m,
                    NLAST1 = 99000000.00m,
                    NLAST2 = 99000000.00m,
                    NLAST3 = 99000000.00m,
                    NLAST4 = 99000000.00m,
                    NLAST5 = 99000000.00m,
                    NLAST6 = 99000000.00m,
                    NLAST7 = 99000000.00m,
                    NLAST8 = 99000000.00m,
                    NLAST9 = 99000000.00m,
                    NLAST10 = 99000000.00m,
                    NLAST11 = 99000000.00m,
                    NLAST12 = 99000000.00m
                }
            };

            return loData;
        }

        public static GLI00100PrintWithBaseHeaderDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO
            {
                CCOMPANY_NAME = "PT. Realta Chakradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Account Status",
                CUSER_ID = "rhc"
            };
            
            GLI00100PrintWithBaseHeaderDTO loData = new GLI00100PrintWithBaseHeaderDTO
            {
                BaseHeaderData = loParam,
                Data = DefaultData()
            };

            return loData;
        }
    }
    
    
}