using Lookup_CBCOMMON.DTOs;
using Lookup_CBCOMMON.DTOs.CBL00100;
using Lookup_CBCOMMON.DTOs.CBL00200;

namespace Lookup_CBCOMMON
{
    public interface IPublicCBLookupRecord
    {
        CBLGenericRecord<CBL00100DTO> CBL00100GetRecord(CBL00100ParameterDTO poEntity);
        CBLGenericRecord<CBL00200DTO> CBL00200GetRecord(CBL00200ParameterDTO poEntity);
        
    }
}