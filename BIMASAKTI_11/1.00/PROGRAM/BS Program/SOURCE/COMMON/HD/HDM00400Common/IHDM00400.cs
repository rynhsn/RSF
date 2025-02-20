using System.Collections.Generic;
using HDM00400Common.DTOs;
using HDM00400Common.Param;
using R_CommonFrontBackAPI;

namespace HDM00400Common
{
    public interface IHDM00400 : R_IServiceCRUDBase<HDM00400PublicLocationDTO>
    {
        HDM00400ListDTO<HDM00400PropertyDTO> HDM00400GetPropertyList();
        IAsyncEnumerable<HDM00400PublicLocationDTO> HDM00400GetPublicLocationListStream();
        HDM00400SingleDTO<HDM00400ActiveInactiveDTO> HDM00400ActivateInactivate(HDM00400ActiveInactiveParamsDTO poParams);
        HDM00400PublicLocationExcelDTO HDM00400DownloadTemplateFileModel();
    }
}