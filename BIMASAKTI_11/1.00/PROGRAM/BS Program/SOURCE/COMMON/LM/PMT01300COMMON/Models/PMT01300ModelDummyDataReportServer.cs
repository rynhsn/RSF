using PMT01300ReportCommon;

namespace PMT01300COMMON.Models
{
    public class PMT01300ModelDummyDataReportServer
    {
        public static PMT01300ReportResultDataDTO DefaultDataWithHeader()
        {
            var loReturn = new PMT01300ReportResultDataDTO()
            {
                Title = "LETTER OF INTENT",
                LabelReport = new PMT01300ReportLabelDTO(),
                Data = GenerateDataUtility()
            };

            return loReturn;
        }

        public static PMT01300ReportDataDTO GenerateDataUtility()
        {
            var dummyData = new PMT01300ReportDataDTO
            {
                CCOMPANY_ID = "C001",
                CCOMPANY_NAME = "PT Contoh Perusahaan",
                CPROPERTY_ID = "P001",
                CPROPERTY_NAME = "Gedung Contoh",
                CDEPT_CODE = "D001",
                CTRANS_CODE = "T001",
                CREF_NO = "REF12345",
                CREF_DATE = "20250219",
                CPROPERTY_CITY = "Jakarta",
                CPROPERTY_PROVINCE = "DKI Jakarta",
                CBUILDING_ID = "B001",
                CBUILDING_NAME = "Menara Contoh",
                CSTART_DATE = "2025-03-01",
                CSTART_TIME = "08:00",
                CEND_DATE = "2026-03-01",
                CEND_TIME = "18:00",
                IMONTHS = 12,
                IYEARS = 1,
                IDAYS = 365,
                IHOURS = 8760,
                CSALESMAN_ID = "S001",
                CSALESMAN_NAME = "Budi Santoso",
                CTENANT_ID = "T001",
                CTENANT_NAME = "CV Contoh Tenant",
                CTENANT_ADDRESS = "Jl. Contoh No. 1, Jakarta",
                CTENANT_EMAIL = "tenant@example.com",
                CTENANT_PHONE1 = "08123456789",
                CTENANT_PHONE2 = "021-98765432",
                CTENANT_ATTENTION1_NAME = "Diana Pratama",
                CTENANT_ATTENTION1_POSITION = "Manager",
                CTENANT_GROUP_NAME = "Retail Group",
                CUNIT_DESCRIPTION = "Unit Ruko 100m2",
                CNOTES = "Pembayaran dilakukan secara bertahap.",
                CCURRENCY_CODE = "IDR",
                CCURRENCY_NAME = "Rupiah",
                CUNIT_LIST = "Unit-01, Unit-02",
                NTOTAL_GROSS_AREA = 200.5m,
                NTOTAL_NET_AREA = 180.3m,
                NTOTAL_ACTUAL_AREA = 175.0m,
                NTOTAL_COMMON_AREA = 20.5m,
                NBOOKING_FEE = 5000000m,
                CLEASE_MODE = "Full Lease",
                CLEASE_MODE_DESCR = "Sewa penuh selama 1 tahun",
                CCHARGE_MODE = "Fixed",
                CCHARGE_MODE_DESCR = "Biaya tetap tanpa tambahan",
                CTRANS_STATUS = "Active",
                CTRANS_STATUS_DESCR = "Kontrak aktif",
                CAGREEMENT_STATUS = "Signed",
                CAGREEMENT_STATUS_DESCR = "Perjanjian telah ditandatangani",
                CUNIT_TYPE_CATEGORY_ID = "UC001",
                CUNIT_TYPE_CATEGORY_NAME = "Retail",
                ITOTAL_UNIT = 2,
                CEXPIRED_DATE = "2026-03-01",
                CBILLING_RULE_TYPE = "Monthly",
                CBILLING_RULE_CODE = "BR001",
                CBILLING_RULE_NAME = "Bulanan Tetap",
                LWITH_DP = true,
                IDP_PERCENTAGE = 20,
                IDP_INTERVAL = 1,
                CDP_PERIOD_MODE = "Monthly",
                IINSTALLMENT_PERCENTAGE = 80,
                IINSTALLMENT_INTERVAL = 11,
                CINSTALLMENT_PERIOD_MODE = "Monthly",
                NLEASE_AMT = 120000000m,
                NLEASE_TOTAL_AMT = 144000000m,
                NLEASE_DP_AMT = 24000000m,
                NLEASE_INSTALLMENT_AMT = 10909090m,
                NCHARGES_AMT = 500000m,
                NCHARGES_TOTAL_AMT = 6000000m,
                NDEPOSIT_TOTAL_AMT = 10000000m,
                NCAPACITY = 50.0m,
                CTC_CODE = "TC001",
                IREVISE_SEQ_NO = 0,
                LWITH_FO = true,
                CHO_PLAN_DATE = "2025-02-25",
                CHO_ACTUAL_DATE = "2025-02-28",
                CACTUAL_START_DATE = "2025-03-01",
                CACTUAL_START_TIME = "08:00",
                CACTUAL_END_DATE = "2026-03-01",
                CACTUAL_END_TIME = "18:00",
                CREC_ID = "REC001",
                CSOLD_DATE = "",
                NACTUAL_PRICE = 120000000m,
                NSELL_PRICE = 140000000m,
                CUNIT_ID = "U001",
                CUNIT_NAME = "Unit-01",
                CFLOOR_NAME = "Floor Name GF -001"
            };

            return dummyData;
        }
    }
}