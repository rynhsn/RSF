namespace PMR00400Common.DTOs
{
    public class PMR00400DataDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public decimal NBUILDING_SPACE { get; set; }
        public decimal NBUILDING_GROSS_AREA_SIZE { get; set; }
        public decimal NBUILDING_NET_AREA_SIZE { get; set; }
        public decimal NBUILDING_COMMON_AREA_SIZE { get; set; }
        public decimal NBUILDING_EMPTY_SPACE { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CFLOOR_NAME { get; set; }
        public decimal NFLOOR_SPACE { get; set; }
        public decimal NFLOOR_GROSS_AREA_SIZE { get; set; }
        public decimal NFLOOR_NET_AREA_SIZE { get; set; }
        public decimal NFLOOR_COMMON_AREA_SIZE { get; set; }
        public decimal NFLOOR_EMPTY_SPACE { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CUNIT_TYPE_ID { get; set; }
        public string CUNIT_TYPE_NAME { get; set; }
        public decimal NUNIT_TYPE_GROSS_AREA_SIZE { get; set; }
        public decimal NUNIT_TYPE_NET_AREA_SIZE { get; set; }
        public decimal NUNIT_TYPE_COMMON_AREA_SIZE { get; set; }
        public string CUNIT_TYPE_CATEGORY_ID { get; set; }
        public string CUNIT_TYPE_CATEGORY_NAME { get; set; }
        public string CUNIT_VIEW_ID { get; set; }
        public string CUNIT_VIEW_NAME { get; set; }
        public string CUNIT_CATEGORY_NAME { get; set; }
        public decimal NUNIT_GROSS_AREA_SIZE { get; set; }
        public decimal NUNIT_NET_AREA_SIZE { get; set; }
        public decimal NUNIT_COMMON_AREA_SIZE { get; set; }
        public string CSTRATA_STATUS_NAME { get; set; }
        public string COWNER_ID { get; set; }
        public string COWNER_NAME { get; set; }
        public string CLEASE_STATUS_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CUNIT_DESCRIPTION { get; set; }
    }
}