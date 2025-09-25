using BaseHeaderReportCOMMON;
using PMR00220COMMON.Print_DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PMR00220COMMON.Model
{
    public static class PMR00200DummyData
    {
        public static PMR00220PrintDislpayDTO PMR00200PrintDislpayWithBaseHeader()
        {
            PMR00220PrintDislpayDTO loRtn = new PMR00220PrintDislpayDTO();
            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "PMR00200",
                CPRINT_NAME = "LOI Status",
                CUSER_ID = "GHC",
            };
            loRtn.SummaryData = new PMR00220ReportDataDTO();
            loRtn.SummaryData.Header = "PM";
            loRtn.SummaryData.Title = "LOI Status";
            loRtn.SummaryData.Column = new PMR00220LabelDTO();
            loRtn.SummaryData.HeaderParam = new PMR00220ParamDTO()
            {
                CCOMPANY_ID = "RCD",
                CPROPERTY_ID = "Property id",
                CPROPERTY_NAME = "Property Name",
                CFROM_DEPARTMENT_ID = "D1",
                CFROM_DEPARTMENT_NAME = "Dept From",
                CTO_DEPARTMENT_ID = "D2",
                CTO_DEPARTMENT_NAME = "Dept To",
                CFROM_SALESMAN_ID = "S1",
                CFROM_SALESMAN_NAME = "SALESMAN From",
                CTO_SALESMAN_ID = "S1",
                CTO_SALESMAN_NAME = "SALESMAN To",
                CFROM_PERIOD = "202401",
                CTO_PERIOD = "202402",
                LIS_OUTSTANDING = true,
                CREPORT_TYPE_DISPLAY = "Summary"
            };
            DateTime loFromDate = DateTime.ParseExact(loRtn.SummaryData.HeaderParam.CFROM_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);
            DateTime loToDate = DateTime.ParseExact(loRtn.SummaryData.HeaderParam.CTO_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);
            loRtn.SummaryData.HeaderParam.CPERIOD_DISPLAY = (loFromDate.Year != loToDate.Year || loFromDate.Month != loToDate.Month)
                ? $"{loFromDate:MMMM yyyy} – {loToDate:MMMM yyyy}"
                : $"{loFromDate:MMMM yyyy}";

            loRtn.SummaryData.Data = new List<PMR00220DataDTO>();
            int countData1 = 2, countData2 = 2, countData3 = 3;
            List<PMR00220DataDTO> loCollData = new List<PMR00220DataDTO>();

            string companyId = "C1";
            string propertyId = "P1";
            string deptCode = "D1";
            string deptName = "Department 1";

            string[] transCodes = { "T1", "T2" };
            string[] transNames = { "Trans 1", "Trans 2" };

            string[,] salesmen = { { "SM1", "Muhammad Andika Putra 1" }, { "SM2", "Muhammad Andika Putra 2" } };

            for (int i = 0; i < transCodes.Length; i++)
            {
                string transCode = transCodes[i];
                string transName = transNames[i];

                for (int j = 0; j < salesmen.GetLength(0); j++)
                {
                    string salesmanId = salesmen[j, 0];
                    string salesmanName = salesmen[j, 1];

                    for (int k = 1; k <= 2; k++)
                    {
                        PMR00220DataDTO dto = new PMR00220DataDTO
                        {
                            CCOMPANY_ID = companyId,
                            CPROPERTY_ID = propertyId,
                            CDEPT_CODE = deptCode,
                            CDEPT_NAME = deptName,
                            CTRANS_CODE = transCode,
                            CTRANS_NAME = transName,
                            CREF_NO = $"R{j + 1}{k}",
                            CREF_DATE = DateTime.Now.ToString("yyyy-MM-dd"),
                            CTENANT_ID = $"Tenant{j + 1}{k}",
                            CTENANT_NAME = $"Tenant Name {j + 1}{k}",
                            CTENURE = $"{i + 1} Year(s), {j + 1} Month(s), {k + 1} Day(s)",
                            CSALESMAN_ID = salesmanId,
                            CSALESMAN_NAME = salesmanName,
                            NREVISION_COUNT = 1,
                            CTC_CODE = "TC1",
                            CTC_MESSAGE = "This is dummy data for Term & Con.",
                            CAGREEMENT_STATUS_ID = "A1",
                            CAGREEMENT_STATUS_NAME = "Agreement Status",
                            CTRANS_STATUS_ID = "TS1",
                            CTRANS_STATUS_NAME = "In Progress",
                            CTAX = "PPN 11%",
                            NTOTAL_GROSS_AREA_SIZE = 100,
                            NTOTAL_NET_AREA_SIZE = 80,
                            NTOTAL_COMMON_AREA_SIZE = 20,
                            NTOTAL_PRICE = 1000
                        };

                        loRtn.SummaryData.Data.Add(dto);
                    }
                }
            }


            //for (int a = 1; a <= countData1; a++)
            //{
            //    for (int b = 1; b <= countData2; b++)
            //    {
            //        for (int c = 1; c <= countData3; c++)
            //        {
            //            loRtn.SummaryData.Data.Add(new PMR00200DataDTO()
            //            {
            //                CTRANS_NAME = $"Trans {a}",
            //                CSALESMAN_ID = $"SM{b}",
            //                CSALESMAN_NAME = $"Muhammad Andika Putra{b}",
            //                CREF_NO = $"Ref00{c}",
            //                CREF_DATE = $"{c} Jan 2024",
            //                CTENURE = $"{c} Year(s), {c} Month(s), {c} Day(s)",
            //                NTOTAL_GROSS_AREA_SIZE = 25,
            //                NTOTAL_NET_AREA_SIZE = 22,
            //                NTOTAL_COMMON_AREA_SIZE = 20,
            //                CTRANS_STATUS_NAME = $"In Progress",
            //                NTOTAL_PRICE = 180000000,
            //                CTAX = "PPN 11%",
            //                CTENANT_ID = "MU001",
            //                CTENANT_NAME = "Muhammad Andika Putra",
            //                CTC_MESSAGE = "This is dummy data for Term & Con."
            //            });
            //        }
            //    }
            //}
            return loRtn;
        }

    }
}

