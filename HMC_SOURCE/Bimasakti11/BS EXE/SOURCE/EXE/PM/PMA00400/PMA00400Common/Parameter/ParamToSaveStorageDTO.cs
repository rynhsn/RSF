namespace PMA00400Common.Parameter
{
    public class ParamToSaveStorageDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CREC_ID { get; set; } = "";
        public string CUNIT_ID { get; set; } = "";
        public string CFLOOR_ID { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CSTORAGE_ID { get; set; } = "";
        public string FileExtension { get; set; }
        public byte[] REPORT { get; set; }
    }
}
