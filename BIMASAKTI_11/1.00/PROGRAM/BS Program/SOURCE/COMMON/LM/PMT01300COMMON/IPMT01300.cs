using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public interface IPMT01300 
    {
        IAsyncEnumerable<PMT01300DTO> GetLOIListStream();
        PMT01300SingleResult<PMT01300DTO> GetLOI(PMT01300DTO poEntity);
        PMT01300SingleResult<PMT01300DTO> SaveLOI(PMT01300SaveDTO<PMT01300DTO> poEntity);
        PMT01300SingleResult<PMT01300DTO> SubmitRedraftAgreementTrans(PMT01300SubmitRedraftDTO poEntity);
        PMT01300UploadFileDTO DownloadTemplateFile();

    }
}
