using System.Collections.Generic;
using PMT01500Common.DTO._7._Document;
using PMT01500Common.Utilities;
using R_CommonFrontBackAPI;

namespace PMT01500Common.Interface
{
    public interface IPMT01500Document : R_IServiceCRUDBase<PMT01500DocumentDetailDTO>
    {
        PMT01500DocumentHeaderDTO GetDocumentHeader(PMT01500GetHeaderParameterDTO poParameter);
        IAsyncEnumerable<PMT01500DocumentListDTO> GetDocumentList();
        
    }
}