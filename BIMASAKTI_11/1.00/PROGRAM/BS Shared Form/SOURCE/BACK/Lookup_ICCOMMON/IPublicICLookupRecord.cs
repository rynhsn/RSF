using Lookup_ICCOMMON.DTOs;
using Lookup_ICCOMMON.DTOs.ICL00100;
using Lookup_ICCOMMON.DTOs.ICL00200;
using Lookup_ICCOMMON.DTOs.ICL00300;

namespace Lookup_ICCOMMON
{
    public interface IPublicICLookupRecord
    {
        ICLGenericRecord<ICL00100DTO> ICL00100GetRecord(ICL00100ParameterDTO poEntity);
        ICLGenericRecord<ICL00200DTO> ICL00200GetRecord(ICL00200ParameterDTO poEntity);
        ICLGenericRecord<ICL00300DTO> ICL00300GetRecord(ICL00300ParameterDTO poEntity);
    }
}