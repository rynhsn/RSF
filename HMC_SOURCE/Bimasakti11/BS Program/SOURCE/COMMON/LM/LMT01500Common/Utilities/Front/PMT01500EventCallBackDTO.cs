namespace PMT01500Common.Utilities.Front
{
    public class PMT01500EventCallBackDTO
    {
        public bool LContractorOnCRUDmode { get; set; }
        public bool LACTIVEUnitInfoHasData { get; set; } = false;
        public string CFlagUnitInfoHasData { get; set; } = "";
        //Updated 3 Mei 2024 : For Parameter Agreement
        public string CBUILDING_ID { get; set; } = "";
        public string CCHARGE_MODE { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";

        //For Parameter UnitInfo And Deposit
        public string CDEPT_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";

        //For Parameter ChargesInfo And Deposit
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }

    }
}
