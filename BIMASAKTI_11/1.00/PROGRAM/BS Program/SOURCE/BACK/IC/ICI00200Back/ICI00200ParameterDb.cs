namespace ICI00200Back;

public class ICI00200ParameterDb
{
    public string CCOMPANY_ID { get; set; } = "";

    #region for SP List

    public string CCATEGORY_ID { get; set; } = "";
    public string CPRODUCT_ID { get; set; } = "";
    public string CLIST_TYPE { get; set; } = "";

    #endregion

    #region for SP Detail

    public string CFILTER_ID { get; set; } = "";
    public string CTYPE { get; set; } = "";

    #endregion
}