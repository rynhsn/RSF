using BaseHeaderReportCOMMON;
using PMR00100Common.DTOs;
using PMR00100Common.Report;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PMR00100Common.Model
{
    public static class GenerateDataModel
    {
        public static LOOStatusReportResultDTO DefaultData()
        {
            LOOStatusReportResultDTO loData = new LOOStatusReportResultDTO()
            {
                title = "LOO Status",
                Header = new LOOStatusHeaderDTO()
                {
                    CPROPERTY = "12345 - Jakarta Cyber Punk",
                    CDEPT = "D001 - Sales Department",
                    CSALESMAN = "S001 - John Doe",
                    CPERIOD = "Januari 2023 - Desember 2024"
                },
                Column = new LOOStatusColumnDTO(),
                DataLOOStatus = new List<LOOStatusDetail1DTO>()
            };
            List<PMR00100DTO> loCollection = new List<PMR00100DTO>();

            int lnData1 = 3;
            int lnData2 = 3;
            int lnData3 = 3;
            int lnData4 = 5;
            for (int a = 1;a < lnData1;a++)
            {
                for (int b = 1;b < lnData2;b++)
                {
                    for (int c = 1;c < lnData3;c++)
                    {
                        for (int d = 1;d < lnData4;d++)
                        {
                            loCollection.Add(new PMR00100DTO()
                            {
                                CCOMPANY_ID = $"Company {a}",
                                CPROPERTY_ID = $"Property {a}",
                                CDEPT_CODE = $"Dept {a}",
                                CDEPT_NAME = $"Department {a}",
                                CTRANS_CODE = $"Trans Code {a}",
                                CTRANS_NAME = $"Trans Type {a}",
                                CSALESMAN_ID = $"SL0{b}",
                                CSALESMAN_NAME = $"Sales {b}",
                                CREF_NO = $"REF-00{c}",
                                CREF_DATE = $"202{c}0801",
                                CTENANT_ID = $"TEN-0{c}",
                                CTENANT_NAME = $"Tenant Name {c}",
                                CTENURE = $"{c} Month, {c} Days",
                                CAGREEMENT_STATUS_ID = $"AgreementID {c}",
                                CAGREEMENT_STATUS_NAME = $"Signed{c}",
                                CTRANS_STATUS_ID = $"StatusID {c}",
                                CTRANS_STATUS_NAME = $"Inprogress{c}",
                                NREVISION_COUNT = c,
                                NTOTAL_PRICE = c * 1000.0m,
                                CTAX = $"PPN {c}%",
                                CTC_CODE = $"TC Code {c}",
                                CTC_DESCRIPTION = $"TC Description {c}",
                                CTC_MESSAGE = $"TC Message {c}",
                                CUNIT_DETAIL_ID = $"Unit {d}",
                                CUNIT_DETAIL_NAME = $"Unit Name {d}",
                                NUNIT_DETAIL_GROSS_AREA_SIZE = d * 100.0m,
                                NUNIT_DETAIL_NET_AREA_SIZE = d * 80.0m,
                                NUNIT_DETAIL_COMMON_AREA_SIZE = d * 20.0m,
                                NUNIT_DETAIL_PRICE = d * 1500.0m,
                                CCHARGE_DETAIL_TYPE_NAME = $"Charge Type {d}",
                                CCHARGE_DETAIL_UNIT_NAME = $"Unit Name {d}",
                                CCHARGE_DETAIL_CHARGE_NAME = $"Charge Name {d}",
                                CCHARGE_DETAIL_TAX_NAME = $"Tax Name {d}",
                                CCHARGE_DETAIL_START_DATE = $"Start Date {d}",
                                CCHARGE_DETAIL_END_DATE = $"End Date {d}",
                                CCHARGE_DETAIL_TENURE = $"Tenure {d}",
                                CCHARGE_DETAIL_FEE_METHOD = $"Fee Method {d}",
                                NCHARGE_DETAIL_FEE_AMOUNT = d * 500.0m,
                                LFOR_TOTAL_AREA = true,
                                NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT = d * 550.0m,
                                CDEPOSIT_DETAIL_ID = $"DepositID-{d}",
                                CDEPOSIT_DETAIL_DATE = $"Deposit Date {c}",
                                NDEPOSIT_DETAIL_AMOUNT = c * 200.0m,
                                CDEPOSIT_DETAIL_DESCRIPTION = $"Deposit Description {c}"
                            });
                        }
                    }
                }
            }


            var loTempData = loCollection
        .GroupBy(data1a => new
        {
            data1a.CTRANS_NAME, // First level of grouping
        })
        .Select(data1b => new LOOStatusDetail1DTO()
        {
            CTRANS_NAME = data1b.Key.CTRANS_NAME,
            LOOStatusDetail2 = data1b.GroupBy(data2a => new
            {
                data2a.CSALESMAN_ID, // Second level of grouping
                data2a.CSALESMAN_NAME,
            }).Select(data2b => new LOOStatusDetail2DTO()
            {
                CSALESMAN_ID = data2b.Key.CSALESMAN_ID,
                CSALESMAN_NAME = data2b.Key.CSALESMAN_NAME,
                LOOStatusDetail3 = data2b.GroupBy(data3a => new
                {
                    data3a.CREF_NO, // Third level of grouping
                    data3a.CREF_DATE,
                    data3a.CTENURE,
                    data3a.CAGREEMENT_STATUS_NAME,
                    data3a.CTRANS_STATUS_NAME,
                    data3a.NREVISION_COUNT,
                    data3a.CTAX,
                    data3a.CTENANT_ID,
                    data3a.CTENANT_NAME,
                    data3a.CTC_MESSAGE,
                }).Select(data3b => new LOOStatusDetail3DTO()
                {
                    CREF_NO = data3b.Key.CREF_NO,
                    DREF_DATE = ConvertStringToDate(data3b.Key.CREF_DATE,"yyyyMMdd"),
                    CTENURE = data3b.Key.CTENURE,
                    CAGREEMENT_STATUS_NAME = data3b.Key.CAGREEMENT_STATUS_NAME,
                    CTRANS_STATUS_NAME = data3b.Key.CTRANS_STATUS_NAME,
                    NREVISION_COUNT = data3b.Key.NREVISION_COUNT,
                    CTAX = data3b.Key.CTAX,
                    CTENANT_ID = data3b.Key.CTENANT_ID,
                    CTENANT_NAME = data3b.Key.CTENANT_NAME,
                    CTC_MESSAGE = data3b.Key.CTC_MESSAGE,
                    LOOStatusDetailUnit = data3b.GroupBy(dataUnit => new
                    {
                        dataUnit.CUNIT_DETAIL_ID,
                        dataUnit.CUNIT_DETAIL_NAME,
                        dataUnit.NUNIT_DETAIL_GROSS_AREA_SIZE,
                        dataUnit.NUNIT_DETAIL_NET_AREA_SIZE,
                        dataUnit.NUNIT_DETAIL_COMMON_AREA_SIZE,
                        dataUnit.NUNIT_DETAIL_PRICE,

                    }).Select(selectUnit => new LOOStatusDetailUnitDTO()
                    {
                        CUNIT_DETAIL_ID = selectUnit.Key.CUNIT_DETAIL_ID,
                        CUNIT_DETAIL_NAME = selectUnit.Key.CUNIT_DETAIL_NAME,
                        NUNIT_DETAIL_GROSS_AREA_SIZE = selectUnit.Key.NUNIT_DETAIL_GROSS_AREA_SIZE,
                        NUNIT_DETAIL_NET_AREA_SIZE = selectUnit.Key.NUNIT_DETAIL_NET_AREA_SIZE,
                        NUNIT_DETAIL_COMMON_AREA_SIZE = selectUnit.Key.NUNIT_DETAIL_COMMON_AREA_SIZE,
                        NUNIT_DETAIL_PRICE = selectUnit.Key.NUNIT_DETAIL_PRICE,

                        ITOTAL_UNIT = selectUnit.Count()
                    }).ToList(),
                    LOOStatusDetailCharge = data3b
                    .GroupBy(dataCharge => new
                    {
                        dataCharge.CCHARGE_DETAIL_TYPE_NAME,
                        dataCharge.CCHARGE_DETAIL_UNIT_NAME,
                        dataCharge.CCHARGE_DETAIL_CHARGE_NAME,
                        dataCharge.CCHARGE_DETAIL_TAX_NAME,
                        dataCharge.CCHARGE_DETAIL_START_DATE,
                        dataCharge.CCHARGE_DETAIL_END_DATE,
                        dataCharge.CCHARGE_DETAIL_TENURE,
                        dataCharge.CCHARGE_DETAIL_FEE_METHOD,
                        dataCharge.NCHARGE_DETAIL_FEE_AMOUNT,
                        dataCharge.LFOR_TOTAL_AREA,
                        dataCharge.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT,
                        dataCharge.CDEPOSIT_DETAIL_ID,
                        dataCharge.CDEPOSIT_DETAIL_DATE,
                        dataCharge.NDEPOSIT_DETAIL_AMOUNT,
                        dataCharge.CDEPOSIT_DETAIL_DESCRIPTION,
                    })
                    .Select(selectCharge => new LOOStatusDetailChargeDTO()
                    {
                        CCHARGE_DETAIL_TYPE_NAME = selectCharge.Key.CCHARGE_DETAIL_TYPE_NAME,
                        LOOStatusDetailChargeUnit = selectCharge.GroupBy(chargeUnit => new
                        {
                            chargeUnit.CCHARGE_DETAIL_UNIT_NAME,
                        }).Select(selectChargeUnit => new LOOStatusDetailChargeTypeUnitDTO()
                        {
                            CCHARGE_DETAIL_UNIT_NAME = selectChargeUnit.Key.CCHARGE_DETAIL_UNIT_NAME,
                            LOOStatusDetailChargeTypeUnitCharge = selectChargeUnit.Select(charge => new LOOStatusDetailChargeTypeUnitChargeDTO()
                            {
                                CCHARGE_DETAIL_CHARGE_NAME = charge.CCHARGE_DETAIL_CHARGE_NAME,
                                CCHARGE_DETAIL_TAX_NAME = charge.CCHARGE_DETAIL_TAX_NAME,
                                DCHARGE_DETAIL_START_DATE = ConvertStringToDate(charge.CCHARGE_DETAIL_START_DATE,"yyyyMMdd"),
                                DCHARGE_DETAIL_END_DATE = ConvertStringToDate(charge.CCHARGE_DETAIL_END_DATE,"yyyyMMdd"),
                                CCHARGE_DETAIL_TENURE = charge.CCHARGE_DETAIL_TENURE,
                                LFOR_TOTAL_AREA = charge.LFOR_TOTAL_AREA,
                                CCHARGE_DETAIL_FEE_METHOD = charge.CCHARGE_DETAIL_FEE_METHOD,
                                NCHARGE_DETAIL_FEE_AMOUNT = charge.NCHARGE_DETAIL_FEE_AMOUNT,
                                NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT = charge.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT,
                            }).ToList(),
                        }).ToList(),
                    }).ToList(),
                    LOOStatusDetailDeposit = data3b
                    .GroupBy(dataDeposit => new
                    {
                        dataDeposit.CDEPOSIT_DETAIL_ID,
                        dataDeposit.CDEPOSIT_DETAIL_DATE,
                        dataDeposit.NDEPOSIT_DETAIL_AMOUNT,
                        dataDeposit.CDEPOSIT_DETAIL_DESCRIPTION,
                    }).Select(selectDeposit => new LOOStatusDetailDepositDTO()
                    {
                        CDEPOSIT_DETAIL_ID = selectDeposit.Key.CDEPOSIT_DETAIL_ID,
                        DDEPOSIT_DETAIL_DATE = ConvertStringToDate(selectDeposit.Key.CDEPOSIT_DETAIL_DATE,"yyyyMMdd"),
                        NDEPOSIT_DETAIL_AMOUNT = selectDeposit.Key.NDEPOSIT_DETAIL_AMOUNT,
                        CDEPOSIT_DETAIL_DESCRIPTION = selectDeposit.Key.CDEPOSIT_DETAIL_DESCRIPTION,
                        ITOTAL_DEPOSIT = selectDeposit.Count()
                    }).ToList(),
                }).ToList()
            }).ToList()
        }).ToList();

            loData.DataLOOStatus = loTempData;
            return loData;
        }
        public static PMR00100LOOStatusResultWithBaseHeaderDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "LOO Status",
                CUSER_ID = "AKC"
            };
            PMR00100LOOStatusResultWithBaseHeaderDTO loRtn = new PMR00100LOOStatusResultWithBaseHeaderDTO();
            loRtn.BaseHeaderData = loParam;
            loRtn.PMR00100PrintData = DefaultData();
            return loRtn;
        }
        public static DateTime? ConvertStringToDate(string dateString,string inputFormat)
        {
            if (DateTime.TryParseExact(dateString,inputFormat,CultureInfo.InvariantCulture,DateTimeStyles.AssumeUniversal,out var date))
            {
                return date;
            }
            else
            {
                return null; // Jika parsing gagal, kembalikan null
            }
        }

    }
}
