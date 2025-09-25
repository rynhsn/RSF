using R_APICommonDTO;
using System.Collections.Generic;

namespace CBR00600COMMON
{
    public class CBR00600InitialDTO
    {
       public List<CBR00600PropetyDTO> VAR_PROPERTY_LIST {  get; set; }
       public List<CBR00600TrxTypeDTO> VAR_TRX_TYPE_LIST {  get; set; }
    }
}
