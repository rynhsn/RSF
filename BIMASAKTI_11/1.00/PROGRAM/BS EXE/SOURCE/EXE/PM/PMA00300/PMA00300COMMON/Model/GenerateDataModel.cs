using PMA00300COMMON.VA_DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace PMA00300COMMON.Model
{
    public class GenarateDataModel
    {
        public static PMA00300ResultDataDTO DefaultDataWithHeader()
        {
            PMA00300ResultDataDTO loReturn = new PMA00300ResultDataDTO()
            {
                Label = new PMA00300LabelDTO(),
                Header = new PMA00300BaseHeaderDTO()
                {
                    PROPERTY_NAME = "HARCO MAS RESIDENCE"
                },
                Data = GenerateData(),
                VirtualAccountData = GenerateVA(),
                DataUnitList = GenerateDataUnit(),
                DataUtility1 =GenerateDataUtility(1),
                DataUtility2 = GenerateDataUtility(2),
                DataUtility3 = GenerateDataUtility(3), // new List<PMA00300UtilityChargesDetail>()  ,//GenerateDataUtility(3),
                DataUtility4 = GenerateDataUtility(4)
            };

            return loReturn;
        }
        public static List<PMA00300VADTO> GenerateVA()
        {
            List<PMA00300VADTO> VADummy = new List<PMA00300VADTO>();
            for (int i = 0; i < 5; i++)
            {
                VADummy.Add(new PMA00300VADTO()
                {
                    CBANK_CODE = $"00 + {i}",
                    CBANK_NAME = $"BANK 0{i} INDONESIA",
                    CVA_CODE = $"0908652639-{i}"
                });
            }
            return VADummy;
        }
        public static List<PMA00300UtilityDetail> GenerateDataUnit()
        {
            List<PMA00300UtilityDetail> DataUnit = new List<PMA00300UtilityDetail>();
            for (int i = 0; i < 3; i++)
            {
                DataUnit.Add(new PMA00300UtilityDetail()
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
        public static List<PMA00300UtilityChargesDetail> GenerateDataUtility(int i)
        {
            List<PMA00300UtilityChargesDetail> DataUnit = new List<PMA00300UtilityChargesDetail>();

            DataUnit.Add(new PMA00300UtilityChargesDetail()
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
        public static PMA00300DataReportDTO GenerateData()
        {
            PMA00300DataReportDTO DataHeader = new PMA00300DataReportDTO()
            {
                CSTATEMENT_DATE = DateTime.Now.ToString("yyyyMMdd"),
                DSTATEMENT_DATE = DateTime.Now.Date, // Gunakan nilai default untuk tanggal
                CDUE_DATE = DateTime.Now.ToString("yyyyMMdd"),
                DDUE_DATE = DateTime.Now.Date, // Gunakan nilai default untuk tanggal

                // SUBHEADER
                CTENANT_ID = "CTENANT_ID",
                CTENANT_NAME = "CTENANT_NAME",
                CBILLING_ADDRESS = "CBILLING_ADDRESS",
                CREF_NO = "R001",
                CBUILDING_ID = "CBUILDING_ID",
                CBUILDING_NAME = "CBUILDING_NAME",
                CUNIT_ID_LIST = "CUNIT_ID_LIST",
                CUNIT_DESCRIPTION = "CUNIT_DESCRIPTION",
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
            };
            return DataHeader;
        }
    }
}
