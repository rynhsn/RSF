using BaseHeaderReportCOMMON;
using PMR00200COMMON.DTO_s;
using PMR00200COMMON.Print_DTO;
using PMR00200COMMON.Print_DTO.Detail;
using PMR00200COMMON.Print_DTO.Detail.SubDetail;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PMR00200COMMON.Model
{
    public class PMR00210DummyData
    {
        public static PMR00210PrintDislpayDTO PMR00210PrintDislpayWithBaseHeader()
        {
            PMR00210PrintDislpayDTO loRtn = new PMR00210PrintDislpayDTO();
            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "PMR00200",
                CPRINT_NAME = "LOI Status",
                CUSER_ID = "GHC",
            };
            loRtn.DetailData = new PMR00210ReportDataDTO();
            loRtn.DetailData.Header = "PM";
            loRtn.DetailData.Title = "LOI Status";
            loRtn.DetailData.Column = new PMR00200LabelDTO();
            loRtn.DetailData.HeaderParam = new PMR00200ParamDTO()
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
                CREPORT_TYPE_DISPLAY = "Detail"
            };
            DateTime loFromDate = DateTime.ParseExact(loRtn.DetailData.HeaderParam.CFROM_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);
            DateTime loToDate = DateTime.ParseExact(loRtn.DetailData.HeaderParam.CTO_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);
            loRtn.DetailData.HeaderParam.CPERIOD_DISPLAY = (loFromDate.Year != loToDate.Year || loFromDate.Month != loToDate.Month)
                ? $"{loFromDate:MMMM yyyy} – {loToDate:MMMM yyyy}"
                : $"{loFromDate:MMMM yyyy}";
            loRtn.DetailData.Data = new List<TransactionDTO>();

            List<PMR00210SPResultDTO> loCollectionData = new List<PMR00210SPResultDTO>();

            //creation for dummy 
            for (int i = 1; i < 2; i++) //TransName
            {
                for (int j = 1; j < 2; j++) //Salesman
                {
                    for (int k = 1; k < 2; k++) //REFDATE??LOINO
                    {
                        for (int l = 1; l < 3; l++) //UNIT
                        {
                            for (int m = 1; m < 2; m++) //ChargeType
                            {
                                for (int n = 1; n < 4; n++) //ChargeTypeUnit
                                {
                                    for (int o = 1; o < 4; o++) //ChargeTypeUnitDetail
                                    {
                                        loCollectionData.Add(new PMR00210SPResultDTO()
                                        {
                                            CTRANS_CODE = $"Code {i}",
                                            CTRANS_NAME = $"TransName {i}",
                                            CSALESMAN_ID = $"ID{i}",
                                            CSALESMAN_NAME = $"NameStaff{i}",
                                            CREF_NO = $"RfNo{k}",
                                            CREF_DATE = "20241224",
                                            //    DREF_DATE = ConvertStringToDateTimeFormat("20241224"),
                                            CTENURE = $"99 year(s), 12 Month(s), 31 Day(s)",
                                            CAGREEMENT_STATUS_NAME = $"sign{k}",
                                            CTRANS_STATUS_NAME = $"In Progress",
                                            NTOTAL_PRICE = 180000000m,
                                            CTENANT_ID = $"SM{k}",
                                            CTENANT_NAME = $"Muhammad Andika Putra {k}",
                                            CTC_MESSAGE = $"This is dummy data for Term & Con",
                                            CUNIT_DETAIL_ID = $"ID Unit{l}",
                                            CUNIT_DETAIL_NAME = $"Unit Name{l}",
                                            NUNIT_DETAIL_GROSS_AREA_SIZE = 100,
                                            NUNIT_DETAIL_NET_AREA_SIZE = 30,
                                            NUNIT_DETAIL_COMMON_AREA_SIZE = 40,
                                            NUNIT_DETAIL_PRICE = 50,
                                            CUTILITY_DETAIL_TYPE_ID = $"E{o}",
                                            CUTILITY_DETAIL_TYPE_NAME = $"Electricity{o}",
                                            CUTILITY_DETAIL_UNIT_NAME = $"Unit{l}",
                                            CUTILITY_DETAIL_CHARGE_ID = $"ChargeID{o}",
                                            CUTILITY_DETAIL_CHARGE_NAME = $"Charge {o}",
                                            CUTILITY_DETAIL_TAX_NAME = $"PPN 11%{o}",
                                            CUTILITY_DETAIL_METER_NO = $"Meter{o}000000",
                                            NUTILITY_DETAIL_METER_START = 100 * o,
                                            CUTILITY_DETAIL_START_INV_PRD = "20240101",
                                            NUTILITY_DETAIL_BLOCK1_START = 10.00m*o,
                                            NUTILITY_DETAIL_BLOCK2_START = 10.00m*o,
                                            CUTILITY_DETAIL_STATUS_NAME = $"Inactive{o}",
                                            CCHARGE_DETAIL_TYPE_NAME = $"Charges Type {m}",
                                            CCHARGE_DETAIL_UNIT_NAME = $"Unit {m}",
                                            CCHARGE_DETAIL_CHARGE_NAME = $"ChargeName {n}",
                                            CCHARGE_DETAIL_TAX_NAME = $"TaxName {n}",
                                            CCHARGE_DETAIL_START_DATE = "20241224",
                                            CCHARGE_DETAIL_END_DATE = "20241224",
                                            CCHARGE_DETAIL_TENURE = $"99 year(s), 12 Month(s), 31 Day(s)",
                                            CCHARGE_DETAIL_FEE_METHOD = $"Monthly {n}",
                                            NCHARGE_DETAIL_FEE_AMOUNT = 550000m * k,
                                            NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT = 999999999m,
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
            }).Select(Data1 => new TransactionDTO
            {
                CTRANS_CODE = Data1.Key.CTRANS_CODE,
                CTRANS_NAME = Data1.Key.CTRANS_NAME,
                TransactionDetail = Data1
               .GroupBy(Data => new
               {
                   Data.CSALESMAN_ID,
                   Data.CSALESMAN_NAME,
               }).Select(Data2 => new SalesmanDTO
               {
                   CSALESMAN_ID = Data2.Key.CSALESMAN_ID,
                   CSALESMAN_NAME = Data2.Key.CSALESMAN_NAME,
                   SalesmanDetail = Data2
                   .GroupBy(Data3 => new
                   {
                       Data3.CREF_NO,
                       Data3.CREF_DATE,
                       Data3.DREF_DATE,
                       Data3.CTENURE,
                       Data3.CTRANS_STATUS_NAME,
                       Data3.NTOTAL_PRICE,
                       Data3.CTENANT_ID,
                       Data3.CTENANT_NAME,
                       Data3.CTC_MESSAGE,
                   }).Select(Data4 => new LoiNoDTO
                   {
                       CREF_NO = Data4.Key.CREF_NO,
                       CREF_DATE = Data4.Key.CREF_DATE,
                       DREF_DATE = DateTime.TryParseExact(Data4.Key.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loresultRefDate) ? loresultRefDate : DateTime.Now,
                       CTENURE = Data4.Key.CTENURE,
                       CTRANS_STATUS_NAME = Data4.Key.CTRANS_STATUS_NAME,
                       NTOTAL_PRICE = Data4.Key.NTOTAL_PRICE,
                       CTENANT_ID = Data4.Key.CTENANT_ID,
                       CTENANT_NAME = Data4.Key.CTENANT_NAME,
                       CTC_MESSAGE = Data4.Key.CTC_MESSAGE,
                       UnitDetail = Data4
                       .GroupBy(Data5 => new
                       {
                           Data5.CUNIT_DETAIL_ID,
                           Data5.CUNIT_DETAIL_NAME,
                           Data5.NUNIT_DETAIL_GROSS_AREA_SIZE,
                           Data5.NUNIT_DETAIL_NET_AREA_SIZE,
                           Data5.NUNIT_DETAIL_COMMON_AREA_SIZE,
                           Data5.NUNIT_DETAIL_PRICE,
                       }).Select(Data6 => new UnitDTO
                       {
                           CUNIT_DETAIL_ID = Data6.Key.CUNIT_DETAIL_ID,
                           CUNIT_DETAIL_NAME = Data6.Key.CUNIT_DETAIL_NAME,
                           NUNIT_DETAIL_GROSS_AREA_SIZE = Data6.Key.NUNIT_DETAIL_GROSS_AREA_SIZE,
                           NUNIT_DETAIL_NET_AREA_SIZE = Data6.Key.NUNIT_DETAIL_NET_AREA_SIZE,
                           NUNIT_DETAIL_COMMON_AREA_SIZE = Data6.Key.NUNIT_DETAIL_COMMON_AREA_SIZE,
                           NUNIT_DETAIL_PRICE = Data6.Key.NUNIT_DETAIL_PRICE,
                       }).ToList(),
                       UtilityDetail = Data4
                       .GroupBy(Data11 => new
                       {
                           Data11.CUTILITY_DETAIL_TYPE_NAME
                       }).Select(Data12 => new UtilityDTO
                       {
                           CUTILITY_DETAIL_TYPE_NAME = Data12.Key.CUTILITY_DETAIL_TYPE_NAME,
                           UtilityTypeDetail = Data12
                         .GroupBy(Data13 => new
                         {
                             Data13.CUTILITY_DETAIL_UNIT_NAME
                         }).Select(Data14 => new UtilityTypeDTO
                         {
                             CUTILITY_DETAIL_UNIT_NAME = Data14.Key.CUTILITY_DETAIL_UNIT_NAME,
                             UtilityTypeUnitDetail = Data14
                             .GroupBy(Data15 => new
                             {
                                 Data15.CUTILITY_DETAIL_CHARGE_NAME,
                                 Data15.CUTILITY_DETAIL_TAX_NAME,
                                 Data15.CUTILITY_DETAIL_METER_NO,
                                 Data15.NUTILITY_DETAIL_METER_START,
                                 Data15.CUTILITY_DETAIL_START_INV_PRD,
                                 Data15.NUTILITY_DETAIL_BLOCK1_START,
                                 Data15.NUTILITY_DETAIL_BLOCK2_START,
                                 Data15.CUTILITY_DETAIL_STATUS_NAME
                             })
                             .Select(Data16 => new UtilityTypeUnitDTO
                             {
                                 CUTILITY_DETAIL_CHARGE_NAME = Data16.Key.CUTILITY_DETAIL_CHARGE_NAME,
                                 CUTILITY_DETAIL_TAX_NAME = Data16.Key.CUTILITY_DETAIL_TAX_NAME,
                                 CUTILITY_DETAIL_METER_NO = Data16.Key.CUTILITY_DETAIL_METER_NO,
                                 NUTILITY_DETAIL_METER_START = Data16.Key.NUTILITY_DETAIL_METER_START,
                                 CUTILITY_DETAIL_START_INV_PRD = DateTime.TryParseExact(Data16.Key.CUTILITY_DETAIL_START_INV_PRD, "yyyyMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result)
        ? result.ToString("MMMM yyyy")
        : "",
                                 NUTILITY_DETAIL_BLOCK1_START = Data16.Key.NUTILITY_DETAIL_BLOCK1_START,
                                 NUTILITY_DETAIL_BLOCK2_START = Data16.Key.NUTILITY_DETAIL_BLOCK2_START,
                                 CUTILITY_DETAIL_STATUS_NAME = Data16.Key.CUTILITY_DETAIL_STATUS_NAME
                             }).ToList(),
                         }).ToList(),

                       }).ToList(),
                       ChargeDetail = Data4
                       .GroupBy(Data5 => new
                       {
                           Data5.CCHARGE_DETAIL_TYPE_NAME
                       }).Select(Data6 => new ChargeDTO
                       {
                           CCHARGE_DETAIL_TYPE_NAME = Data6.Key.CCHARGE_DETAIL_TYPE_NAME,
                           ChargeTypeDetail = Data6
                           .GroupBy(Data7 => new
                           {
                               Data7.CCHARGE_DETAIL_UNIT_NAME
                           }).Select(Data8 => new ChargeTypeDTO
                           {
                               CCHARGE_DETAIL_UNIT_NAME = Data8.Key.CCHARGE_DETAIL_UNIT_NAME,
                               ChargeTypeUnitDetail = Data8
                               .GroupBy(Data9 => new
                               {
                                   Data9.CCHARGE_DETAIL_CHARGE_NAME,
                                   Data9.CCHARGE_DETAIL_TAX_NAME,
                                   Data9.CCHARGE_DETAIL_START_DATE,
                                   Data9.CCHARGE_DETAIL_END_DATE,
                                   Data9.DCHARGE_DETAIL_START_DATE,
                                   Data9.DCHARGE_DETAIL_END_DATE,
                                   Data9.CCHARGE_DETAIL_TENURE,
                                   Data9.CCHARGE_DETAIL_FEE_METHOD,
                                   Data9.NCHARGE_DETAIL_FEE_AMOUNT,
                                   Data9.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT
                               })
                               .Select(Data10 => new ChargeTypeUnitDTO
                               {
                                   CCHARGE_DETAIL_CHARGE_NAME = Data10.Key.CCHARGE_DETAIL_CHARGE_NAME,
                                   CCHARGE_DETAIL_TAX_NAME = Data10.Key.CCHARGE_DETAIL_TAX_NAME,
                                   CCHARGE_DETAIL_START_DATE = Data10.Key.CCHARGE_DETAIL_START_DATE,
                                   CCHARGE_DETAIL_END_DATE = Data10.Key.CCHARGE_DETAIL_END_DATE,
                                   DCHARGE_DETAIL_START_DATE = DateTime.TryParseExact(Data10.Key.CCHARGE_DETAIL_START_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultStartDate) ? loResultStartDate : DateTime.Now,
                                   DCHARGE_DETAIL_END_DATE = DateTime.TryParseExact(Data10.Key.CCHARGE_DETAIL_END_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultEndDate) ? loResultEndDate : DateTime.Now,
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
                              Data5.DDEPOSIT_DETAIL_DATE,
                              Data5.NDEPOSIT_DETAIL_AMOUNT,
                              Data5.CDEPOSIT_DETAIL_DESCRIPTION
                          }).Select(Data6 => new DepositDTO
                          {
                              CDEPOSIT_DETAIL_ID = Data6.Key.CDEPOSIT_DETAIL_ID,
                              CDEPOSIT_DETAIL_DATE = Data6.Key.CDEPOSIT_DETAIL_DATE,
                              DDEPOSIT_DETAIL_DATE = DateTime.TryParseExact(Data6.Key.CDEPOSIT_DETAIL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loDepoDTDate) ? loDepoDTDate : DateTime.Now,
                              NDEPOSIT_DETAIL_AMOUNT = Data6.Key.NDEPOSIT_DETAIL_AMOUNT,
                              CDEPOSIT_DETAIL_DESCRIPTION = Data6.Key.CDEPOSIT_DETAIL_DESCRIPTION
                          }).ToList(),
                       DocumentDetail = Data4
                       .GroupBy(Data17 => new
                       {
                           Data17.CDOCUMENT_DETAIL_NO,
                           Data17.CDOCUMENT_DETAIL_DATE,
                           Data17.DDOCUMENT_DETAIL_DATE,
                           Data17.CDOCUMENT_DETAIL_EXPIRED_DATE,
                           Data17.DDOCUMENT_DETAIL_EXPIRED_DATE,
                           Data17.CDOCUMENT_DETAIL_FILE,
                           Data17.CDOCUMENT_DETAIL_DESCRIPTION
                       }).Select(Data18 => new DocumentDTO
                       {
                           CDOCUMENT_DETAIL_NO = Data18.Key.CDOCUMENT_DETAIL_NO,
                           CDOCUMENT_DETAIL_DATE = Data18.Key.CDOCUMENT_DETAIL_DATE,
                           DDOCUMENT_DETAIL_DATE = DateTime.TryParseExact(Data18.Key.CDOCUMENT_DETAIL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loDocDtDate) ? loDocDtDate : DateTime.Now,
                           CDOCUMENT_DETAIL_EXPIRED_DATE = Data18.Key.CDOCUMENT_DETAIL_EXPIRED_DATE,
                           DDOCUMENT_DETAIL_EXPIRED_DATE = DateTime.TryParseExact(Data18.Key.CDOCUMENT_DETAIL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loDocDtExpDate) ? loDocDtExpDate : DateTime.Now,
                           CDOCUMENT_DETAIL_FILE = Data18.Key.CDOCUMENT_DETAIL_DATE,
                           CDOCUMENT_DETAIL_DESCRIPTION = Data18.Key.CDOCUMENT_DETAIL_DESCRIPTION,
                       }).ToList(),
                   }).ToList(),
               }).ToList(),
            }).ToList();

            loRtn.DetailData.Data = loTempData;
            return loRtn;
        }
    }


}
