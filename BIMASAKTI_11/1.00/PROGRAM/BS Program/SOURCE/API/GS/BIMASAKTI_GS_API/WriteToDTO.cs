public class WriteToDTO
{
    public string Name {get; set;}
    public SrArgDTO Args { get; set;}=new SrArgDTO();
}


public class SrArgDTO
{
    public string endpoint { get; set;}
}