using BaseHeaderReportCOMMON;
using PMR00220COMMON.Print_DTO;
using PMR00220COMMON.Print_DTO.Detail;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PMR00220COMMON.Model
{
    public class PMR00220DummyData
    {
        public static PMR00221PrintDislpayDTO PMR00220PrintDislpayWithBaseHeader()
        {
            PMR00221PrintDislpayDTO loRtn = new PMR00221PrintDislpayDTO();
            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "PMR00200",
                CPRINT_NAME = "LOI Status",
                CUSER_ID = "GHC",
            };
            loRtn.DetailData = new PMR00221ReportDataDTO();
            loRtn.DetailData.Header = "PM";
            loRtn.DetailData.Title = "LOI Status";
            loRtn.DetailData.Column = new PMR00220LabelDTO();
            loRtn.DetailData.HeaderParam = new PMR00220ParamDTO()
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
                CREPORT_TYPE_DISPLAY="Detail"
            };
            DateTime loFromDate = DateTime.ParseExact(loRtn.DetailData.HeaderParam.CFROM_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);
            DateTime loToDate = DateTime.ParseExact(loRtn.DetailData.HeaderParam.CTO_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);
            loRtn.DetailData.HeaderParam.CPERIOD_DISPLAY = (loFromDate.Year != loToDate.Year || loFromDate.Month != loToDate.Month)
                ? $"{loFromDate:MMMM yyyy} – {loToDate:MMMM yyyy}"
                : $"{loFromDate:MMMM yyyy}";

            //loRtn.DetailData.Data = new List<PMR00221DataDTO>();

            string companyId = "C1";
            string propertyId = "P1";
            string deptCode = "D1";
            string deptName = "Department 1";

            string[] transCodes = { "T1", "T2" };
            string[] transNames = { "Transaction 1", "Transaction 2" };

            string[,] salesmen = { { "S1", "Salesman 1" }, { "S2", "Salesman 2" } };

            //for (int i = 0; i < transCodes.Length; i++)
            //{
            //    string transCode = transCodes[i];
            //    string transName = transNames[i];

            //    for (int j = 0; j < salesmen.GetLength(0); j++)
            //    {
            //        string salesmanId = salesmen[j, 0];
            //        string salesmanName = salesmen[j, 1];

            //        for (int k = 1; k <= 2; k++)
            //        {
            //            PMR00221DataDTO dto = new PMR00221DataDTO
            //            {
            //                CCOMPANY_ID = companyId,
            //                CPROPERTY_ID = propertyId,
            //                CDEPT_CODE = deptCode,
            //                CDEPT_NAME = deptName,
            //                CTRANS_CODE = transCode,
            //                CTRANS_NAME = transName,
            //                CREF_NO = $"R{j + 1}{k}",
            //                CREF_DATE = DateTime.Now.ToString("yyyy-MM-dd"),
            //                CTENANT_ID = $"Tenant{j + 1}{k}",
            //                CTENANT_NAME = $"Tenant Name {j + 1}{k}",
            //                CTENURE = "Tenure",
            //                CSALESMAN_ID = salesmanId,
            //                CSALESMAN_NAME = salesmanName,
            //                CTC_CODE = "TC1",
            //                CTC_MESSAGE = "Message",
            //                CAGREEMENT_STATUS_ID = "A1",
            //                CAGREEMENT_STATUS_NAME = "Agreement Status",
            //                CTRANS_STATUS_ID = "TS1",
            //                CTRANS_STATUS_NAME = "Transaction Status",
            //                CUNIT_DETAIL_ID = $"Unit{k}",
            //                CUNIT_DETAIL_NAME = $"Unit Name {k}",
            //                NUNIT_DETAIL_PRICE = 1000,
            //                NUNIT_DETAIL_GROSS_AREA_SIZE = 100,
            //                NUNIT_DETAIL_NET_AREA_SIZE = 80,
            //                NUNIT_DETAIL_COMMON_AREA_SIZE = 20,
                            
            //                CUTILITY_DETAIL_TYPE_NAME = "Electricity",
            //                CUTILITY_DETAIL_UNIT_NAME = $"Unit {k}",
            //                CUTILITY_DETAIL_CHARGE_NAME = "Charge 1",
            //                CUTILITY_DETAIL_TAX_NAME = "PPN 10%",
            //                CUTILITY_DETAIL_METER_NO = "Meter 1",
            //                IUTILITY_DETAIL_METER_START = 1000,
            //                CUTILITY_DETAIL_START_INV_PRD = "May 2024",
            //                IUTILITY_DETAIL_INVOICING_PERIOD = 1,
            //                CUTILITY_DETAIL_STATUS_NAME = "Active",
            //                CCHARGE_DETAIL_TYPE_NAME = "Rent",
            //                CCHARGE_DETAIL_UNIT_NAME = $"Unit {k}",
            //                CCHARGE_DETAIL_CHARGE_NAME = "Installment",
            //                CCHARGE_DETAIL_TAX_NAME = "PPN 11%",
            //                CCHARGE_DETAIL_START_DATE = "2 Apr 2024",
            //                CCHARGE_DETAIL_END_DATE = "2 Apr 2025",
            //                CCHARGE_DETAIL_TENURE = "1 year(s), 1 month(s), 0 day(s)",
            //                CCHARGE_DETAIL_FEE_METHOD = "Monthly",
            //                NCHARGE_DETAIL_FEE_AMOUNT = 2000000,
            //                NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT = 24000000,
            //                CDEPOSIT_DETAIL_ID = $"Depo {k}",
            //                CDEPOSIT_DETAIL_DATE = "2 Jan 2024",
            //                NDEPOSIT_DETAIL_AMOUNT = 500000,
            //                CDEPOSIT_DETAIL_DESCRIPTION = "Deposit description",
            //                CDOCUMENT_DETAIL_NO = $"Doc {k}",
            //                CDOCUMENT_DETAIL_DATE = "2 Jan 2024",
            //                CDOCUMENT_DETAIL_EXPIRED_DATE = "2 Jan 2024",
            //                CDOCUMENT_DETAIL_DESCRIPTION = "Document description",
            //                CDOCUMENT_DETAIL_FILE = "/path/to/file"
            //            };

            //            loRtn.DetailData.Data.Add(dto);
            //        }
            //    }
            //}
            return loRtn;
        }
    }
}
