using BaseHeaderReportCOMMON.Models;
using PMR00150COMMON.DTO_Report_Detail;
using PMR00150COMMON.DTO_Report_Detail.Detail;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PMR00150COMMON
{
    public static class GenerateDataModel
    {
        #region Format Summary

        public static PMR00150SummaryResultDTO DefaultDataSummary()
        {
            PMR00150SummaryResultDTO loReturn = new PMR00150SummaryResultDTO()
            {
                Title = "LOC Status",
                Header = new PMR00150DataHeaderDTO()
                {
                    PROPERTY = "Harco Mas",
                    FROM_DEPARTMENT = "Accounting",
                    TO_DEPARTMENT = "Finance",
                    FROM_SALESMEN = "Accounting Staff",
                    TO_SALESMEN = "Finance Staff",
                    FROM_PERIOD = "02 Apr 2024",
                    TO_PERIOD = "03 Apr 2024"
                },
                ColumnSummary = new PMR00150ColumnSummaryDTO(),
                Label = new PMR00150LabelDTO()
            };

            List<PMR00150DataSummaryDbDTO> loCollectionData = new List<PMR00150DataSummaryDbDTO>();

            //GENERATE DATA DUMMY
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        loCollectionData.Add(new PMR00150DataSummaryDbDTO()
                        {
                            CTRANS_CODE = $"Code {i}",
                            CTRANS_NAME = $"TransName {i}",
                            CSALESMAN_ID = $"ID{j}",
                            CSALESMAN_NAME = $"NameStaff{j}",
                            CREF_NO = $"RfNo{k}",
                            CREF_DATE = "20241224",
                            //    DREF_DATE = ConvertStringToDateTimeFormat("20241224"),
                            CTENURE = $"Tenure{k}",
                            NTOTAL_GROSS_AREA_SIZE = 200m * k,
                            NTOTAL_NET_AREA_SIZE = 300m * k,
                            NTOTAL_COMMON_AREA_SIZE = 400m * k,
                            CAGREEMENT_STATUS_NAME = $"sign{k}",
                            CTRANS_STATUS_NAME = $"open{k}",
                            NTOTAL_PRICE = 550000m * k,
                            CTAX = $"PPN % {k}",
                            CTENANT_ID = $"TENANNT {k}",
                            CTENANT_NAME = $"Tenant NAME {k}",
                            CTC_MESSAGE = $"This is Dummy data form TNC{k}"
                        }
                        );
                    }
                }
            }

            //Grouping Data From DB to Ready to Display on Report

            var loTempData = loCollectionData
                .GroupBy(x => new
                {
                    x.CTRANS_CODE,
                    x.CTRANS_NAME,
                }).Select(itemData1 => new PMR00150DataTransactionDTO
                {
                    CTRANS_CODE = itemData1.Key.CTRANS_CODE,
                    CTRANS_NAME = itemData1.Key.CTRANS_NAME,
                    TransactionDetail = itemData1
                    .GroupBy(x => new
                    {
                        x.CSALESMAN_ID,
                        x.CSALESMAN_NAME,
                    }).Select(itemData2 => new PMR00150DataTransactionDetailDTO
                    {
                        CSALESMAN_ID = itemData2.Key.CSALESMAN_ID,
                        CSALESMAN_NAME = itemData2.Key.CSALESMAN_NAME,
                        SalesmanDetail = itemData2
                        .Select(x => new PMR00150DataSalesmanDetailDTO
                        {
                            CREF_NO = x.CREF_NO,
                            CREF_DATE = x.CREF_DATE,
                            DREF_DATE = ConvertStringToDateTimeFormat(x.CREF_DATE!),
                            CTENURE = x.CTENURE,
                            NTOTAL_GROSS_AREA_SIZE = x.NTOTAL_GROSS_AREA_SIZE,
                            NTOTAL_NET_AREA_SIZE = x.NTOTAL_NET_AREA_SIZE,
                            NTOTAL_COMMON_AREA_SIZE = x.NTOTAL_COMMON_AREA_SIZE,
                            CAGREEMENT_STATUS_NAME = x.CAGREEMENT_STATUS_NAME,
                            CTRANS_STATUS_NAME = x.CTRANS_STATUS_NAME,
                            NTOTAL_PRICE = x.NTOTAL_PRICE,
                            CTAX = x.CTAX,
                            CTENANT_ID = x.CTENANT_ID,
                            CTENANT_NAME = x.CTENANT_NAME,
                            CTC_MESSAGE = x.CTC_MESSAGE
                        }).ToList(),
                    }).ToList(),
                }).ToList();

            loReturn.Data = loTempData;
            return loReturn;
        }
        public static PMR00150SummaryResultWithHeaderDTO DefaultDataSummaryWithHeader()
        {
            PMR00150SummaryResultWithHeaderDTO loReturn = new PMR00150SummaryResultWithHeaderDTO();
            loReturn.BaseHeaderData = GenerateDataModelHeader.DefaultData().BaseHeaderData;
            loReturn.PMR00150SummaryResulDataFormatDTO = DefaultDataSummary();
            return loReturn;
        }
        #endregion

        #region Format DETAIL

        public static PMR00150DetailResultDTO DefaultDataDetail()
        {
            PMR00150DetailResultDTO loReturn = new PMR00150DetailResultDTO()
            {
                Title = "LOC Status",
                Header = new PMR00150DataHeaderDTO()
                {
                    PROPERTY = "Harco Mas",
                    FROM_DEPARTMENT = "Accounting",
                    TO_DEPARTMENT = "Finance",
                    FROM_SALESMEN = "Accounting Staff",
                    TO_SALESMEN = "Finance Staff",
                    FROM_PERIOD = "02 Apr 2024",
                    TO_PERIOD = "03 Apr 2024"
                },
                ColumnDetail = new PMR00150ColumnDetailDTO(),
                ParameterVisible = new PMR00150ParameterVisibleDTO()
                {
                    LVISIBLE_DETAIL_UNIT = true,
                    LVISIBLE_DETAIL_CHARGE = true,
                    LVISIBLE_DETAIL_DEPOSIT = true,
                },
                Label = new PMR00150LabelDTO()
            };
            List<PMR00150DataDetailDbDTO> loCollectionData = new List<PMR00150DataDetailDbDTO>();

            for (int i = 1; i < 2; i++) //TransName
            {
                for (int j = 1; j < 2; j++) //Salesman
                {
                    for (int k = 1; k < 2; k++) //REFDATE??LOCNO
                    {
                        for (int l = 1; l < 3; l++) //UNIT
                        {
                            for (int m = 1; m < 2; m++) //ChargeType
                            {
                                for (int n = 1; n < 4; n++) //ChargeTypeUnit
                                {
                                    for (int o = 1; o < 4; o++) //ChargeTypeUnitDetail
                                    {
                                        loCollectionData.Add(new PMR00150DataDetailDbDTO()
                                        {
                                            CTRANS_CODE = $"Code {i}",
                                            CTRANS_NAME = $"TransName {i}",
                                            CSALESMAN_ID = $"ID{i}",
                                            CSALESMAN_NAME = $"NameStaff{i}",
                                            CREF_NO = $"RfNo{k}",
                                            CREF_DATE = "20241224",
                                            //    DREF_DATE = ConvertStringToDateTimeFormat("20241224"),
                                            CTENURE = $"Tenure{k}",
                                            CAGREEMENT_STATUS_NAME = $"sign{k}",
                                            CTRANS_STATUS_NAME = $"open{k}",
                                            NTOTAL_PRICE = 550000m,
                                            CTAX = $"PPN % {k}",
                                            CTENANT_ID = $"TENANNT {k}",
                                            CTENANT_NAME = $"Tenant NAME {k}",
                                            CTC_MESSAGE = $"This is Dummy data form TNC{k}",
                                            CUNIT_DETAIL_ID = $"Detail ID Unit{l}",
                                            NUNIT_DETAIL_GROSS_AREA_SIZE = 20,
                                            NUNIT_DETAIL_NET_AREA_SIZE = 30,
                                            NUNIT_DETAIL_COMMON_AREA_SIZE = 40,
                                            NUNIT_DETAIL_PRICE = 50,
                                            NUNIT_TOTAL_GROSS_AREA_SIZE = 60,
                                            NUNIT_TOTAL_NET_AREA_SIZE = 90,
                                            NUNIT_TOTAL_COMMON_AREA_SIZE = 120,
                                            NUNIT_TOTAL_PRICE = 150,
                                            CCHARGE_DETAIL_TYPE_NAME = $"Charges Type {m}",
                                            CCHARGE_DETAIL_UNIT_NAME = $"Unit {m}",
                                            CCHARGE_DETAIL_CHARGE_NAME = $"ChargeName {n}",
                                            CCHARGE_DETAIL_TAX_NAME = $"TaxName {n}",
                                            CCHARGE_DETAIL_START_DATE = "20241224",
                                            CCHARGE_DETAIL_END_DATE = "20241224",
                                            CCHARGE_DETAIL_TENURE = $"FEE Method method {n}",
                                            CCHARGE_DETAIL_FEE_METHOD = $"Monthly {n}",
                                            NCHARGE_DETAIL_FEE_AMOUNT = 550000m * k,
                                            NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT = 160000m * k,
                                            NCHARGE_DETAIL_SUBTOTAL_CALCULATED_FEE_AMOUNT = 100000m * k,
                                            CDEPOSIT_DETAIL_ID = $"Depo {l}",
                                            CDEPOSIT_DETAIL_DATE = "20241224",
                                            NDEPOSIT_DETAIL_AMOUNT = 5120m * k,
                                            CDEPOSIT_DETAIL_DESCRIPTION = "Description Diterima"
                                        });
                                    }
                                }
                            }

                        }
                    }
                }
            }



            var loTempData = loCollectionData
                .GroupBy(Data => new
                {
                    Data.CTRANS_CODE,
                    Data.CTRANS_NAME,
                }).Select(Data1 => new PMR00150DataDetailTransactionDTO
                {
                    CTRANS_CODE = Data1.Key.CTRANS_CODE,
                    CTRANS_NAME = Data1.Key.CTRANS_NAME,
                    TransactionDetail = Data1
                   .GroupBy(Data => new
                   {
                       Data.CSALESMAN_ID,
                       Data.CSALESMAN_NAME,
                   }).Select(Data2 => new PMR00150DataDetailSalesmanDTO
                   {
                       CSALESMAN_ID = Data2.Key.CSALESMAN_ID,
                       CSALESMAN_NAME = Data2.Key.CSALESMAN_NAME,
                       SalesmanDetail = Data2
                       .GroupBy(Data3 => new
                       {
                           Data3.CREF_NO,
                           Data3.CREF_DATE,
                           Data3.CTENURE,
                           Data3.CAGREEMENT_STATUS_NAME,
                           Data3.CTRANS_STATUS_NAME,
                           Data3.NTOTAL_PRICE,
                           Data3.CTAX,
                           Data3.CTENANT_ID,
                           Data3.CTENANT_NAME,
                           Data3.CTC_MESSAGE,
                       }).Select(Data4 => new PMR00150DataDetailLocNoDTO
                       {
                           CREF_NO = Data4.Key.CREF_NO,
                           CREF_DATE = Data4.Key.CREF_DATE,
                           DREF_DATE = ConvertStringToDateTimeFormat(Data4.Key.CREF_DATE),
                           CTENURE = Data4.Key.CTENURE,
                           CAGREEMENT_STATUS_NAME = Data4.Key.CAGREEMENT_STATUS_NAME,
                           CTRANS_STATUS_NAME = Data4.Key.CTRANS_STATUS_NAME,
                           NTOTAL_PRICE = Data4.Key.NTOTAL_PRICE,
                           CTAX = Data4.Key.CTAX,
                           CTENANT_ID = Data4.Key.CTENANT_ID,
                           CTENANT_NAME = Data4.Key.CTENANT_NAME,
                           CTC_MESSAGE = Data4.Key.CTC_MESSAGE,
                           UnitDetail = Data4
                           .GroupBy(Data5 => new
                           {
                               Data5.CUNIT_DETAIL_ID,
                               Data5.NUNIT_DETAIL_GROSS_AREA_SIZE,
                               Data5.NUNIT_DETAIL_NET_AREA_SIZE,
                               Data5.NUNIT_DETAIL_COMMON_AREA_SIZE,
                               Data5.NUNIT_DETAIL_PRICE,
                               Data5.NUNIT_TOTAL_GROSS_AREA_SIZE,
                               Data5.NUNIT_TOTAL_NET_AREA_SIZE,
                               Data5.NUNIT_TOTAL_COMMON_AREA_SIZE,
                               Data5.NUNIT_TOTAL_PRICE
                           }).Select(Data6 => new PMR00150DataDetailUnitDTO
                           {
                               CUNIT_DETAIL_ID = Data6.Key.CUNIT_DETAIL_ID,
                               NUNIT_DETAIL_GROSS_AREA_SIZE = Data6.Key.NUNIT_DETAIL_GROSS_AREA_SIZE,
                               NUNIT_DETAIL_NET_AREA_SIZE = Data6.Key.NUNIT_DETAIL_NET_AREA_SIZE,
                               NUNIT_DETAIL_COMMON_AREA_SIZE = Data6.Key.NUNIT_DETAIL_COMMON_AREA_SIZE,
                               NUNIT_DETAIL_PRICE = Data6.Key.NUNIT_DETAIL_PRICE,
                               NUNIT_TOTAL_GROSS_AREA_SIZE = Data6.Key.NUNIT_TOTAL_GROSS_AREA_SIZE,
                               NUNIT_TOTAL_NET_AREA_SIZE = Data6.Key.NUNIT_TOTAL_NET_AREA_SIZE,
                               NUNIT_TOTAL_COMMON_AREA_SIZE = Data6.Key.NUNIT_TOTAL_COMMON_AREA_SIZE,
                               NUNIT_TOTAL_PRICE = Data6.Key.NUNIT_TOTAL_PRICE
                           }).ToList(),
                           ChargeDetail = Data4
                           .GroupBy(Data5 => new
                           {
                               Data5.CCHARGE_DETAIL_TYPE_NAME
                           }).Select(Data6 => new PMR00150DataDetailChargeDTO
                           {
                               CCHARGE_DETAIL_TYPE_NAME = Data6.Key.CCHARGE_DETAIL_TYPE_NAME,
                               ChargeTypeDetail = Data6
                               .GroupBy(Data7 => new
                               {
                                   Data7.CCHARGE_DETAIL_UNIT_NAME
                               }).Select(Data8 => new PMR00150DataDetailChargeTypeDTO
                               {
                                   CCHARGE_DETAIL_UNIT_NAME = Data8.Key.CCHARGE_DETAIL_UNIT_NAME,
                                   ChargeTypeUnitDetail = Data8
                                   .GroupBy(Data9 => new
                                   {
                                       Data9.CCHARGE_DETAIL_CHARGE_NAME,
                                       Data9.CCHARGE_DETAIL_TAX_NAME,
                                       Data9.CCHARGE_DETAIL_START_DATE,
                                       Data9.CCHARGE_DETAIL_END_DATE,
                                       Data9.CCHARGE_DETAIL_TENURE,
                                       Data9.CCHARGE_DETAIL_FEE_METHOD,
                                       Data9.NCHARGE_DETAIL_FEE_AMOUNT,
                                       Data9.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT
                                   })
                                   .Select(Data10 => new PMR00150DataDetailChargeTypeUnitDTO
                                   {
                                       CCHARGE_DETAIL_CHARGE_NAME = Data10.Key.CCHARGE_DETAIL_CHARGE_NAME,
                                       CCHARGE_DETAIL_TAX_NAME = Data10.Key.CCHARGE_DETAIL_TAX_NAME,
                                       CCHARGE_DETAIL_START_DATE = Data10.Key.CCHARGE_DETAIL_START_DATE,
                                       CCHARGE_DETAIL_END_DATE = Data10.Key.CCHARGE_DETAIL_END_DATE,
                                       DCHARGE_DETAIL_START_DATE = ConvertStringToDateTimeFormat(Data10.Key.CCHARGE_DETAIL_START_DATE)!,
                                       DCHARGE_DETAIL_END_DATE = ConvertStringToDateTimeFormat(Data10.Key.CCHARGE_DETAIL_END_DATE)!,
                                       CCHARGE_DETAIL_TENURE = Data10.Key.CCHARGE_DETAIL_TENURE,
                                       CCHARGE_DETAIL_FEE_METHOD = Data10.Key.CCHARGE_DETAIL_FEE_METHOD,
                                       NCHARGE_DETAIL_FEE_AMOUNT = Data10.Key.NCHARGE_DETAIL_FEE_AMOUNT,
                                       NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT = Data10.Key.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT
                                   }).ToList(),
                               }).ToList(),
                           }).ToList(),
                           DepositDetail = Data4
                              .GroupBy(Data5 => new
                              {
                                  Data5.CDEPOSIT_DETAIL_ID,
                                  Data5.CDEPOSIT_DETAIL_DATE,
                                  Data5.NDEPOSIT_DETAIL_AMOUNT,
                                  Data5.CDEPOSIT_DETAIL_DESCRIPTION
                              }).Select(Data6 => new PMR00150DataDetailDepositDTO
                              {
                                  CDEPOSIT_DETAIL_ID = Data6.Key.CDEPOSIT_DETAIL_ID,
                                  CDEPOSIT_DETAIL_DATE = Data6.Key.CDEPOSIT_DETAIL_DATE,
                                  DDEPOSIT_DETAIL_DATE = ConvertStringToDateTimeFormat(Data6.Key.CDEPOSIT_DETAIL_DATE),
                                  NDEPOSIT_DETAIL_AMOUNT = Data6.Key.NDEPOSIT_DETAIL_AMOUNT,
                                  CDEPOSIT_DETAIL_DESCRIPTION = Data6.Key.CDEPOSIT_DETAIL_DESCRIPTION
                              }).ToList(),
                       }).ToList(),
                   }).ToList(),
                }).ToList();

            loReturn.Data = loTempData;
            return loReturn;
        }


        public static PMR00150DetailResultWithHeaderDTO DefaultDataDetailWithHeader()
        {
            PMR00150DetailResultWithHeaderDTO loReturn = new PMR00150DetailResultWithHeaderDTO();
            loReturn.BaseHeaderData = GenerateDataModelHeader.DefaultData().BaseHeaderData;
            loReturn.PMR00150DetailResultDataFormatDTO = DefaultDataDetail();
            return loReturn;
        }
        #endregion

        #region Utility


        private static DateTime? ConvertStringToDateTimeFormat(string pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                return null; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
            }
            // Parse string ke DateTime
            DateTime result;
            if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
            return null; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
        }
        #endregion
    }
}