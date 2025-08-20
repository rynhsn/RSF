using System;
using System.Collections.Generic;
using PMR03000Common.DTOs.Print;

namespace PMR03000Common.Model
{
    public class GenerateDataModel
    {
        // public static PMR03000ResultDataPrintDTO DefaultDataWithHeaderPrint()
        // {
        //     var loReturn = new PMR03000ResultDataPrintDTO()
        //     {
        //         Label = new PMR03000ReportLabelDTO(),
        //         Header = new PMR03000BaseHeaderDTO()
        //         {
        //             PROPERTY_NAME = "HARCO MAS RESIDENCE"
        //         },
        //     };
        //
        //     for (int i = 0; i < 3; i++)
        //     {
        //         loReturn.Data.Add(DefaultDataWithHeader());
        //     }
        //
        //     return loReturn;
        // }

        public static PMR03000ResultDataDTO DefaultDataWithHeader()
        {
            var loReturn = new PMR03000ResultDataDTO()
            {
                Label = new PMR03000ReportLabelDTO(),
                Header = new PMR03000BaseHeaderDTO()
                {
                    PROPERTY_NAME = "HARCO MAS RESIDENCE"
                },
                Datas = GenerateData(3)
            };

            return loReturn;
        }

        public static List<PMR03000VADTO> GenerateVA()
        {
            List<PMR03000VADTO> VADummy = new List<PMR03000VADTO>();
            for (int i = 0; i < 5; i++)
            {
                VADummy.Add(new PMR03000VADTO()
                {
                    CBANK_CODE = $"00 + {i}",
                    CBANK_NAME = $"BANK 0{i} INDONESIA",
                    CVA_CODE = $"0908652639-{i}"
                });
            }

            return VADummy;
        }

        public static List<PMR03000DetailUnitDTO> GenerateDataUnit()
        {
            List<PMR03000DetailUnitDTO> DataUnit = new List<PMR03000DetailUnitDTO>();
            for (int i = 0; i < 3; i++)
            {
                DataUnit.Add(new PMR03000DetailUnitDTO()
                {
                    CCOMPANY_ID = "C001",
                    CPROPERTY_ID = "P001",
                    CREF_NO = "R001",
                    CCUSTOMER_ID = "CU001",
                    CCUSTOMER_NAME = "John Doe",
                    CTRANS_DESC = "Rent Payment",
                    NOCCUPIABLE_AREA = 100.5m,
                    CINV_PERIOD_DESC = "Jan 2024",
                    NFEE_AMT = 500m,
                    NCHARGE_AMOUNT = 50m * i,
                    NTOTAL_AMOUNT = 550m
                });
            }

            return DataUnit;
        }

        public static List<PMR03000DetailUtilityDTO> GenerateDataUtility(int i)
        {
            List<PMR03000DetailUtilityDTO> DataUnit = new List<PMR03000DetailUtilityDTO>();

            DataUnit.Add(new PMR03000DetailUtilityDTO()
            {
                CCOMPANY_ID = "C001",
                CPROPERTY_ID = "P001",
                CCUSTOMER_ID = "CU001",
                CREF_NO = "R001",
                CCHARGES_TYPE = $"{i:D2}",
                CMETER_NO = $"M00{i}",
                NMETER_START = 10 * i,
                NMETER_END = 50,
                NBEBAN_BERSAMA = 10,
                NMETER_USAGE = 50,
                NCAPACITY = 5,
                NCF = 1.1m,
                NUSAGE_MIN_CHARGE = 20,
                NMIN_CHARGE_AMT = 30,
                NADDITIONAL_AMT = 5,
                NTOTAL_USAGE_KVA = 100,
                NSTANDING_CONSUMP_AMT = 200,
                NSUB_TOTAL_AMT = 300,
                NADMIN_FEE_AMT = 20,
                NVAT_AMT = 30,
                NADMIN_FEE_TAX_AMT = 5,
                NTOTAL_AMT = 355,
                CFROM_SEQ_NO = 1
            });

            return DataUnit;
        }

        public static List<PMR03000DataReportDTO> GenerateData(int len)
        {
            var loReturn = new List<PMR03000DataReportDTO>();

            for (int i = 0; i < len; i++)
            {
                var DataHeader = new PMR03000DataReportDTO()
                {
                    CSTATEMENT_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    DSTATEMENT_DATE = DateTime.Now.Date, // Gunakan nilai default untuk tanggal
                    CDUE_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    DDUE_DATE = DateTime.Now.Date, // Gunakan nilai default untuk tanggal
                    
                    
                    
                    // SUBHEADER
                    CTENANT_ID = $"CTENANT_ID {i}",
                    CTENANT_NAME = $"CTENANT_NAME {i}",
                    CBILLING_ADDRESS = $"CBILLING_ADDRESS {i}",
                    CREF_NO = $"R001 {i}",
                    CBUILDING_ID = $"CBUILDING_ID {i}",
                    CBUILDING_NAME = $"CBUILDING_NAME {i}",
                    CUNIT_ID_LIST = $"CUNIT_ID_LIST {i}",
                    CUNIT_DESCRIPTION = $"CUNIT_DESCRIPTION {i}",
                    CCURRENCY_CODE = "Rp",

                    NPREVIOUS_BALANCE = 0,
                    NPREVIOUS_PAYMENT = 0,
                    NCURRENT_PENALTY = 0,
                    NNEW_BILLING = 0,
                    NNEW_BALANCE = 0,

                    // SUPPRESS if value 0
                    NSALES = 0,
                    NRENT = 0,
                    NDEPOSIT = 0,
                    NREVENUE_SHARING = 0,
                    NSERVICE_CHARGE = 0,
                    NSINKING_FUND = 0,
                    NPROMO_LEVY = 0,
                    NGENERAL_CHARGE = 0,
                    NELECTRICITY = 0,
                    NCHILLER = 0,
                    NWATER = 12312.321m,
                    NGAS = 0,
                    NPARKING = 0,
                    NOVERTIME = 0,
                    NGENERAL_UTILITY = 0,
                
                    VirtualAccountData = GenerateVA(),
                    DataUnitList = GenerateDataUnit(),
                    DataUtility1 = GenerateDataUtility(1),
                    DataUtility1IsEmpty = true,
                    DataUtility2 = GenerateDataUtility(2),
                    DataUtility3 =
                        GenerateDataUtility(3), // new List<PMR03000DetailUtilityDTO>()  ,//GenerateDataUtility(3),
                    DataUtility4 = GenerateDataUtility(4)
                };
                
                loReturn.Add(DataHeader);
            }

            return loReturn;
        }
    }
}