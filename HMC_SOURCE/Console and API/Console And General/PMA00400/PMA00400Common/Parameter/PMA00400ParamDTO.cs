namespace PMA00400Common.Parameter
{
    public class PMA00400ParamDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CBUILDING_ID { get; set; } //@LASSIGNED CIMAGE_TYPE
        public bool LASSIGNED { get; set; }
        //     public string? CIMAGE_TYPE { get; set; }
        public string? CLANG_ID { get; set; }
        public string? CSTORAGE_ID { get; set; }
        public string? CPROGRAM_ID { get; set; }
    }
}
