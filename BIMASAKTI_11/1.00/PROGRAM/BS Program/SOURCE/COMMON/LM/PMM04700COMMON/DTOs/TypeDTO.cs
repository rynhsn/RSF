namespace PMM04700Common.DTOs;

public class TypeDTO
{
    public string CCODE { get; set; }
    public string CDESCRIPTION { get; set; }
}

public class TypeParam : TypeDTO
{
    public string CLASS_APPLICATION { get; set; }
    public string CCOMPANY_ID { get; set; }
    public string CLASS_ID { get; set; }
    public string REC_ID_LIST { get; set; }
    public string LANG_ID { get; set; }
}