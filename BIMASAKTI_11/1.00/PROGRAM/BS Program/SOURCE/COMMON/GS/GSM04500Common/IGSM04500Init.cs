using GSM04500Common.DTOs;

namespace GSM04500Common
{
    public interface IGSM04500Init
    {
        GSM04500ListDTO<GSM04500PropertyDTO> GSM04500GetPropertyList();
        GSM04500ListDTO<GSM04500FunctionDTO> GSM04500GetTypeList();
    }
}