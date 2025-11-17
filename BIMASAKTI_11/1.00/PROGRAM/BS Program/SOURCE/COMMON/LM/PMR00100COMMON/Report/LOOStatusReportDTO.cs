using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00100Common.Report
{
    public class LOOStatusHeaderDTO
    {
        //HEADER

        public string CPROPERTY { get; set; }
        public string CDEPT { get; set; }
        public string CSALESMAN { get; set; }
        public string CPERIOD { get; set; }
        public string CREPORT_NAME { get; set; }
    }

    public class LOOStatusDetail1DTO
    {
        //DETAIL
        public string CTRANS_NAME { get; set; }
        public List<LOOStatusDetail2DTO> LOOStatusDetail2 { get; set; }
    }

    public class LOOStatusDetail2DTO
    {
        public string CSALESMAN_ID { get; set; }
        public string CSALESMAN_NAME { get; set; }

        public List<LOOStatusDetail3DTO> LOOStatusDetail3 { get; set; }
    }
    public class LOOStatusDetail3DTO
    {
        public string CREF_NO { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CTENURE { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CAGREEMENT_STATUS_NAME { get; set; }
        public string CTRANS_STATUS_NAME { get; set; }
        public decimal NREVISION_COUNT { get; set; }
        public decimal NTOTAL_PRICE { get; set; }
        public string CTAX { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CTENANT => $"{CTENANT_ID} - {CTENANT_NAME}";
        public string CTC_MESSAGE { get; set; }
        public List<LOOStatusDetailUnitDTO> LOOStatusDetailUnit { get; set; }
        public List<LOOStatusDetailChargeDTO> LOOStatusDetailCharge { get; set; }
        public List<LOOStatusDetailDepositDTO> LOOStatusDetailDeposit { get; set; }


    }

    #region Unit
    public class LOOStatusDetailUnitDTO
    {
        public string CUNIT_DETAIL_ID { get; set; }
        public string CUNIT_DETAIL_NAME { get; set; }
        public string CUNIT => $"{CUNIT_DETAIL_ID} - {CUNIT_DETAIL_NAME}";
        public decimal NUNIT_DETAIL_GROSS_AREA_SIZE { get; set; }
        public decimal NUNIT_DETAIL_NET_AREA_SIZE { get; set; }
        public decimal NUNIT_DETAIL_COMMON_AREA_SIZE { get; set; }
        public decimal NUNIT_DETAIL_PRICE { get; set; }
        public int ITOTAL_UNIT { get; set; }
    }
    #endregion

    #region Charge
    public class LOOStatusDetailChargeDTO
    {
        public string CCHARGE_DETAIL_TYPE_NAME { get; set; }
        public List<LOOStatusDetailChargeTypeUnitDTO> LOOStatusDetailChargeUnit { get; set; }
    }
    public class LOOStatusDetailChargeTypeUnitDTO
    {
        public string CCHARGE_DETAIL_UNIT_NAME { get; set; }
        public List<LOOStatusDetailChargeTypeUnitChargeDTO> LOOStatusDetailChargeTypeUnitCharge { get; set; }
    }
    public class LOOStatusDetailChargeTypeUnitChargeDTO
    {
        public string CCHARGE_DETAIL_CHARGE_NAME { get; set; }
        public string CCHARGE_DETAIL_TAX_NAME { get; set; }
        public DateTime? DCHARGE_DETAIL_START_DATE { get; set; }
        public DateTime? DCHARGE_DETAIL_END_DATE { get; set; }
        public string CCHARGE_DETAIL_TENURE { get; set; }
        public string CCHARGE_DETAIL_FEE_METHOD { get; set; }
        public decimal NCHARGE_DETAIL_FEE_AMOUNT { get; set; }
        public bool LFOR_TOTAL_AREA { get; set; }
        public decimal NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT { get; set; }
        public decimal NCHARGE_DETAIL_SUBTOTAL_CALCULATED_FEE_AMOUNT { get; set; }
    }
    #endregion

    #region Deposit
    public class LOOStatusDetailDepositDTO
    {
        public string CDEPOSIT_DETAIL_ID { get; set; }
        public DateTime? DDEPOSIT_DETAIL_DATE { get; set; }
        public decimal NDEPOSIT_DETAIL_AMOUNT { get; set; }
        public string CDEPOSIT_DETAIL_DESCRIPTION { get; set; }
        public int ITOTAL_DEPOSIT { get; set; }
    }
    #endregion

    #region Total
    public class LOOStatusTotalTransDTO
    {
        public string COL_TOTAL_TRANS { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANS_NAME { get; set; }
        public int CTRANS_TOTAL { get; set; }
    }
    public class LOOStatusTotalSalesDTO
    {
        public string COL_TOTAL_SALESMAN { get; set; }
        public string CSALESMAN_ID { get; set; }
        public int CSALESMAN_TOTAL { get; set; }
    }
    #endregion



}
