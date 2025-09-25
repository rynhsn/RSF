using BaseHeaderReportCOMMON;
using PMR00600COMMON.DTO_s.Print;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PMR00600COMMON.DTO_s.Model
{
    public static class PMR00600DummyData
    {
        public static PMR00600PrintDisplayDTO PMR00600PrintDislpayWithBaseHeader()
        {
            PMR00600PrintDisplayDTO loRtn = new PMR00600PrintDisplayDTO();

            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "Overtime",
                CUSER_ID = "GHC",
            };
            loRtn.ReportDataDTO = new PMR00600ReportDataDTO();
            loRtn.ReportDataDTO.Header = "Header";
            loRtn.ReportDataDTO.Title = "Overtime Report";
            loRtn.ReportDataDTO.Label = new PMR00600LabelDTO();
            loRtn.ReportDataDTO.Param = new PMR00600ParamDTO()
            {
                CCOMPANY_ID = "RCD",
                CPROPERTY_ID = "ProeprtyID",
                CPROPERTY_NAME = "Property Name",
                CFROM_PERIOD = "20240612",
                CTO_PERIOD = "20240712",
                CFROM_BUILDING_ID = "BuildingID001",
                CFROM_BUILDING_NAME = "BuildingName001",
                CTO_BUILDING_ID = "BuildingID002",
                CTO_BUILDING_NAME = "BuildingName002",
                CFROM_DEPT_CODE = "dept001",
                CFROM_DEPT_NAME = "deptName001",
                CTO_DEPT_CODE = "dept002",
                CTO_DEPT_NAME = "deptName002",
                CREPORT_TYPE = "Summary",
                CFROM_TENANT_ID = "Tenant01",
                CFROM_TENANT_NAME = "TenantName001",
                CTO_TENANT_ID = "Tenant02",
                CTO_TENANT_NAME = "TenantName002",
                CFROM_SERVICE_ID = "Serv1",
                CFROM_SERVICE_NAME = "ServName1",
                CTO_SERVICE_ID = "serv2",
                CTO_SERVICE_NAME = "ServName2",
                CSTATUS = "1", //"2"
                CSTATUS_DISPLAY = "Open",
                CINVOICE = "1", //"2"
                CINVOICE_DISPLAY = "Involved",
                CLANG_ID = "en",
                LINVOICE = false,
                LSERVICE = false,
                LSTATUS = true,
                LTENANT = true,
                CGROUP_TYPE_DISPLAY="Tenant",
                CREPORT_TYPE_DISPLAY = "Summary"

            };
            DateTime loFromDate = DateTime.ParseExact(loRtn.ReportDataDTO.Param.CFROM_PERIOD, "yyyyMMdd", CultureInfo.InvariantCulture);
            DateTime loToDate = DateTime.ParseExact(loRtn.ReportDataDTO.Param.CTO_PERIOD, "yyyyMMdd", CultureInfo.InvariantCulture);
            loRtn.ReportDataDTO.Param.CPERIOD_DISPLAY = (loFromDate.Year != loToDate.Year || loFromDate.Month != loToDate.Month)
                ? $"{loFromDate:MMMM yyyy} – {loToDate:MMMM yyyy}"
                : $"{loFromDate:MMMM yyyy}";
            loRtn.ReportDataDTO.Data = new List<PMR00600DataDTO>()
            {
               // Tenant 1, Agreement 1, Charge 1
                new PMR00600DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "TENANT01",
                    CTENANT_NAME = "Muhammad Andika Putra",
                    CAGREEMENT_NO = "AGREEMENT01",
                    CCHARGE_ID = "CHG01",
                    CCHARGE_NAME = "Overtime Weekday(s)",
                    NJAN_AMOUNT = 20000000,
                    NFEB_AMOUNT = 20000000,
                    NMAR_AMOUNT = 20000000,
                    NAPR_AMOUNT = 20000000,
                    NMAY_AMOUNT = 20000000,
                    NJUN_AMOUNT = 20000000,
                    NJUL_AMOUNT = 20000000,
                    NAUG_AMOUNT = 20000000,
                    NSEP_AMOUNT = 20000000,
                    NOCT_AMOUNT = 20000000,
                    NNOV_AMOUNT = 20000000,
                    NDEC_AMOUNT = 20000000
                },
                // Tenant 1, Agreement 1, Charge 2
                new PMR00600DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "TENANT01",
                    CTENANT_NAME = "Muhammad Andika Putra",
                    CAGREEMENT_NO = "AGREEMENT01",
                    CCHARGE_ID = "CHG02",
                    CCHARGE_NAME = "Overtime Weekend(s)",
                    NJAN_AMOUNT = 40000000,
                    NFEB_AMOUNT = 40000000,
                    NMAR_AMOUNT = 40000000,
                    NAPR_AMOUNT = 40000000,
                    NMAY_AMOUNT = 40000000,
                    NJUN_AMOUNT = 40000000,
                    NJUL_AMOUNT = 40000000,
                    NAUG_AMOUNT = 40000000,
                    NSEP_AMOUNT = 40000000,
                    NOCT_AMOUNT = 40000000,
                    NNOV_AMOUNT = 40000000,
                    NDEC_AMOUNT = 40000000
                },
                // Tenant 1, Agreement 2, Charge 1
                new PMR00600DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "TENANT01",
                    CTENANT_NAME = "Muhammad Andika Putra",
                    CAGREEMENT_NO = "AGREEMENT02",
                    CCHARGE_ID = "CHG01",
                    CCHARGE_NAME = "Overtime Weekday(s)",
                    NJAN_AMOUNT = 10000000,
                    NFEB_AMOUNT = 10000000,
                    NMAR_AMOUNT = 10000000,
                    NAPR_AMOUNT = 10000000,
                    NMAY_AMOUNT = 10000000,
                    NJUN_AMOUNT = 10000000,
                    NJUL_AMOUNT = 10000000,
                    NAUG_AMOUNT = 10000000,
                    NSEP_AMOUNT = 10000000,
                    NOCT_AMOUNT = 10000000,
                    NNOV_AMOUNT = 10000000,
                    NDEC_AMOUNT = 10000000
                },
                // Tenant 1, Agreement 2, Charge 2
                new PMR00600DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "TENANT01",
                    CTENANT_NAME = "Muhammad Andika Putra",
                    CAGREEMENT_NO = "AGREEMENT02",
                    CCHARGE_ID = "CHG02",
                    CCHARGE_NAME = "Overtime Weekend(s)",
                    NJAN_AMOUNT = 30000000,
                    NFEB_AMOUNT = 30000000,
                    NMAR_AMOUNT = 30000000,
                    NAPR_AMOUNT = 30000000,
                    NMAY_AMOUNT = 30000000,
                    NJUN_AMOUNT = 30000000,
                    NJUL_AMOUNT = 30000000,
                    NAUG_AMOUNT = 30000000,
                    NSEP_AMOUNT = 30000000,
                    NOCT_AMOUNT = 30000000,
                    NNOV_AMOUNT = 30000000,
                    NDEC_AMOUNT = 30000000
                },
                // Tenant 2, Agreement 1, Charge 1
                new PMR00600DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "TENANT02",
                    CTENANT_NAME = "Aurelius Irvin",
                    CAGREEMENT_NO = "AGREEMENT01",
                    CCHARGE_ID = "CHG01",
                    CCHARGE_NAME = "Overtime Weekday(s)",
                    NJAN_AMOUNT = 20000000,
                    NFEB_AMOUNT = 20000000,
                    NMAR_AMOUNT = 20000000,
                    NAPR_AMOUNT = 20000000,
                    NMAY_AMOUNT = 20000000,
                    NJUN_AMOUNT = 20000000,
                    NJUL_AMOUNT = 20000000,
                    NAUG_AMOUNT = 20000000,
                    NSEP_AMOUNT = 20000000,
                    NOCT_AMOUNT = 20000000,
                    NNOV_AMOUNT = 20000000,
                    NDEC_AMOUNT = 20000000
                },
                // Tenant 2, Agreement 1, Charge 2
                new PMR00600DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "TENANT02",
                    CTENANT_NAME = "Aurelius Irvin",
                    CAGREEMENT_NO = "AGREEMENT01",
                    CCHARGE_ID = "CHG02",
                    CCHARGE_NAME = "Overtime Weekend(s)",
                    NJAN_AMOUNT = 40000000,
                    NFEB_AMOUNT = 40000000,
                    NMAR_AMOUNT = 40000000,
                    NAPR_AMOUNT = 40000000,
                    NMAY_AMOUNT = 40000000,
                    NJUN_AMOUNT = 40000000,
                    NJUL_AMOUNT = 40000000,
                    NAUG_AMOUNT = 40000000,
                    NSEP_AMOUNT = 40000000,
                    NOCT_AMOUNT = 40000000,
                    NNOV_AMOUNT = 40000000,
                    NDEC_AMOUNT = 40000000
                },
                // Tenant 2, Agreement 2, Charge 1
                new PMR00600DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "TENANT02",
                    CTENANT_NAME = "Aurelius Irvin",
                    CAGREEMENT_NO = "AGREEMENT02",
                    CCHARGE_ID = "CHG01",
                    CCHARGE_NAME = "Overtime Weekday(s)",
                    NJAN_AMOUNT = 10000000,
                    NFEB_AMOUNT = 10000000,
                    NMAR_AMOUNT = 10000000,
                    NAPR_AMOUNT = 10000000,
                    NMAY_AMOUNT = 10000000,
                    NJUN_AMOUNT = 10000000,
                    NJUL_AMOUNT = 10000000,
                    NAUG_AMOUNT = 10000000,
                    NSEP_AMOUNT = 10000000,
                    NOCT_AMOUNT = 10000000,
                    NNOV_AMOUNT = 10000000,
                    NDEC_AMOUNT = 10000000
                },
                // Tenant 2, Agreement 2, Charge 2
                new PMR00600DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "TENANT02",
                    CTENANT_NAME = "Aurelius Irvin",
                    CAGREEMENT_NO = "AGREEMENT02",
                    CCHARGE_ID = "CHG02",
                    CCHARGE_NAME = "Overtime Weekend(s)",
                    NJAN_AMOUNT = 30000000,
                    NFEB_AMOUNT = 30000000,
                    NMAR_AMOUNT = 30000000,
                    NAPR_AMOUNT = 30000000,
                    NMAY_AMOUNT = 30000000,
                    NJUN_AMOUNT = 30000000,
                    NJUL_AMOUNT = 30000000,
                    NAUG_AMOUNT = 30000000,
                    NSEP_AMOUNT = 30000000,
                    NOCT_AMOUNT = 30000000,
                    NNOV_AMOUNT = 30000000,
                    NDEC_AMOUNT = 30000000
                }
            };
            return loRtn;
        }
    }
}
