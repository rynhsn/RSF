namespace Lookup_GSCOMMON.DTOs
{
    public class GSL03200ParameterDTO
    {
        public string CSEARCH_TEXT { get; set; } = "";
        public string CACTIVE_TYPE { get; set; } = "";

        #region CR21
        public string CDEPT_CODE { get; set; } = "";
        #endregion

        #region CR24
        public string CALLOC_ID { get; set; } = "";
        #endregion
    }

}
