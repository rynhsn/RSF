using BaseHeaderReportCOMMON;
using PMR00600COMMON.DTO_s.PrintDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00600COMMON.Model
{
    public class PMR00601DummyData
    {
        public static PMR00601PrintDisplayDTO PMR00601PrintDislpayWithBaseHeader()
        {
            PMR00601PrintDisplayDTO loRtn = new PMR00601PrintDisplayDTO();

            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "Overtime",
                CUSER_ID = "GHC",
            };
            loRtn.ReportDataDTO = new PMR00601ReportDataDTO();
            loRtn.ReportDataDTO.Header = "Header";
            loRtn.ReportDataDTO.Title = "Overtime Detail Report";
            loRtn.ReportDataDTO.Label = new PMR00601LabelDTO();
            loRtn.ReportDataDTO.Param = new PMR00601ParamDTO()
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
                LINVOICE = true,
                LSERVICE = true,
                LSTATUS = true,
                LTENANT = true,
                CGROUP_TYPE_DISPLAY = "Charges",
                CREPORT_TYPE_DISPLAY = "Detail"

            };
            loRtn.ReportDataDTO.Data = new List<PMR00601DataDTO>();

            string[] companyIds = { "C001" };
            string[] propertyIds = { "P001" };
            string[] buildingIds = { "B001" };
            string[] buildingNames = { "Building A" };
            string[] deptCodes = { "D001" };
            string[] deptNames = { "Department A" };
            string[] tenantIds = { "T001", "T002" };
            string[] tenantNames = { "Tenant A", "Tenant B" };
            string[] invoiceNos = { "INV001", "INV002" };
            string[] agreementNos = { "AGR001", "AGR002" };
            string[] chargeIds = { "CHG001", "CHG002" };
            string[] chargeNames = { "Charge A", "Charge B" };

            decimal amount = 100000000;
            int invoiceCounter = 0;

            foreach (var companyId in companyIds)
            {
                foreach (var propertyId in propertyIds)
                {
                    foreach (var buildingId in buildingIds)
                    {
                        foreach (var buildingName in buildingNames)
                        {
                            foreach (var deptCode in deptCodes)
                            {
                                foreach (var deptName in deptNames)
                                {
                                    foreach (var tenantId in tenantIds)
                                    {
                                        foreach (var tenantName in tenantNames)
                                        {
                                            foreach (var invoiceNo in invoiceNos)
                                            {
                                                invoiceCounter++;
                                                foreach (var agreementNo in agreementNos)
                                                {
                                                    foreach (var chargeId in chargeIds)
                                                    {
                                                        foreach (var chargeName in chargeNames)
                                                        {
                                                            loRtn.ReportDataDTO.Data.Add(new PMR00601DataDTO
                                                            {
                                                                CCOMPANY_ID = companyId,
                                                                CPROPERTY_ID = propertyId,
                                                                CBUILDING_ID = buildingId,
                                                                CBUILDING_NAME = buildingName,
                                                                CDEPT_CODE = deptCode,
                                                                CDEPT_NAME = deptName,
                                                                CTENANT_ID = tenantId,
                                                                CTENANT_NAME = tenantName,
                                                                CINVOICE_NO = invoiceNo,
                                                                CINVOICE_DATE = DateTime.Now.ToString("yyyyMMdd"),
                                                                NINVOICE_AMOUNT = amount,
                                                                NINVOICE_TAX_AMOUNT = amount * 0.1m,
                                                                NINVOICE_TOTAL_AMOUNT = amount * 1.1m,
                                                                CAGREEMENT_NO = agreementNo,
                                                                CAGREEMENT_DATE = DateTime.Now.ToString("yyyyMMdd"),
                                                                CCHARGE_ID = chargeId,
                                                                CCHARGE_NAME = chargeName,
                                                                CUNIT_ID = "U001",
                                                                CUNIT_NAME = "Unit 1",
                                                                COVERTIME_DATE = DateTime.Now.ToString("yyyyMMdd"),
                                                                CTIME_IN = "08:00",
                                                                CTIME_OUT = "17:00",
                                                                CTENURE = "3 Years",
                                                                NUNIT_PRICE = amount,
                                                                NUNIT_ACTUAL_AMOUNT = amount,
                                                                CNOTE = "Note",
                                                                CTRANS_STATUS_CODE = "1",
                                                                CTRANS_STATUS_NAME = "Active"
                                                            });
                                                            amount += 100000000;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return loRtn;
        }
    }
}
