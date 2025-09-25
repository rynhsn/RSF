using BaseAOC_BS11Common.DTO.Utilities.BaseDTO;

namespace PMT01900Common.DTO.CRUDBase
{
    public class PMT01900Unit_UtilitiesDetailDTO : BaseAOCDateTimeDTO
    {
        public string CTAX_ID { get; set; } = "";
        public int IMETER_START { get; set; } = 0;
        public string? CSTART_INV_PRD { get; set; } = "";
        //For DB Parameter : R_Display
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CCHARGES_TYPE { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CSEQ_NO { get; set; }
        public string? CUSER_ID { get; set; }
        //RealDto : For Front
        public string? CUTILITY_TYPE_DESCR { get; set; }
        public string? CMETER_NO { get; set; }
        public decimal NCAPACITY { get; set; }
        public decimal NCALCULATION_FACTOR { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public string? CSTATUS_DESCR { get; set; }

        //CR 23/07/2024
        public string? CTAX_NAME { get; set; }
    }
}
